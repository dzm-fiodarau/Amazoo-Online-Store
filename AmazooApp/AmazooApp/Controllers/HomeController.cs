using AmazooApp.Data;
using AmazooApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        // This is a constructor of the HomeController class.
        public HomeController(ILogger<HomeController> logger, AmazooAppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
            products = from p in _db.Products
                       select p;
        }

        // This action outputs a View of the home page.
        public async Task<IActionResult> Index(String searchEntry)
        {
            ViewBag.SearchEntry = searchEntry;

            var products = from p in _db.Products
                           select p;

            if (!String.IsNullOrEmpty(searchEntry))
            {
                products = products.Where(p => (p.ProductName.Contains(searchEntry)) || (p.Brand.Contains(searchEntry)));
            }

            return View(await products.ToListAsync());
        }

        // This action outputs a View holding the details about one particular product.
        public IActionResult ProductPage(int? id)
        {
            if ((id == null) || (id == 0))
            {
                return NotFound();
            }

            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Product = product;

            var reviews = from currentProductReview in _db.Reviews
                                          where currentProductReview.ProductId == id
                                          select currentProductReview;
            if (reviews == null)
            {
                return NotFound();
            }

            float average = 0;
            foreach(Review review in reviews)
            {
                average += review.Rating;
            }

            average /= reviews.Count();
            average = (float)Math.Round(average * 100f) / 100f;
            ViewBag.AverageRating = average;

            if(reviews.Count() > 6)
            {
                reviews = reviews.Take<Review>(6);
            }

            ViewBag.Id = id;

            return View(reviews);
        }

        // This action outputs a View holding all reviews of one particular product.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AllReviews(int? id)
        {
            if ((id == null) || (id == 0))
            {
                return NotFound();
            }

            var product = _db.Products.Find(id);
            ViewBag.Product = product;

            var reviews = from currentProductReview in _db.Reviews
                          where currentProductReview.ProductId == id
                          select currentProductReview;
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // This action outputs a View where a user can enter a new review.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveReviewAsync(int? id)
        {
            if ((id == null) || (id == 0))
            {
                return NotFound();
            }

            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if(currentUser == null)
            {
                return RedirectToAction("ProductPage", new { id = id});
            }

            var hasPurchased = false;

            var pastDeliveredOrders = from order in _db.Orders
                                      where order.Customer.Equals(currentUser.Id) && order.Status.Equals("Delivered")
                                      select order;

            List<Order> pastDeliveredOrdersList = new();
            foreach(Order order in pastDeliveredOrders)
            {
                pastDeliveredOrdersList.Add(order);
            }

            foreach(Order order in pastDeliveredOrdersList)
            {
                var orderProducts = from oP in _db.OrderProducts
                                    where oP.OrderId == order.Id && oP.ProductId == id
                                    select oP;

                if (orderProducts.Any())
                {
                    hasPurchased = true;
                    break;
                }
            }

            ViewBag.HasPurchased = hasPurchased;
            ViewBag.Id = id;

            return View();
        }

        // This action outputs a View where a user can enter a new review.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReviewStoredAsync(int? id, string description, string rating)
        {
            if ((id == null) || (id == 0) || (description == null) || (description.Equals("")) || (rating == null) || (rating.Equals("")))
                return RedirectToAction("ProductPage", new { id });

            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            Review reviewToStore = new() { Customer = currentUser.FirstName + " " + currentUser.LastName, Description = description, 
                ProductId = (int)id, Rating = Int32.Parse(rating)
            };

            _db.Reviews.Add(reviewToStore);
            _db.SaveChanges();

            return View();
        }

        // This action filters the products on the home page View.
        public IActionResult Filter(IFormCollection formCollection)
        {
            var actionsChckbox = formCollection.TryGetValue("chckBox", out var filterValues);
            var actionsRadio = formCollection.TryGetValue("type", out var filterRadioValues);

            var selected = products.Where(p => filterValues.Any(chck => (chck.Equals(p.Category)) || (chck.Equals(p.Brand, StringComparison.Ordinal))));

            if(filterRadioValues.Count > 0)
            {
                var selectedx = selected.Where(p => (filterRadioValues.Any(chk => chk.Contains("outStck")) && (p.QuantityInStock <= 0)));
                var selectedy = selected.Where(p => (filterRadioValues.Any(chk => chk.Contains("inStck")) && (p.QuantityInStock > 0)));
                selected = selectedx.Concat(selectedy);
            }

            var checkedList = filterValues.Count == 0 ? products : selected;

            return View("Index", checkedList);
        }

        // This action outputs a View where the error is shown.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}