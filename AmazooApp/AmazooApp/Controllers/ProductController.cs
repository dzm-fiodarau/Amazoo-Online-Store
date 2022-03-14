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
        private IHostingEnvironment _environment;

        public ProductController(AmazooAppDbContext db, IHostingEnvironment environment)
        {
            _db = db;
            _environment = environment;
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
        public IActionResult AddProduct(Product obj, IFormFile postedFile)
        {
            var objectToSave = obj;
            if (postedFile != null)
            {
                string fileName = System.IO.Path.GetFileName(postedFile.FileName);

                string path = Path.Combine(this._environment.WebRootPath, "media");

                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(fileStream);
                }
                objectToSave.Image = "/media/" + fileName;
            }
            _db.Products.Add(objectToSave);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



        //GET-Delete

        public IActionResult Delete(int? id)
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

        //GET-Edit

        public IActionResult Edit(int? id)
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

        //POST-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj, IFormFile postedFile)
        {
            if (ModelState.IsValid)
            {
                var objectToSave = obj;

                if (postedFile != null)
                {
                    string fileName = System.IO.Path.GetFileName(postedFile.FileName);

                    string path = Path.Combine(this._environment.WebRootPath, "media");

                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(fileStream);
                    }
                    objectToSave.Image = "/media/" + fileName;
                }
                _db.Products.Update(objectToSave);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    
    
    }
    }
