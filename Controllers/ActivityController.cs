using System;
using Microsoft.AspNetCore.Mvc;
using bbelt.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

/*
THINGS NOT DONE

= Validation for user
= /delete (couldnt get casading effect to delete)
= not allow to join activity if conflicting activities
*/

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

            List<Activity> activities = _context.Activities
                .Include(a => a.Creator)
                .Include(a => a.Participants)
                    .ThenInclude(p => p.Participant)
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
        public IActionResult NewActivity(int activityId)
        {
            Activity activ;
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("ShowLogin", "User");
            }
            User uzer = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));

            try {
                activ = _context.Activities
                .Where(a => a.ActivityId == activityId)
                .Include(a => a.Creator)
                .Include(a => a.Participants)
                .First();
            } catch {
                return RedirectToAction("ActivityList");
            }

            ViewBag.activity = activ;
            // return View(_context.Users.ToList());
            return View("ShowActivity");
        }


        // POST: /new
        [HttpPost]
        [Route("new")]
        public IActionResult AddNewActivity(ActivityValid activ)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("ShowLogin", "User");
            }

            if (ModelState.IsValid)
            {
                DateTime enddate = GetEndDate(activ.DateAt, activ.DurationInc, activ.Duration);

                Activity newActiv = new Activity {
                    Title = activ.Title,
                    DateAt = activ.DateAt,
                    DateEnd = enddate,
                    Duration = activ.Duration,
                    DurationInc = activ.DurationInc,
                    Description = activ.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatorId = (int)HttpContext.Session.GetInt32("UserId")
                };

                _context.Add(newActiv);
                _context.SaveChanges();

                Activity createdActiv = _context.Activities.SingleOrDefault(a => a.Title == activ.Title);

                UserActivity newUserActiv = new UserActivity {
                    ParticipantId = (int)HttpContext.Session.GetInt32("UserId"),
                    ActivityId = createdActiv.ActivityId
                };

                _context.Add(newUserActiv);
                _context.SaveChanges();

                return RedirectToAction("ActivityList", "Activity");
            }

            return View("NewActivity", activ);
        }

        // GET: /leave
        [HttpPost]
        [Route("leave")]
        public IActionResult LeaveActivity(int activityId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("ShowLogin", "User");
            }
            User uzer = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Activity activ = _context.Activities.SingleOrDefault(act => act.ActivityId == activityId);

            UserActivity uzerActiv = _context.UserActivities.SingleOrDefault(ua => ua.ParticipantId == uzer.UserId && ua.ActivityId == activ.ActivityId);

            _context.UserActivities.Remove(uzerActiv);
            _context.SaveChanges();

            return RedirectToAction("ActivityList");
        }

        // GET: /leave
        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteActivity(int activityId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("ShowLogin", "User");
            }
            User uzer = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Activity activ = _context.Activities.SingleOrDefault(act => act.ActivityId == activityId);

            // if uzer.id != activ.CreatorId, dont let them delete
            // should be protected, but some1 could send POST req
            // from somewhere other than the app interface
            if (activ.CreatorId != uzer.UserId)
            {
                return RedirectToAction("ActivityList");
            }

            List<UserActivity> uzerActiv = _context.UserActivities.Where(ua => ua.ActivityId == activ.ActivityId).ToList();

            // remove all UserActivities with the given ac, then
            // delete the Activity to avoid nested transaction error
            _context.UserActivities.RemoveRange(uzerActiv);
            _context.SaveChanges();
            _context.Remove(activ);
            _context.SaveChanges();

            return RedirectToAction("ActivityList");
        }

        // GET: /leave
        [HttpPost]
        [Route("join")]
        public IActionResult JoinActivity(int activityId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("ShowLogin", "User");
            }
            System.Console.WriteLine(activityId);
            System.Console.WriteLine(activityId.GetType());
            User uzer = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Activity activ = _context.Activities.SingleOrDefault(act => act.ActivityId == activityId);

            UserActivity newUserActiv = new UserActivity {
                ParticipantId = uzer.UserId,
                ActivityId = activ.ActivityId
            };

            _context.Add(newUserActiv);
            _context.SaveChanges();

            return RedirectToAction("ActivityList");
        }

        private DateTime GetEndDate(DateTime startdate, string inc, int duration)
        {
            DateTime enddate = new DateTime();

            if (inc == "Minute")
            {
                enddate = startdate.AddMinutes(duration);
            }
            if (inc == "Hour")
            {
                enddate = startdate.AddHours(duration);
            }
            if (inc == "Day")
            {
                enddate = startdate.AddDays(duration);
            }
            return enddate;
        }

        
    }
}
