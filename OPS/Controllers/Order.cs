using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPS.Models;
using System.Collections.Generic;
using System.Linq;

namespace OPS.Controllers
{
    public class OrderController : Controller
    {
        private static List<Order> orders = new List<Order>();
        private static List<Product> products = Ops.products;

        // GET: Order
        public ActionResult Index()
        {
            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.Products = products; // Pass the list of products to the view
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order, int[] selectedProducts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    order.Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1;
                    order.OrderDate = DateTime.Now;

                    // Add selected products to the order
                    order.OrderItems = new List<OrderItem>();
                    foreach (var productId in selectedProducts)
                    {
                        var product = products.FirstOrDefault(p => p.Id == productId);
                        if (product != null)
                        {
                            order.OrderItems.Add(new OrderItem
                            {
                                ProductId = product.Id,
                                ProductName = product.Name,
                                ProductPrice = product.Price,
                                Quantity = 1 // Default to 1 for simplicity
                            });
                        }
                    }


                    orders.Add(order);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Products = products;
                return View(order);
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Products = products;
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order updatedOrder, int[] selectedProducts)
        {
            try
            {
                var order = orders.FirstOrDefault(o => o.Id == id);
                if (order == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    // Update order fields
                    order.OrderDate = updatedOrder.OrderDate;
                    order.CustomerName = updatedOrder.CustomerName;
                    order.CustomerEmail = updatedOrder.CustomerEmail;
                    order.ShippingAddress = updatedOrder.ShippingAddress;
                    order.Status = updatedOrder.Status;

                    // Clear old order items and add new selected products
                    order.OrderItems.Clear();
                    foreach (var productId in selectedProducts)
                    {
                        var product = products.FirstOrDefault(p => p.Id == productId);
                        if (product != null)
                        {
                            order.OrderItems.Add(new OrderItem
                            {
                                ProductId = product.Id,
                                ProductName = product.Name,
                                ProductPrice = product.Price,
                                Quantity = 1
                            });
                        }
                    }


                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Products = products;
                return View(updatedOrder);
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var order = orders.FirstOrDefault(o => o.Id == id);
                if (order != null)
                {
                    orders.Remove(order);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
