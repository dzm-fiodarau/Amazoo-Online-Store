
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AmazooApp.Data;
using AmazooApp.Models.Checkout;
using AmazooApp.Models;
using Microsoft.AspNetCore.Identity;
using AmazooApp.Utility;

namespace AmazooApp.Controllers
{
    public class CheckoutController : Controller
    {
        //Global variables
        private readonly AmazooAppDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;



        //Constructor
        public CheckoutController(AmazooAppDbContext db, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            
        }
        
        
        public async Task<IActionResult> ShippingAddressAsync(CheckoutViewModel model)
        {
            
            //If user is loged in, get user information
            if (User.Identity.IsAuthenticated == true && model.flag == false)
            {
                model.currentUser = await _userManager.GetUserAsync(HttpContext.User);
                model.tempUser = model.currentUser;
                model.flag = true;
            }
            //If user is not loged in, create guest user
            if (User.Identity.IsAuthenticated == false && model.flag == false)
            {   //If not, create a guest user
                model.currentUser = new ApplicationUser
                {
                    FirstName = "Guest ",
                    LastName = "User ",

                    Email = "",
                    UserName = "",
                    Address = "",
                    City = "",
                    Province = "",
                    Zipcode = ""
                };
                model.flag = true;
                model.tempUser = model.currentUser;

            }

            //Read value from forms
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                City = model.City,
                Province = model.Province,
                Zipcode = model.Zipcode

            };

            //If chechbox is checked, we update the shipping info from the forms
            if (model.chkAddon != null)
                 model.tempUser = user;

            ViewBag.currentUser = model.currentUser;
            ViewBag.tempUser = model.tempUser;

            return View("ShippingAddress");

        }



        public async Task<IActionResult> BillingInfo(CheckoutViewModel model)
        {

            var card = new CreditCard
            {
                name = model.name,
                cardNumber = model.cardNumber,
                expMonth = model.expMonth,
                expYear = model.expYear,
                ccv = model.ccv,
            };
            model.cc = card;

            return View("BillingInfo");

        }
        public ActionResult myFunction()
        {
            return View("ShippingAddress");

        }
    }
}
