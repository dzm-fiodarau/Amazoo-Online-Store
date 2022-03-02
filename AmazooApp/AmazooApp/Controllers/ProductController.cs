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

        //GET-Add
        public IActionResult AddProduct()
        {
            return View();
        }

        //POST-Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product obj)
        {
            _db.Products.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



        //GET-Delete

        public IActionResult Delete(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            var obj = _db.Products.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            
            return View(obj);
        }

        //POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Products.Find(id);
         if (obj == null)
            {
                return NotFound();
            }

            _db.Products.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
