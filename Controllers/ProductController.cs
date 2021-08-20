
using ECommerceWebsite.data;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            // Get all products, then display all products
            List<Product> products = await context.Products.ToListAsync();

            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product p)
        {
            if (ModelState.IsValid)
            {
                // Add to database
                // redirect back to catalog page
                context.Products.Add(p);
                await context.SaveChangesAsync();

                TempData["Message"] = $"{p.Title} was added successfully.";

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // get product with corresponding id
            // pass product to view

            Product p =
               await (from prod in context.Products
                      where prod.ProductId == id
                      select prod).SingleAsync();
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                context.Entry(p).State = EntityState.Modified;
                await context.SaveChangesAsync();

                ViewData["Message"] = "Product updated successfully.";

            }
            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product p =
               await (from prod in context.Products
                      where prod.ProductId == id
                      select prod).SingleAsync();

            return View(p);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            Product p =
               await (from prod in context.Products
                      where prod.ProductId == id
                      select prod).SingleAsync();

            String prodTitle = p.Title;

            context.Entry(p).State = EntityState.Deleted;
            await context.SaveChangesAsync();

            TempData["Message"] = prodTitle + " was deleted.";

            return RedirectToAction("Index");
        }
    }
}
