using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using asp_ecommerce.Models;

namespace asp_ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<Product> all_stuff = await _context.Products
                                                    .OrderBy(s => s.Qty)
                                                    .ToListAsync();
            all_stuff.Where(s => s.ApplicationUserEmail != User.Identity.Name).ToList();
            if (all_stuff.Count() > 4)
            {
                all_stuff = all_stuff.GetRange(0, 4);
            }
            return View("Index", all_stuff);
        }
    }
}
