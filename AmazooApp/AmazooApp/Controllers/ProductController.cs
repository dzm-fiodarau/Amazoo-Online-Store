using AmazooApp.Data;
using AmazooApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AmazooApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AmazooAppDbContext _db;
        [Obsolete]
        private readonly IHostingEnvironment _environment;

        // This is a constructor of the ProductController class.
        [Obsolete]
        public ProductController(AmazooAppDbContext db, IHostingEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        // This action outputs a View holding all products stored in the system.
        public IActionResult Index()
        {
            var productList = _db.Products;
            return View(productList);
        }

        // GET-Add.
        // This action outputs a View from which you can add a product to the system.
        public IActionResult AddProduct()
        {
            return View();
        }

        // POST-Add.
        // This action adds the new product to the system and outputs the Products page View.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult AddProduct(Product obj, IFormFile postedFile)
        {
            var productToSave = obj;
            if (postedFile != null)
            {
                string fileName = System.IO.Path.GetFileName(postedFile.FileName);
                string path = Path.Combine(this._environment.WebRootPath, "media");

                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(fileStream);
                }
                productToSave.Image = "/media/" + fileName;
            }

            _db.Products.Add(productToSave);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }



        // GET-Delete.
        // This action outputs a View from which you can delete a specific product from the system.
        public IActionResult Delete(int? id)
        {
            var product = GetProductFromId(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST-Delete.
        // This action deletes the specific product from the system and outputs the Products page View.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var product = GetProductFromId(id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET-Edit.
        // This action outputs a View from which you can edit a specific product in the system.
        public IActionResult Edit(int? id)
        {
            var product = GetProductFromId(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST-Edit.
        // This action edits the specific product in the system and outputs the Products page View.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult Edit(Product obj, IFormFile postedFile)
        {
            if (ModelState.IsValid)
            {
                var productToSave = obj;

                if (postedFile != null)
                {
                    string fileName = System.IO.Path.GetFileName(postedFile.FileName);
                    string path = Path.Combine(this._environment.WebRootPath, "media");

                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(fileStream);
                    }
                    productToSave.Image = "/media/" + fileName;
                }

                _db.Products.Update(productToSave);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // This method verifies the product id is valid and returns the
        // product associated with it.
        private Product GetProductFromId(int? id)
        {
            if ((id == null) || (id == 0))
            {
                return null;
            }

            var product = _db.Products.Find(id);

            return product;
        }
    }
}
