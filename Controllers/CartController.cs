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

        // constructor
        public CartController(ProductContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        
        public async Task<IActionResult> Add(int id, string previousUrl)
        {
            // get single product from database
            Product p = await ProductDb.GetSingleProductAsync(_context, id);

            // add product to cart
            CookieHelper.AddProductToCart(_httpContext, p);

            TempData["Message"] = p.Title + " added successfully";

            // redirects to previous url
            return Redirect(previousUrl);
        }

        public IActionResult Summary()
        {
            return View(CookieHelper.GetCartProducts(_httpContext));
        }
    }
}