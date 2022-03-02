using AmazooApp.Data;
using AmazooApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AmazooAppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AmazooAppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index(String searchEntry, String b1, String b2, String b3, String b4)
        {
            //Console.WriteLine("HELLO");
            //Console.WriteLine(b1==null);
            //Console.WriteLine(b2 == null);
            //Console.WriteLine(b3 == null);
            //Console.WriteLine(b4 == null);

            ViewBag.SearchEntry = searchEntry;

            var products = from p in _db.Products
                           select p;
            if (!String.IsNullOrEmpty(searchEntry))
            {
                products = products.Where(p => p.ProductName.Contains(searchEntry) || p.Brand.Contains(searchEntry));
            }
            if (!(b1 == null)){
                Console.WriteLine(b1);
                products = products.Where(p => p.Category.Contains(b1));
            }
            if (!(b2 == null))
            {
                Console.WriteLine(b2);
                products = products.Where(p => p.Category.Contains(b2));
            }
            if (!(b3 == null))
            {
                Console.WriteLine(b3);
                products = products.Where(p => p.Category.Contains(b3));
            }
            if (!(b4 == null))
            {
                Console.WriteLine(b4);
                products = products.Where(p => p.QuantityInStock>0);
            }

            return View(await products.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
