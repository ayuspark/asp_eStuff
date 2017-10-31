using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using asp_ecommerce.Models;


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
        
        public IActionResult Index()
        {
            int my_customerId = _context.Customers.SingleOrDefault(c => c.ApplicationUserEmail == User.Identity.Name).CustomerId;
            List<Order> my_order = _context.Orders.Include(o => o.CustomerId == my_customerId).ToList();
            ViewBag.my_order = my_order;
            return View("Index");
        }
    }
}
