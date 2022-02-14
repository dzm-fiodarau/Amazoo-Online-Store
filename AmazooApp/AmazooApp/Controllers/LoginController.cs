using AmazooApp.Data;
using AmazooApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AmazooApp.Controllers
{
    public class LoginController : Controller
    {

        private readonly AmazooAppDbContext _db;
        UserManager<UserProfile> _userManager;
        SignInManager<UserProfile> _signInManager;

        public LoginController(AmazooAppDbContext db, UserManager<UserProfile> userManager,
        SignInManager<UserProfile> signInManager)
        {
            _db = db;
            _userManager = userManager; ;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Check(UserProfile userProfileModel)
        {

            if (ModelState.IsValid)
            {
                //TODO Add the remember checkbox
                var result = await _signInManager.PasswordSignInAsync(userProfileModel.Email, userProfileModel.Password, true,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(userProfileModel);
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
