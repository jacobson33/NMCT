using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NMCT.Models;


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
            //var upcomingEvents = db.Database.SqlQuery<Event>("dbo.GetUpcomingEvents").ToList();

            //ViewBag.UpcomingEvents = upcomingEvents;
            ViewBag.TopFive = topfive;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}