using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using asp_ecommerce.Models;
using asp_ecommerce.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asp_ecommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
         
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                 ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("account/register")]
        //public IActionResult Register()
        //{
        //    return View("Index");
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("account/register")]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FName = vm.FName,
                    LName = vm.LName,
                    Email = vm.Email,
                    UserName = vm.UserName,
                    Birthday = vm.Birthday,
                };

                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);

                    // Put new user to Customer Table.
                    var task = GetCurrentUserId();
                    Customer new_customer = new Customer
                    {
                        ApplicationUserEmail = vm.Email,
                        ApplicationUserId = task.Id,
                        Created = DateTime.Now,
                    };
                    Console.WriteLine("cust Applcation userId " + new_customer.ApplicationUserId);
                    _context.Customers.Add(new_customer);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                
            }
            return View("Index"); 
        }

        //[HttpGet]
        //[Route("account/login")]
        //[AllowAnonymous]
        //public IActionResult Login()
        //{
        //    return View("Index");
        //}

        [HttpPost]
        [Route("account/Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.UsernameLogin, vm.PasswordLogin, vm.RememberMe, false);
                if (result.Succeeded)
                {
                    Console.WriteLine("************* user logged in");
                    Console.WriteLine("*************" + User.Identity.IsAuthenticated);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login attempt failed.");
                }
            }
            return View("Index");
        }

        [HttpGet]
        [Route("account/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<int> GetCurrentUserId()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Console.WriteLine("inside getuserid: " + user.Id);
            return user.Id;
        }
    }
}
