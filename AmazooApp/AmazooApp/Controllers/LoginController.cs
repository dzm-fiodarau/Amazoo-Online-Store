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

        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;

        public LoginController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager; ;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            //in case the user is already logged in 
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Check()
        {
            return View();
        }



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
            return View("Login",model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

     

    }
}
