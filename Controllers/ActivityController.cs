using Microsoft.AspNetCore.Mvc;
using bbelt.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace bbelt.Controllers
{
    public class ActivityController : Controller
    {
        private MyContext _context;
        public ActivityController(MyContext context)
        {
            _context = context;
        }
        // GET: /home
        [HttpGet]
        [Route("home")]
        public IActionResult ActivityList()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("ShowLogin", "User");
            }
            User uzer = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));

            // User uzer = _context.Users
            //         .Include(u => u.Activities)
            //             .ThenInclude(a => a.Participant)
            //         .SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));

            List<Activity> activities = _context.Activities
                .Include(a => a.Participants)
                .ToList();
            
            ViewBag.user = uzer;
            ViewBag.activities = activities;
            return View("ActivityList");
        }

        // GET: /new
        [HttpGet]
        [Route("new")]
        public IActionResult NewActivity()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("ShowLogin", "User");
            }
            User uzer = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            // return View(_context.Users.ToList());
            return View("NewActivity");
        }

        // GET: /activity/{x}
        [HttpGet]
        [Route("activity/{ActivityId}")]
        public IActionResult NewActivity(int ActivityId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("ShowLogin", "User");
            }
            User uzer = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            // return View(_context.Users.ToList());
            return View("ShowActivity");
        }

        
    }
}
