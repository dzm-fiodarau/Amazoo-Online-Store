using Microsoft.AspNetCore.Mvc;

namespace AmazooApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
