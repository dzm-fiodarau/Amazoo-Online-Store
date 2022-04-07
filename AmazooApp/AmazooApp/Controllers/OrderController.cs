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
        private readonly UserManager<ApplicationUser> _userManager;

        // This is a constructor of the OrderController class.
        public OrderController(AmazooAppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;

            var allInProcessOrders = from inProcessOrder in _db.Orders
                                     where inProcessOrder.Status == "In Process"
                                     select inProcessOrder;

            DateTime today = DateTime.Today;

            foreach(Order order in allInProcessOrders)
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

        // This action outputs a View holding all orders stored in the system.
        public IActionResult AdminOrderList()
        {
            Hashtable idName = new();
            var allUsers = _db.Users;
            foreach(ApplicationUser user in allUsers)
            {
                idName.Add(user.Id, user.FirstName + " " + user.LastName);
            }
            ViewBag.IdNameHash = idName;

            var allOrders = _db.Orders;
            List<int> orderIds = new();
            foreach(var order in allOrders)
            {
                orderIds.Add(order.Id);
            }

            Hashtable orderNbrProducts = new();
            var allOrderProducts = _db.OrderProducts;
            int nbrItems;
            foreach(int orderId in orderIds)
            {
                nbrItems = 0;
                foreach(OrderProduct orderProduct in allOrderProducts)
                {
                    if(orderId == orderProduct.OrderId)
                    {
                        nbrItems += orderProduct.Quantity;
                    }
                    if ((nbrItems > 0) && (orderId != orderProduct.OrderId))
                        break;
                }
                orderNbrProducts.Add(orderId, nbrItems);
            }
            ViewBag.OrderNbrItems = orderNbrProducts;

            return View(allOrders);
        }

        // This action outputs a View holding all orders associated to the current user's account.
        public async Task<IActionResult> MyOrdersAsync()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allOrders = from orderOfCurrentUser in _db.Orders
                            where orderOfCurrentUser.Customer == currentUser.Id
                            select orderOfCurrentUser;

            List<int> orderIds = new();
            foreach (Order order in allOrders)
            {
                orderIds.Add(order.Id);
            }

            Hashtable orderNbrProducts = new();
            var allOrderProducts = _db.OrderProducts;
            int nbrItems;
            foreach(int orderId in orderIds)
            {
                nbrItems = 0;
                foreach(OrderProduct orderProduct in allOrderProducts)
                {
                    if(orderId == orderProduct.OrderId)
                    {
                        nbrItems += orderProduct.Quantity;
                    }
                    if ((nbrItems > 0) && (orderId != orderProduct.OrderId))
                        break;
                }
                orderNbrProducts.Add(orderId, nbrItems);
            }
            ViewBag.OrderNbrItems = orderNbrProducts;

            return View(allOrders);
        }

        // This action outputs a View holding all details of a specific
        // order from administrator list.
        public IActionResult ViewDetails(int? id)
        {
            var order = GetOrderFromId(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewBag.Order = order;

            string customerName = _db.Users.Find(order.Customer).FirstName +" "+ _db.Users.Find(order.Customer).LastName;
            ViewBag.CustomerName = customerName;

            Hashtable productQuantity = new();
            var orderProducts = from specificOrderOP in _db.OrderProducts
                                where specificOrderOP.OrderId == order.Id
                                select specificOrderOP;
            foreach(OrderProduct oP in orderProducts)
            {
                productQuantity.Add(oP.ProductId, oP.Quantity);
            }

            ViewBag.ProductQuantity = productQuantity;
            
            var products = _db.Products;

            return View(products);
        }

        // This action outputs a View holding all details of a specific
        // order from the customer list.
        public async Task<IActionResult> ViewMyOrderDetailsAsync(int? id)
        {
            var order = GetOrderFromId(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Order = order;

            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentUserId = currentUser.Id;

            Hashtable productQuantity = new();
            var orderProducts = from specificOrderOP in _db.OrderProducts
                                where specificOrderOP.OrderId == order.Id
                                select specificOrderOP;

            foreach (OrderProduct oP in orderProducts)
            {
                productQuantity.Add(oP.ProductId, oP.Quantity);
            }
            ViewBag.ProductQuantity = productQuantity;

            var products = _db.Products;

            return View(products);
        }

        // This action cancels a specific order and outputs MyOrders View.
        public IActionResult Cancel(int? id)
        {

            var orderToCancel = GetOrderFromId(id);

            if (orderToCancel == null)
            {
                return NotFound();
            }

            orderToCancel.Status = "Canceled";
            orderToCancel.DeliveryDate = new DateTime(1111, 1, 1);
            orderToCancel.TotalPaid = 0.0f;
            _db.Orders.Update(orderToCancel);

            Hashtable productQuantity = new();
            var orderProduct = from oP in _db.OrderProducts
                               where oP.OrderId == id
                               select oP;
            foreach(OrderProduct oP in orderProduct)
            {
                productQuantity.Add(oP.ProductId, oP.Quantity);
            }

            var allProducts = _db.Products;

            foreach(DictionaryEntry prodQuant in productQuantity)
            {
                var product = allProducts.Find(prodQuant.Key);
                if (product == null)
                    continue;

                product.QuantityInStock += (int)prodQuant.Value;
                _db.Products.Update(product);
            }

            _db.SaveChanges();

            return RedirectToAction("MyOrders");
        }

        // This method verifies the order id is valid and returns the
        // order associated with it.
        private Order GetOrderFromId(int? id)
        {
            if ((id == null) || (id == 0))
            {
                return null;
            }

            var order = _db.Orders.Find(id);

            return order;
        }
    }
}
