using AmazooApp.Data;
using AmazooApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AmazooAppDbContext _db;

        public ProductController(AmazooAppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _db.Products;
            return View(productList);
        }
    }
}
