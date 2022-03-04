using Microsoft.AspNetCore.Mvc;
using AmazooApp.Utility;


namespace AmazooApp.Controllers
{
    public class BillingController : Controller
    {
       
        public IActionResult Billing()
        {
            return View();
        }
    }
}
