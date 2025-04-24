using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPS.Models;
using System.Collections.Generic;
using System.Linq;

namespace OPS.Controllers
{
    public class Ops : Controller
    {
        public static List<Product> products = new List<Product>();

        public ActionResult Index()
        {
            return View(products);
        }

        public ActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1; 
                    products.Add(product);
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product updatedProduct)
        {
            try
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    product.Name = updatedProduct.Name; 
                    product.Description = updatedProduct.Description;
                    product.Price = updatedProduct.Price;
                    product.Stock = updatedProduct.Stock;
                    return RedirectToAction(nameof(Index));
                }
                return View(updatedProduct);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    products.Remove(product);
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
