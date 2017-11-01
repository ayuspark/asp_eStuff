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

            List<Product> stuff_to_select = _context.Products.Where(s => s.ApplicationUserName != User.Identity.Name)
                                                    .OrderBy(s => s.Name)
                                                    .ToList();

            ViewBag.my_order = my_order;
            ViewBag.stuff_to_select = stuff_to_select;
            return View("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("order/stuff/add")]
        public IActionResult AddStuffToOrder(SelectOrderViewModel vm)
        {
            Console.WriteLine("^^^^^^^^^orderid " + vm.OrderId);
            Console.WriteLine("********OPId " + vm.OrderProductId);
            Console.WriteLine("$$$$$$$$$ProId " + vm.ProductId);
            Console.WriteLine("^^^^^^^^qty " + vm.ProductQty);
            int left_qty = _context.Products.SingleOrDefault(p => p.ProductId == vm.ProductId).ProductId;
            if (vm.ProductQty > left_qty)
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

                    //TODO: wrong model
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
