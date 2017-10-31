using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using asp_ecommerce.Models;
using asp_ecommerce.ViewModels;


namespace asp_ecommerce.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("/stuff/all")]
        public IActionResult Index()
        {
            List<Product> all_stuff = _context.Products.OrderBy(s => s.Name).ToList();
            ViewBag.all_stuff = all_stuff;
            return View("Index");
        }

        [Authorize]
        [HttpPost]
        [Route("/stuff/add")]
        public IActionResult AddStuff(ProductViewModel vm)
        {
            if (_context.Products.Any(p => p.Name.ToLower().Contains(vm.Name.ToLower()) && p.ApplicationUserEmail == User.Identity.Name))
            {
                ModelState.AddModelError("", "You already have similar stuff to sell, please add something new.");
                return PartialView("_ProductPartial", vm);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Product new_stuff = new Product
                    {
                        Name = vm.Name,
                        Url = vm.URL,
                        Desc = vm.Desc,
                        Qty = vm.Qty,
                        ApplicationUserEmail = User.Identity.Name
                    };
                    _context.Products.Add(new_stuff);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                return PartialView("_ProductPartial", vm);
            }
        }
    }
}
