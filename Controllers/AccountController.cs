using person_money.Models;
using person_money.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace person_money.Controllers
{
    public class AccountController : Controller
    {
        private PersonMoneyContext db;
        public AccountController(PersonMoneyContext context)
        {
            db = context;
            if (context.Roles.Count() == 0)
            {
                Role role = new Role() { Name = "Администратор" };
                context.Roles.Add(role);
                Role role2 = new Role() { Name = "Пользователь" };
                context.Roles.Add(role2);            
                context.SaveChanges();

            }
            if (context.Users.Count() == 0)
            {
                User user = new User() { IdRole = 1, Login = "admin", Password = "1234" };
                context.Users.Add(user);
                context.SaveChanges();

            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    string roleUser = db.Roles.FirstOrDefault(n => n.Id == user.IdRole).Name;
                    if (roleUser == "Администратор")
                    {
                        return RedirectToAction("Index", "Users");
                    }
                    if (roleUser == "Пользователь")
                    {
                        return RedirectToAction("Index", "Reports", new { idCustomer = user.Id } );
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
         
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    int idRole = db.Roles.FirstOrDefault(n => n.Name == model.UserRole).Id;
                    User usr = new User { Login = model.Login, Password = model.Password, IdRole = idRole };
                    db.Users.Add(usr);
                    await db.SaveChangesAsync();

                    await Authenticate(usr);

                    return RedirectToAction("Index", "Reports", new { idCustomer = usr.Id });
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.IdRole.ToString()+","+user.Id.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
