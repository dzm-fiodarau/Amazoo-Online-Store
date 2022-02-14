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
            return Ok("WE can Login YAY!");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Check(LoginViewModel model)
        {
            //TODO ADD [Compare("Password",ErrorMessage ="Password and confirmation password not match.")]
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View("Login",model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }


    }
}
