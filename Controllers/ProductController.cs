
using ECommerceWebsite.data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWebsite.Controllers
{
    public class ProductController : Controller
    {
        private ProductContext context;

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
            // Display all products
            
            return View();
        }
    }
}
