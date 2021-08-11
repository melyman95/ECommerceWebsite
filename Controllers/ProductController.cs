
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
    }
}
