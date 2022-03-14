using Microsoft.AspNetCore.Mvc;
using AmazooApp.Utility;
using Microsoft.AspNetCore.Authorization;
using AmazooApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AmazooApp.Controllers
{

    public class BillingController : Controller
    {
        UserManager<ApplicationUser> _userManager;

        public BillingController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> BillingAsync()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentUser = currentUser;

            return View();
        }
    }
}
