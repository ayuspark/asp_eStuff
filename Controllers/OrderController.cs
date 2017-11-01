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
                                           .Include(o => o.Products)
                                           .ToList();
            //my_order[0].Products.GetEnumerator();

            List<Product> stuff_to_select = _context.Products.Where(s => s.ApplicationUserEmail != email_to_get_my_customer_id)
                                                    .OrderBy(s => s.Name)
                                                    .ToList();

            ViewBag.my_order = my_order;
            ViewBag.stuff_to_select = stuff_to_select;
            return View("Index");
        }

        [HttpPost]
        [Route("order/stuff/add")]
        public IActionResult AddStuffToOrder(SelectOrderViewModel vm)
        {
            int left_qty = _context.Products.SingleOrDefault(p => p.ProductId == vm.ProductId).ProductId;
            if (vm.ProductQty < left_qty)
            {
                ModelState.AddModelError("", "There is not enough stuff for the quantity you made.");
                return RedirectToAction("Index", "Order");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Product stuff_to_add = _context.Products.SingleOrDefault(p => p.ProductId == vm.ProductId);
                    stuff_to_add.Qty -= (int)vm.ProductQty;
                    _context.SaveChanges();

                    Order curr_order = _context.Orders.SingleOrDefault(p => p.OrderId == vm.OrderId);
                    curr_order.Products.Add(stuff_to_add);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Order");
            }
        }

        //[HttpGet]
        //[Route("order/stuff/delete/{id}")]
        //public IActionResult DelectStuffFromOrder(int id)
        //{
            
        //}
    }
}
