using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OPS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OPS.Controllers
{
    public class OrderController : Controller
    {
        private readonly OPSContext _context;

        // Inject OPSContext into the controller
        public OrderController(OPSContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            // Get orders from the database, including related OrderItems and Products
            var orders = await _context.Orders.Include(o => o.OrderItems)
                                               .ThenInclude(oi => oi.Product)
                                               .ToListAsync();
            return View(orders);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public async Task<IActionResult> Create()
        {
            // Retrieve the list of products from the database to pass to the view
            ViewBag.Products = await _context.Products.ToListAsync();
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order, int[] selectedProducts)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                order.OrderItems = new List<OrderItem>();

                // Add selected products to the order's OrderItems list
                foreach (var productId in selectedProducts)
                {
                    var product = await _context.Products.FindAsync(productId);
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

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If we get here, something failed. Re-pass the products list to the view.
            ViewBag.Products = await _context.Products.ToListAsync();
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.Products = await _context.Products.ToListAsync();
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order updatedOrder, int[] selectedProducts)
        {
            if (id != updatedOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
                if (order == null)
                {
                    return NotFound();
                }

                // Update order properties
                order.CustomerName = updatedOrder.CustomerName;
                order.CustomerEmail = updatedOrder.CustomerEmail;
                order.ShippingAddress = updatedOrder.ShippingAddress;
                order.Status = updatedOrder.Status;

                // Clear old order items and add new selected products
                order.OrderItems.Clear();
                foreach (var productId in selectedProducts)
                {
                    var product = await _context.Products.FindAsync(productId);
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

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If we get here, something failed. Re-pass the products list to the view.
            ViewBag.Products = await _context.Products.ToListAsync();
            return View(updatedOrder);
        }

        // GET: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var order = _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
