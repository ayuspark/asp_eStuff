using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
         
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                 ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
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
                    int new_user_id = _context.Users.SingleOrDefault(u => u.Email == vm.Email).Id;
                    Customer new_customer = new Customer
                    {
                        ApplicationUserEmail = vm.Email,
                        ApplicationUserId = new_user_id,
                        Created = DateTime.Now,
                    };

                    _context.Customers.Add(new_customer);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //ModelState.AddModelError("", "Login attempt failed.");
                    ViewBag.error = "Uh oh, login attempt failed.";
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
    }
}
