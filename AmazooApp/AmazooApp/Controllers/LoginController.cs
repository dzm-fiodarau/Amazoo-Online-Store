using AmazooApp.Data;
using AmazooApp.Models;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Check(UserProfile obj)
        {
            
            
            return Ok("Welcome " + obj.email);
        }
    }
}
