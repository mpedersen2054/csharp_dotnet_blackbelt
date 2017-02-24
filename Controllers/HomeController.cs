using Microsoft.AspNetCore.Mvc;
using bbelt.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace bbelt.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }
        // GET: /Wall
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        // GET: /Wall
        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("ShowLogin", "User");
            }
            User uzer = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            return View("Success", uzer);
        }
    }
}
