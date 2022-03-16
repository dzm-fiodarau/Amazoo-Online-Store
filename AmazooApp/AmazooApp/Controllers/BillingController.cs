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

namespace AmazooApp.Controllers
{

    public class BillingController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        private readonly AmazooAppDbContext _db;

        private bool VerifyInfo(string firstName, string lastName, string email, string address, string city, string province, string zipcode,
            string cc_name, string cc_number, string cc_expiration, string cc_cvv)
        {
            if (firstName == null || lastName == null || email == null || address == null || city == null || province == null|| zipcode == null)
                return false;

            if (!Regex.IsMatch(firstName,@"^[A-Za-z]+$") || !Regex.IsMatch(lastName, @"^[A-Za-z]+$") || !Regex.IsMatch(province, @"^[A-Za-z]+$"))
                return false;
            if (!Regex.IsMatch(cc_cvv, @"^\d{3,4}$"))
                return false;
            if (!Regex.IsMatch(cc_number, @"^\d{4} \d{4} \d{4} \d{4}$"))
                return false;
            if (!Regex.IsMatch(cc_expiration, @"^\d{6}$"))
                return false;
            if (!Regex.IsMatch(cc_name, @"^[A-Za-z ]+$"))
                return false;
            if (!Regex.IsMatch(zipcode, @"^[A-Za-z0-9 ]{6,7}$"))
                return false;
            if (!Regex.IsMatch(city, @"^\D+$"))
                return false;

            return true;
        }

        public BillingController(UserManager<ApplicationUser> userManager, AmazooAppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> BillingAsync(string? error)
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

        //POST-Summary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Summary(string firstName, string lastName, string email, string address, string city, string province, string zipcode,
            string cc_name, string cc_number, string cc_expiration, string cc_cvv, string? productIds, string productQnts, string cost)
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

            List<Product> productLst = new List<Product>();
            Hashtable idQnt = new Hashtable();

            int counter = 0;
            foreach(var id in itemIds)
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
    }
}
