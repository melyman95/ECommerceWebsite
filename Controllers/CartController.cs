using ECommerceWebsite.data;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWebsite.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext context;

        public CartController(ProductContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            // get product from database
            // add product to cart cookie
            // redirect back to previous page
            //List<Product> products = await ProductDb.GetProductsAsync(context);
            return View();
        }

        public IActionResult Summary()
        {
            // display all products in shopping cart cookie
            return View();
        }
    }
}