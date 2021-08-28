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
    public class UserController : Controller
    {
        private readonly ProductContext _context;

        public UserController(ProductContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account()
                {
                    DateOfBirth = reg.DateOfBirth,
                    Email = reg.Email,
                    Password = reg.Password,
                    UserName = reg.UserName
                };

                // add to database
               await  _context.Accounts.AddAsync(account);
               await _context.SaveChangesAsync();

                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }
            return View(reg);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Account account =
               await (from u in _context.Accounts
                 where (u.UserName == model.UserNameOrEmail
                 || u.Email == model.UserNameOrEmail)
                 && u.Password == model.Password
                 select u).SingleOrDefaultAsync();

            if (account == null)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
