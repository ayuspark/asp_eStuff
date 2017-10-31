using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using asp_ecommerce.Models;
using asp_ecommerce.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asp_ecommerce.Controllers
{
    [Authorize]
    public class RestoReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RestoReviewController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/restoreviews")]
        public IActionResult Index()
        {
            List<Resto> reviews_index = _context.Restos.Include(resto => resto.Reviews)
                                                .OrderBy(resto => resto.Name)
                                                .ToList();
            ViewBag.reviews_index = reviews_index;
            return View();
        }

        [HttpGet]
        [Route("/restoreviews/add")]
        public IActionResult Review()
        {
            return View("Review");
        }

        [HttpPost]
        [Route("/restoreviews/add")]
        public IActionResult Review(RestoReviewViewModel vm)
        {
            if (vm.DateVisited > DateTime.Today)
            {
                ModelState.AddModelError("DateVisited", "Date can't be greater than today.");
                return View(vm);
            }
            else
            {
                Resto resto_to_review = new Resto();
                if (ModelState.IsValid)
                {
                    resto_to_review = _context.Restos.SingleOrDefault(resto => resto.Name.ToLower() == vm.RestoName.ToLower());
                    if (resto_to_review == null)
                    {
                        Resto new_resto = new Resto
                        {
                            Name = vm.RestoName.ToLower(),
                        };
                        _context.Restos.Add(new_resto);
                        _context.SaveChanges();
                    }
                    int resto_to_review_id = _context.Restos.SingleOrDefault(resto => resto.Name == vm.RestoName).RestoId;

                    RestoReview new_review = new RestoReview
                    {
                        RestoId = resto_to_review_id,
                        ReviewContent = vm.ReviewContent,
                        Created = vm.DateVisited,
                        ApplicationUserEmail = User.Identity.Name,
                    };
                    _context.Add(new_review);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "RestoReview");
                }
                else
                {
                    ModelState.AddModelError("", "Something is wrong with your review.");
                }
                return View(vm);
            }
        }
    }
}
