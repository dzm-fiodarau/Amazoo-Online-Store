using AmazooApp.Data;
using AmazooApp.Models;
using AmazooApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AmazooApp.Controllers
{
    public class LoginController : Controller
    {

        private readonly AmazooAppDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;

        public LoginController(AmazooAppDbContext db, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager; ;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Check()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Check(LoginViewModel userProfileModel)
        {

            if (ModelState.IsValid)
            {
                //TODO Add the remember checkbox
                var result = await _signInManager.PasswordSignInAsync(userProfileModel.Email, userProfileModel.Password, true,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Login", "Invalid Login Attempt");
            }
            return View(userProfileModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        private ApplicationUser createUser()
        {
            return new ApplicationUser
            {
                UserName = "John",
                Email = "a@a",
                FirstName = "John",
                LastName = "Doe",
                PasswordHash = "123"
            };
        }
    }
}
