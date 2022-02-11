using AmazooApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace AmazooApp.Controllers
{
    public class LoginController : Controller
    {

        private readonly AmazooAppDbContext _db;
        public IActionResult Login()
        {
            return View();
        }

        public LoginController(AmazooAppDbContext db)
        {
            _db = db;
        }
        public IActionResult Check()
        {
            return Ok("WELCOME Jane Doe");
        }
    }
}
