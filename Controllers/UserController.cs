using ECommerceWebsite.data;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                // Checking if username/email is unique/in use
                // if so, display error and send back to view
                bool EmailTaken = await (from useraccount in _context.Accounts
                                   where useraccount.Email == reg.Email
                                   select useraccount).AnyAsync();

                bool UserNameTaken = await (from useraccount in _context.Accounts
                                            where useraccount.UserName == reg.UserName
                                            select useraccount).AnyAsync();

                if (EmailTaken)
                {
                    ModelState.AddModelError(nameof(RegisterViewModel.Email), "That email is already in use.");
                    return View(reg);
                }

                if (UserNameTaken)
                {
                    ModelState.AddModelError(nameof(RegisterViewModel.UserName), "That username is already taken.");
                    return View(reg);
                }

                if (EmailTaken || UserNameTaken)
                {
                    return View(reg);
                }

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

                LogUserIn(account.AccountID);

                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }
            return View(reg);
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Check if already logged in
            if (HttpContext.Session.GetInt32("UserID").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Account account =
               await (from u in _context.Accounts
                      where (u.UserName == model.UserNameOrEmail
                      || u.Email == model.UserNameOrEmail)
                      && u.Password == model.Password
                      select u).SingleOrDefaultAsync();

            if (account == null)
            {
                // Credentials did not match
                // custom error message
                ModelState.AddModelError(string.Empty, "Account not found.");
                return View(model);
            }

            // credentials did match, log the user in
            LogUserIn(account.AccountID);

            return RedirectToAction("Index", "Home");
        }

        private void LogUserIn(int accountID)
        {
            HttpContext.Session.SetInt32("UserID", accountID);
        }

        public IActionResult Logout()
        {
            // terminate the session
            HttpContext.Session.Clear();
            return RedirectToAction(actionName:"Index", controllerName:"Home");
        }
    }
}
