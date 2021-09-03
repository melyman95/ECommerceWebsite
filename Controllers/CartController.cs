using ECommerceWebsite.data;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWebsite.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CartController(ProductContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            Product p = await ProductDb.GetSingleProductAsync(_context, id);

            const string CartCookie = "CartCookie";

            // Get existing cart items
            string existingItems = _httpContext.HttpContext.Request.Cookies[CartCookie];
            List<Product> cartProducts = new List<Product>();
            if (existingItems != null)
            {
                cartProducts = JsonConvert.DeserializeObject<List<Product>>(existingItems);
            }

            // Add current product to existing cart
            cartProducts.Add(p);

            // Add products list to cart cookie
            string data = JsonConvert.SerializeObject(cartProducts);
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };

            _httpContext.HttpContext.Response.Cookies.Append(CartCookie, data, options);

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Summary()
        {
            string cookieData = _httpContext.HttpContext.Request.Cookies["CartCookie"];

            List<Product> cartProducts = JsonConvert.DeserializeObject<List<Product>>(cookieData);

            return View(cartProducts);
        }
    }
}