using Microsoft.AspNetCore.Mvc;
using AmazooApp.Utility;
using Microsoft.AspNetCore.Authorization;

namespace AmazooApp.Controllers
{
    [Authorize]
    public class BillingController : Controller
    {
       [Authorize]
        public IActionResult Billing()
        {
            return View();
        }
    }
}
