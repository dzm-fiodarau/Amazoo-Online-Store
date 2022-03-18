using AmazooApp.Data;
using AmazooApp.Models;
using Microsoft.AspNetCore.Http;
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
        public IEnumerable<Product>  products;
        public HomeController(ILogger<HomeController> logger, AmazooAppDbContext db)
        {
            _logger = logger;
            _db = db;
            products = from p in _db.Products
                       select p;
        }

        public async Task<IActionResult> Index(String searchEntry)
        {
            //Console.WriteLine("HELLO");


            ViewBag.SearchEntry = searchEntry;

            var products = from p in _db.Products
                           select p;
            if (!String.IsNullOrEmpty(searchEntry))
            {
                products = products.Where(p => p.ProductName.Contains(searchEntry) || p.Brand.Contains(searchEntry));
            }

            return View(await products.ToListAsync());
        }
        
        public IActionResult ProductPage(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        public IActionResult Filter(IFormCollection formCollection)
        {
            var actionsChckbox = formCollection.TryGetValue("chckBox", out var filterValues);
            var actionsRadio = formCollection.TryGetValue("type", out var filterRadioValues);

            //Filter the checkboxes
            var selected = products.Where(p => filterValues.Any(chck => chck.Equals(p.Category) || chck.Equals(p.Brand)));

            if(filterRadioValues.Count > 0)
            {
                var selectedx = selected.Where(p => filterRadioValues.Any(chk => chk.Contains("outStck") && p.QuantityInStock <= 0));
                var selectedy = selected.Where(p => filterRadioValues.Any(chk => chk.Contains("inStck") && p.QuantityInStock > 0));
               selected = selectedx.Concat(selectedy);

            }
            IEnumerable < Product > checkedList = filterValues.Count == 0 ? products : selected;
            return View("Index", checkedList);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}