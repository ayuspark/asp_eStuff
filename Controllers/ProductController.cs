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
            return View("Index");
        }

        [Authorize]
        [HttpPost]
        [Route("/stuff/add")]
        public IActionResult AddStuff(ProductViewModel vm)
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
                if (ModelState.IsValid)
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
                // TODO: get rid of this, using AJAX.
                List<Product> all_stuff = _context.Products.OrderByDescending(s => s.Created_date_by_seller).ToList();
                ViewBag.all_stuff = all_stuff;
                return View("Index", vm);
            }
        }


        [HttpGet]
        [Route("/stuff/confirm_order")]
        public IActionResult CreateOrder()
        {
            var task = _userManager.GetUserAsync(User).Result;
            Order new_order = new Order
            {
                CustomerId = task.Id,
                Created = DateTime.Now,
            };
            new_order.OrderProducts.Add();
            _context.Orders.Add(new_order);
            _context.SaveChanges();
            return RedirectToAction("Index", "Order");
        }

       
    }
}
