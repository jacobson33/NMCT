using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NMCT.Models;
using NMCT.Models.ViewModels;


namespace NMCT.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public class TopFiveResults
        {
            public string Name { get; set; }
            public int TrailID { get; set; }
            public string Rating { get; set; }
        }
        
        public ActionResult Index()
        {
            var topfive = db.Database.SqlQuery<TopFiveResults>("dbo.GetTopFiveTrails").ToList();
            List<EventViewModel> upcomingEvents = db.Database.SqlQuery<EventViewModel>("dbo.GetUpcomingEvents").ToList();

            ViewBag.UpcomingEvents = upcomingEvents;
            ViewBag.TopFive = topfive;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Providing Trail Information to the Northern Michigan Cycling Community.";

            return View();
        }

        public ActionResult Contact()
        {
            return RedirectToAction("Create", "ContactForm");
        }
    }
}