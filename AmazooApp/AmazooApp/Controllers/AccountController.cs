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

        public AccountController(AmazooAppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyAccountAsync()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.CurrentUser = currentUser;

            return View();
        }

        public IActionResult AdminUserList()
        {
            var allUsers = _db.Users;
            

            return View(allUsers);
        }
    }
}
