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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // This is a constructor of the LoginController class.
        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // This action outputs a View where a user can login into his account.
        public IActionResult Login()
        {
            // In case the user is already logged in, redirects home.
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // This action verifies that the entered information is valid for an account, logs the user in and redirects
        // to home page if successfully logged in.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Check(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //in case the user is already logged in 
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }

                ApplicationUser user  =  _userManager.FindByEmailAsync(model.Email).GetAwaiter().GetResult();

                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email or Password! Please Try again");
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View("Login", model);
        }

        // This action logs the current user out and outputs the home page View.
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

     

    }
}
