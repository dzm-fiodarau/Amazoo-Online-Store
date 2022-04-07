using AmazooApp.Data;
using AmazooApp.Models;
using AmazooApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AmazooApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // This is a constructor of the RegisterController class.
        public RegisterController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // This action is the entry point for the registartion page.
        // When a user reaches the registartion page, this method will check if the roles 
        // for the admin and customer exists and will ceate them if necessary.
        public async Task<IActionResult> Register()
        {
            // In case the user is already logged in, redirects to home.
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!_roleManager.RoleExistsAsync(Utility.RoleHelper.Customer).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Utility.RoleHelper.Customer));
                await _roleManager.CreateAsync(new IdentityRole(Utility.RoleHelper.Admin));
            }

            return View();
        }

        // This action registers the new user in the system.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser{FirstName = model.FirstName, LastName = model.LastName, Email = model.Email,
                    UserName= model.FirstName, Address=model.Address, City= model.City, Province = model.Province,
                    Zipcode = model.Zipcode};

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}
