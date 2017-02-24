using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bbelt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace bbelt.Controllers
{
    public class UserController : Controller
    {
        private MyContext _context;
        public UserController(MyContext context)
        {
            _context = context;
        }

        // GET: /register
        [HttpGet]
        [Route("register")]
        public IActionResult ShowRegister()
        {
            return View("Register");
        }

        // GET: /login
        [HttpGet]
        [Route("login")]
        public IActionResult ShowLogin()
        {
            return View("Login");
        }

        // GET: /register
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // POST: /register
        [HttpPost]
        [Route("register")]
        public IActionResult SubmitRegister(UserRegister userReg)
        {
            if (ModelState.IsValid)
            {
                // take the userReg object and convert it to User, with a hashed pw
                PasswordHasher<UserRegister> Hasher = new PasswordHasher<UserRegister>();
                User newUser = new User {
                    FirstName = userReg.FirstName,
                    LastName = userReg.LastName,
                    Email = userReg.Email,
                    Password = Hasher.HashPassword(userReg, userReg.Password), // hash pw
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                // save the new user with hashed pw
                _context.Users.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Index", "Home");
            }
            return View("Register", userReg);
        }

        // POST: /login
        [HttpPost]
        [Route("login")]
        public IActionResult SubmitLogin(UserLogin userLog)
        {
            if (ModelState.IsValid)
            {
                var User = _context.Users.SingleOrDefault(user => user.Email == userLog.Email);
                if (User != null && userLog.Password != null)
                {
                    // check if the password matches
                    var Hasher = new PasswordHasher<User>();
                    if (0 != Hasher.VerifyHashedPassword(User, User.Password, userLog.Password))
                    {
                        // if match, set id to session & redirect
                        HttpContext.Session.SetInt32("UserId", User.UserId);
                        return RedirectToAction("Success", "Home");
                    }
                }
            }
            return View("Login", userLog);
        }
    }
}
