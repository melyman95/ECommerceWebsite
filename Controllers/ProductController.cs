
using ECommerceWebsite.data;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWebsite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext context;

        public ProductController(ProductContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Displays view that lists all products
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // Get all products, then display all products
            List<Product> products = context.Products.ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product p)
        {
            if (ModelState.IsValid)
            {
                // Add to database
                // redirect back to catalog page
                context.Products.Add(p);
                context.SaveChanges();

                TempData["Message"] = $"{p.Title} was added successfully.";

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
