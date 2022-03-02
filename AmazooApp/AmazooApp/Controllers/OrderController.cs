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
        }

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

        public IActionResult MyOrders()
        {
            return View();
        }

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
    }
}
