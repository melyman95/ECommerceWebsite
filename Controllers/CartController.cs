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
        private readonly ProductContext context;
        private readonly IHttpContextAccessor httpContext;

        public CartController(ProductContext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            Product p = await ProductDb.GetSingleProductAsync(context, id);

            string data = JsonConvert.SerializeObject(p);
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };

            httpContext.HttpContext.Response.Cookies.Append("Cart_Cookie", data, options);

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Summary()
        {
            // display all products in shopping cart cookie
            return View();
        }
    }
}