using AmazooApp.Data;
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
        private readonly AmazooAppDbContext _db;
        UserManager<ApplicationUser> _userManager;

        // This is a constructor of the AccountController class.
        public AccountController(AmazooAppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // This action outputs a View to the user holding its personal information.
        public async Task<IActionResult> MyAccountAsync()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.CurrentUser = currentUser;

            return View();
        }

        // This action outputs a View to the administrator containing all accounts
        // registered in the system.
        public IActionResult AdminUserList()
        {
            var allUsers = _db.Users;
            

            return View(allUsers);
        }
    }
}
