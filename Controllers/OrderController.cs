using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using asp_ecommerce.Models;
using asp_ecommerce.ViewModels;

namespace asp_ecommerce.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("orders/all")]
        public IActionResult Index()
        {
            string email_to_get_my_customer_id = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name).Email;
            int my_customerId = _context.Customers.SingleOrDefault(c => c.ApplicationUserEmail == email_to_get_my_customer_id).CustomerId;

            List<Order> my_order = _context.Orders.Where(o => o.CustomerId == my_customerId)
                                           .Include(o => o.OrderProducts)
                                           .ThenInclude(op => op.Product)
                                           .ToList();
            
            List<List<Product>> select_options = new List<List<Product>>();
            foreach (var order in my_order)
            {
                List<Product> stuff_to_select = _context.Products.Where(s => s.ApplicationUserName != User.Identity.Name)
                                                    .OrderBy(s => s.Name)
                                                    .ToList();
                foreach (var op in order.OrderProducts)
                {
                    stuff_to_select.Remove(stuff_to_select.SingleOrDefault(s => s.ProductId == op.ProductId));
                }
                select_options.Add(stuff_to_select);
            }

            ViewBag.my_order = my_order;
            ViewBag.select_options = select_options;
            ViewBag.removed = TempData["removed"];
            ViewBag.error = TempData["error"];
            return View("Index");
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("order/stuff/add")]
        public IActionResult AddStuffToOrder(SelectOrderViewModel vm)
        {
            if (ModelState.IsValid)
            {
                int left_qty = _context.Products.SingleOrDefault(p => p.ProductId == vm.ProductId).ProductId;
                if (vm.ProductQty > left_qty)
                {
                    TempData["error"] = "There is not enough stuff for the quantity you made.";
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    Product stuff_to_add = _context.Products.SingleOrDefault(p => p.ProductId == vm.ProductId);
                    stuff_to_add.Qty -= (int)vm.ProductQty;
                    _context.SaveChanges();

                    Order curr_order = _context.Orders.SingleOrDefault(p => p.OrderId == vm.OrderId);
                    OrderProduct new_op = new OrderProduct
                    {
                        ProductId = stuff_to_add.ProductId,
                        Product = stuff_to_add,
                        OrderId = curr_order.OrderId,
                        Order = curr_order,
                        Ordered_date = DateTime.Now,
                        QtyOrdered = vm.ProductQty,
                    };
                    curr_order.OrderProducts.Add(new_op);
                    _context.SaveChanges();
                }
            }
            TempData["error"] = "Please select stuff and its quantity.";
            foreach(var er in ModelState.Values)
            {
                foreach (var e in er.Errors)
                {
                    Console.WriteLine("&&&&&& error" +  e.ErrorMessage);

                }
            }
            return RedirectToAction("Index", "Order");
        }


        [HttpGet]
        [Route("order/stuff/delete/{orderProductId}/{productId}")]
        public IActionResult DelectStuffFromOrder(int orderProductId, int productId)
        {
            //TODO: Delete order when no stuff on the order!
            OrderProduct curr = _context.OrderProducts.Include(op => op.Product)
                                        .SingleOrDefault(op => op.OrderProductId == orderProductId && op.ProductId == productId);
            int qty_to_add_back_to_inventory = curr.QtyOrdered;

            TempData["removed"] = $"You removed stuff: {qty_to_add_back_to_inventory} {curr.Product.Name}.";
            _context.Remove(curr);
            _context.SaveChanges();

            Product back_to_inventory = _context.Products.SingleOrDefault(p => p.ProductId == productId);
            back_to_inventory.Qty += qty_to_add_back_to_inventory;
            _context.SaveChanges();
           
            return RedirectToAction("Index", "Order");
        }

    }
}
