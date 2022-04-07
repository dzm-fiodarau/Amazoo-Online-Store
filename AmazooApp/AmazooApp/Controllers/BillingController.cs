using Microsoft.AspNetCore.Mvc;
using AmazooApp.Utility;
using Microsoft.AspNetCore.Authorization;
using AmazooApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using AmazooApp.Data;
using System.Collections;
using System.Linq;

namespace AmazooApp.Controllers
{

    public class BillingController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AmazooAppDbContext _db;

        // This is a constructor of the BillingController class.
        public BillingController(UserManager<ApplicationUser> userManager, AmazooAppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        // This action outputs a View where the user can enter his payment information.
        public async Task<IActionResult> BillingAsync(string error)
        {
            if(error == null)
            {
                ViewBag.Warning = "";
            }
            else
            {
                ViewBag.Warning = error;
            }

            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentUser = currentUser;

            return View();
        }

        // POST-Summary.
        // This action outputs a View holding the summary of the order to checkout.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Summary(string firstName, string lastName, string email, string address, string city, string province, string zipcode,
            string cc_name, string cc_number, string cc_expiration, string cc_cvv, string productIds, string productQnts, string cost)
        {
            if(productIds == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!VerifyInfo(firstName, lastName, email, address, city, province, zipcode,cc_name, cc_number, cc_expiration, cc_cvv))
            {
                return RedirectToAction("Billing", new { error = "*One or more errors detected. Please enter your information again." });
            }

            ViewBag.FirstName = firstName;
            ViewBag.LastName = lastName;
            ViewBag.Email = email;
            ViewBag.Address = address;
            ViewBag.City = city;
            ViewBag.Province = province;
            ViewBag.Zipcode = zipcode;
            ViewBag.CCName = cc_name;
            ViewBag.CCNumber = cc_number;
            ViewBag.CCExpiration = cc_expiration;
            ViewBag.CVV = cc_cvv;

            string[] itemIds = productIds.Split(";");
            string[] itemQnts = productQnts.Split(";");

            List<Product> productLst = new();
            Hashtable idQnt = new();

            int counter = 0;
            foreach(string id in itemIds)
            {
                if (!(id == null) && !(id==""))
                { 
                    int intId = Int32.Parse(id);
                    
                    productLst.Add(_db.Products.Find(intId));
                    idQnt.Add(intId, itemQnts[counter]);
                }
                counter++;
            }

            ViewBag.IdQnt = idQnt;
            ViewBag.Cost = cost;

            return View(productLst);
        }

        // POST-OrderConfirmation.
        // This action stores a new order in the system and outputs a page with order confirmation.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderConfirmationAsync(string productIds, string productQnts, string cost)
        {
            if (productIds == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            DateTime today = DateTime.Today;
            DateTime deliveryDate = DateTime.Today.AddDays(7);

            
            Order newOrder = new() { Customer = currentUser.Id, Status = "In Process" , DeliveryDate = deliveryDate, CreationDate = today, 
                TotalPaid = float.Parse(cost)};

            _db.Orders.Add(newOrder);
            _db.SaveChanges();

            ViewBag.DeliveryDate = deliveryDate;

            var savedOrder = from o in _db.Orders
                             where (o.Customer == newOrder.Customer && o.Status == newOrder.Status && o.CreationDate == newOrder.CreationDate &&
                             o.DeliveryDate == newOrder.DeliveryDate && o.TotalPaid == newOrder.TotalPaid)
                             select o;

            Order myNewOrder = new();

            int counter = 0;
            foreach(var savOrd in savedOrder)
            {
                if(savOrd != null) {
                    if(counter == 0)
                    {
                        myNewOrder = savOrd;
                    }
                    else
                    {
                        _db.Orders.Remove(savOrd);
                    }
                    counter++;
                }
            }
            _db.SaveChanges();

            string[] itemIds = productIds.Split(";");
            string[] itemQnts = productQnts.Split(";");
            counter = 0;
            foreach (string id in itemIds)
            {
                if (!(id == null) && !(id == ""))
                {
                    int intId = Int32.Parse(id);
                    int intQnt = Int32.Parse(itemQnts[counter]);

                    OrderProduct orderProduct = new() {OrderId = myNewOrder.Id, ProductId = intId, Quantity = intQnt };
                    _db.OrderProducts.Add(orderProduct);
                }
                counter++;
            }

            _db.SaveChanges();

            counter = 0;
            foreach (string id in itemIds)
            {
                if (!(id == null) && !(id == ""))
                {
                    int intId = Int32.Parse(id);
                    int intQnt = Int32.Parse(itemQnts[counter]);

                    var savedOrderProduct = from oP in _db.OrderProducts
                                            where (oP.OrderId == myNewOrder.Id && oP.ProductId == intId && oP.Quantity == intQnt)
                                            select oP;

                    int counter2 = 0;
                    foreach (OrderProduct savOrd in savedOrderProduct)
                    {
                        if (savOrd != null)
                        {
                            if (!(counter2 == 0))
                            {
                                _db.OrderProducts.Remove(savOrd);
                            }
                            counter2++;
                        }
                    }

                    var product = _db.Products.Find(intId);
                    product.QuantityInStock -= intQnt;

                    _db.Products.Update(product);
                    _db.SaveChanges();
                }
                counter++;
            }

            return View();
        }

        // This method verifies information entered into the billing page form and returns true if everything information is valid
        // and false if some information is not valid.
        private static bool VerifyInfo(string firstName, string lastName, string email, string address, string city, string province, string zipcode,
            string ccName, string ccNumber, string ccExpiration, string ccCvv)
        {
            if ((firstName == null) || (lastName == null) || (email == null) || (address == null) || (city == null) || 
                (province == null) || (zipcode == null))
                return false;

            if ((!Regex.IsMatch(firstName, @"^[A-Za-z]+$")) || (!Regex.IsMatch(lastName, @"^[A-Za-z]+$")) || (!Regex.IsMatch(province, @"^[A-Za-z]+$")))
                return false;
            if (!Regex.IsMatch(ccCvv, @"^\d{3,4}$"))
                return false;
            if (!Regex.IsMatch(ccNumber, @"^\d{4} \d{4} \d{4} \d{4}$"))
                return false;
            if (!Regex.IsMatch(ccExpiration, @"^\d{6}$"))
                return false;
            if (!Regex.IsMatch(ccName, @"^[A-Za-z ]+$"))
                return false;
            if (!Regex.IsMatch(zipcode, @"^[A-Za-z0-9 ]{6,7}$"))
                return false;
            if (!Regex.IsMatch(city, @"^\D+$"))
                return false;

            return true;
        }
    }
}
