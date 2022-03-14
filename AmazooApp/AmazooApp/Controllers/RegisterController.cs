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
        private readonly AmazooAppDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public RegisterController(AmazooAppDbContext db, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
           _roleManager = roleManager;
        }

        /**
         * This Method Is the entry point for the registartion page
         * When a user reaches the registartion page, this method will check if the roles 
         * for the admin and customer exists and will ceate them if necessary.
         * 
         * **/
        public async Task<IActionResult> Register()
        {
            //in case the user is already logged in 
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!_roleManager.RoleExistsAsync(Utility.RoleHelper.Customer).GetAwaiter().GetResult())
            {
               await  _roleManager.CreateAsync(new IdentityRole(Utility.RoleHelper.Customer));
                await _roleManager.CreateAsync(new IdentityRole(Utility.RoleHelper.Admin));

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName= model.FirstName,
                    Address=model.Address,
                    City= model.City,
                    Province = model.Province,
                    Zipcode = model.Zipcode

                };

                var result = await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }


}
