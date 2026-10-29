using Lucas_The_Tavern_Order_Management_System.Data;
using Microsoft.AspNetCore.Mvc;
using Lucas_The_Tavern_Order_Management_System;
using Lucas_The_Tavern_Order_Management_System.Models;

namespace Lucas_The_Tavern_Order_Management_System.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(bool fromCart = false, string searchString = "")
        {
            ViewBag.FromCart = fromCart;

            var products = _db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p =>
                    p.Name.ToLower().Contains(searchString.ToLower()));
            }

            ViewData["searchString"] = searchString;

            return View(products.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
                return RedirectToAction("Index");

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);

            if (product != null)
            {
                _db.CartItems.RemoveRange(
                    _db.CartItems.Where(c => c.ProductID == id)
                );

                _db.Products.Remove(product);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
