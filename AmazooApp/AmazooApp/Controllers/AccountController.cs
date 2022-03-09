using AmazooApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> MyAccountAsync()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.CurrentUser = currentUser;

            return View();
        }
    }
}
