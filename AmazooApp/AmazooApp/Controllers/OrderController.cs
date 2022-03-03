using AmazooApp.Data;
using AmazooApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly AmazooAppDbContext _db;
        UserManager<ApplicationUser> _userManager;

        public OrderController(AmazooAppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;

            //Order Status Update (Delivered or Not)
            var allInProcessOrders = from order in _db.Orders
                                     where order.Status == "In Process"
                                     select order;

            DateTime today = DateTime.Today;

            foreach(var order in allInProcessOrders)
            {
                int compared = DateTime.Compare(order.DeliveryDate, today);
                if (compared <= 0)
                {
                    order.Status = "Delivered";
                    _db.Orders.Update(order);
                }
            }
            _db.SaveChanges();
        }

        /*
         * Admin page that outputs all orders from the system to a table
         */
        public IActionResult AdminOrderList()
        {
            Hashtable idName = new Hashtable();
            var allUsers = _db.Users;
            foreach(var user in allUsers)
            {
                idName.Add(user.Id, user.FirstName + " " + user.LastName);
            }
            ViewBag.IdNameHash = idName;

            

            var allOrders = _db.Orders;
            List<int> orderIds = new List<int>();
            foreach(var order in allOrders)
            {
                orderIds.Add(order.Id);
            }

            Hashtable orderNbrProducts = new Hashtable();
            var allOrderProducts = _db.OrderProducts;
            int nbrItems = 0;
            foreach(int orderId in orderIds)
            {
                nbrItems = 0;
                foreach(var orderProduct in allOrderProducts)
                {
                    if(orderId == orderProduct.OrderId)
                    {
                        nbrItems += orderProduct.Quantity;
                    }
                    if (nbrItems > 0 && orderId != orderProduct.OrderId)
                        break;
                }
                orderNbrProducts.Add(orderId, nbrItems);
            }
            ViewBag.OrderNbrItems = orderNbrProducts;

            return View(allOrders);
        }

        /*
         * Orders page that outputs past and present orders of the logged in user
         * */
        public async Task<IActionResult> MyOrdersAsync()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allOrders = from order in _db.Orders
                            where order.Customer == currentUser.Id
                            select order;

            List<int> orderIds = new List<int>();
            foreach (var order in allOrders)
            {
                orderIds.Add(order.Id);
            }

            Hashtable orderNbrProducts = new Hashtable();
            var allOrderProducts = _db.OrderProducts;
            int nbrItems = 0;
            foreach(int orderId in orderIds)
            {
                nbrItems = 0;
                foreach(var orderProduct in allOrderProducts)
                {
                    if(orderId == orderProduct.OrderId)
                    {
                        nbrItems += orderProduct.Quantity;
                    }
                    if (nbrItems > 0 && orderId != orderProduct.OrderId)
                        break;
                }
                orderNbrProducts.Add(orderId, nbrItems);
            }
            ViewBag.OrderNbrItems = orderNbrProducts;

            return View(allOrders);
        }

        /*
         * Details page for each order viewed through the administrator page
         */
        public IActionResult ViewDetails(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Order = order;

            string customerName = _db.Users.Find(order.Customer).FirstName +" "+ _db.Users.Find(order.Customer).LastName;
            ViewBag.CustomerName = customerName;

            Hashtable productQuantity = new Hashtable();
            var orderProducts = from oP in _db.OrderProducts
                                where oP.OrderId == order.Id
                                select oP;
            foreach(var oP in orderProducts)
            {
                productQuantity.Add(oP.ProductId, oP.Quantity);
            }
            ViewBag.ProductQuantity = productQuantity;
            
            var products = _db.Products;

            return View(products);
        }

        /*
         * Details page for each order viewed through the My Orders page of a logged in user
         */
        public async Task<IActionResult> ViewMyOrderDetailsAsync(int? id)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentUserId = currentUser.Id;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Order = order;

            Hashtable productQuantity = new Hashtable();
            var orderProducts = from oP in _db.OrderProducts
                                where oP.OrderId == order.Id
                                select oP;
            foreach (var oP in orderProducts)
            {
                productQuantity.Add(oP.ProductId, oP.Quantity);
            }
            ViewBag.ProductQuantity = productQuantity;

            var products = _db.Products;

            return View(products);
        }


        public IActionResult Cancel(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var orderToCancel = _db.Orders.Find(id);

            if (orderToCancel == null)
            {
                return NotFound();
            }

            orderToCancel.Status = "Canceled";
            orderToCancel.DeliveryDate = new DateTime(1111, 1, 1);
            orderToCancel.TotalPaid = 0.0f;
            _db.Orders.Update(orderToCancel);
            _db.SaveChanges();

            return RedirectToAction("MyOrders");
        }
    }
}
