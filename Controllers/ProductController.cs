using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using asp_ecommerce.Models;
using asp_ecommerce.ViewModels;


namespace asp_ecommerce.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("/stuff/all")]
        public IActionResult Index()
        {
            List<Product> all_stuff = _context.Products.OrderByDescending(s => s.Created_date_by_seller).ToList();
            ViewBag.all_stuff = all_stuff;
            ViewBag.stuff_info = TempData["stuff_info"];
            ViewBag.confirm_order = TempData["confirm_order"];
            return View("Index");
        }

        [Authorize]
        [HttpPost]
        [Route("/stuff/add")]
        public IActionResult AddStuff(ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (_context.Products.Any(p => p.Name.ToLower() == vm.Name.ToLower() && p.ApplicationUserName == User.Identity.Name))
                {
                    ModelState.AddModelError("", "You already have similar stuff to sell, please add something new.");
                    // TODO: get rid of this, using AJAX.
                    List<Product> all_stuff = _context.Products.OrderByDescending(s => s.Created_date_by_seller).ToList();
                    ViewBag.all_stuff = all_stuff;
                    return View("Index", vm);
                }
                else
                {
                    var task = _userManager.GetUserAsync(User).Result;
                    
                    Product new_stuff = new Product
                    {
                        Name = vm.Name,
                        Url = vm.URL,
                        Desc = vm.Desc,
                        Qty = vm.Qty,
                        ApplicationUserName = User.Identity.Name,
                        ApplicationUserId = task.Id,
                        Created_date_by_seller = DateTime.Now,
                    };

                    _context.Products.Add(new_stuff);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // TODO: get rid of this, using AJAX.
                List<Product> all_stuff = _context.Products.OrderByDescending(s => s.Created_date_by_seller).ToList();
                ViewBag.all_stuff = all_stuff;
                return View("Index", vm);
            }
        }


        [HttpGet]
        [Route("/stuff/shopping_cart/{productId}")]
        public IActionResult AddToShoppingCart(int productId)
        {
            HoldStuffBeforeConfirmOrder(productId);
            Product stuff_added = HttpContext.Session.GetObjectFromJson<Product>(productId.ToString());
            TempData["stuff_info"] = $"You added a {stuff_added.Name}. There are {stuff_added.Qty - 1} left.";
            return RedirectToAction("Index", "Product");
        }


        [HttpGet]
        [Route("/stuff/confirm_order")]
        public IActionResult ConfirmOrder()
        {
            //TODO: Check session, if session empty, don't create order!
            var task = _userManager.GetUserAsync(User).Result;
            //FIRST: Create a new order
            Order new_order = new Order
            {
                CustomerId = task.Id,
                Created = DateTime.Now,
            };
            _context.Orders.Add(new_order);
            _context.SaveChanges();

            //SECOND: create a new orderProduct obj with each product in HoldStuffList
            Order latest_order = _context.Orders.LastOrDefault();
            foreach (string key in HttpContext.Session.Keys)
            {
                Product each_stuff = HttpContext.Session.GetObjectFromJson<Product>(key);
                OrderProduct new_op = new OrderProduct
                {
                    Product = each_stuff,
                    ProductId = Convert.ToInt32(key),
                    Order = latest_order,
                    OrderId = latest_order.OrderId,
                    Ordered_date = DateTime.Now,
                    QtyOrdered = 1,
                };
                // THIRD: Decrease the product qty in inventory
                Product stuff_to_modify_qty =  _context.Products.SingleOrDefault(product => product.ProductId == Convert.ToInt32(key));
                stuff_to_modify_qty.Qty -- ;
                _context.SaveChanges();

                //open DB again to save new OP
                _context.OrderProducts.Add(new_op);
                _context.SaveChanges();
            }
            int shopping_cart_num = HttpContext.Session.Keys.Count();
            //FOURTH: Clear the shopping cart session for next rounds of selecting
            TempData["confirm_order"] = $"You ordered {shopping_cart_num} stuff!";
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Order");
        }



        // +++++++++++++++++ region helpers +++++++++++++++++++
        private void HoldStuffBeforeConfirmOrder(int productId)
        {
            Product stuff_to_add = _context.Products.SingleOrDefault(p => p.ProductId == productId); 
            HttpContext.Session.SetObjectAsJson(stuff_to_add.ProductId.ToString(), stuff_to_add);
        }
    }
}
