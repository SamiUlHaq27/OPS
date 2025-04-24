using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using OPS.Models;

namespace OPS.Controllers
{
    public class ops : Controller
    {
        // GET: ops
        public ActionResult Index()
        {
            Product obj = new Product{ id = 1, name = "apple", description = "sweet apple", price = 14, stock = 10 };
            return View();
        }

        // GET: ops/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ops/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ops/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ops/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ops/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
