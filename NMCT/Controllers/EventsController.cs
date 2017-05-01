using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NMCT.Models;
using NMCT.CustomAttribute;
using NMCT.Models.ViewModels;
using System.Threading.Tasks;

namespace NMCT.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult Index(string search, string sortby)
        {
            IEnumerable<Event> events = db.Event.ToList();
            
            //filtering
            if (!String.IsNullOrWhiteSpace(search))
                events = events.Where(e => e.EventName.ToUpper().Contains(search.ToUpper()));

            //sorting setup
            ViewBag.DateSort = sortby == "Date" ? "Date_desc" : "Date";
            ViewBag.NameSort = sortby == "Name" ? "Name_desc" : "Name";
            ViewBag.SortBy = sortby;

            //sorting
            switch (sortby)
            {
                case "Date":
                    events = events.OrderBy(e => e.EventDate);
                    ViewBag.DateSortArrow = "glyphicon-chevron-up";
                    ViewBag.NameSortArrow = null;
                    break;

                case "Date_desc":
                    events = events.OrderByDescending(e => e.EventDate);
                    ViewBag.DateSortArrow = "glyphicon-chevron-down";
                    ViewBag.NameSortArrow = null;
                    break;

                case "Name":
                    events = events.OrderBy(e => e.EventName);
                    ViewBag.NameSortArrow = "glyphicon-chevron-up";
                    ViewBag.DateSortArrow = null;
                    break;

                case "Name_desc":
                    events = events.OrderByDescending(e => e.EventName);
                    ViewBag.NameSortArrow = "glyphicon-chevron-down";
                    ViewBag.DateSortArrow = null;
                    break;

                default:
                    events = events.OrderByDescending(e => e.EventName);
                    ViewBag.NameSortArrow = null;
                    ViewBag.CountySortArrow = null;
                    break;
            }

            return View(events);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            @event.EventPictures = db.EventPictures.Where(x => x.EventID == @event.EventID).ToList();
            return View(@event);
        }

        [NonAction]
        public List<SelectListItem> GetTrails()
        {
            var trails = new List<SelectListItem>();
            trails.Add(new SelectListItem() { Text = "Choose a trail (optional)", Value = "" });

            var trailList = db.Trail.OrderBy(t => t.Name).ToList();
            trailList.ForEach(t => trails.Add(new SelectListItem() { Text = t.Name, Value = t.TrailID.ToString() }));

            return trails;
        }

        // GET: Events/Create
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public ActionResult Create()
        {
            ViewBag.Trails = GetTrails();
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public ActionResult Create([Bind(Include = "EventID,TrailID,EventDate,EventName,EventDescription,ContactPhone,ContactEmail,ContactUrl")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.Status = "active";
                db.Event.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        public async Task<JsonResult> SaveImageUrl(string url, int eventID)
        {
            if (url.Trim() == "")
                url = null;

            List<EventPictures> ep = db.EventPictures.Where(p => p.EventID == eventID).ToList();
            ep.ForEach(p => db.EventPictures.Remove(p));

            if (url != null)
                db.EventPictures.Add(new EventPictures() { PictureURL = url, EventID = eventID });

            db.SaveChanges();

            return Json("URL Updated!");
        }

        // GET: Events/Edit/5
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            ViewBag.ImageUrl = db.EventPictures.FirstOrDefault(x => x.EventID == @event.EventID).PictureURL;
            ViewBag.Trails = GetTrails();
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public ActionResult Edit([Bind(Include = "EventID,TrailID,EventDate,EventName,EventDescription,ContactPhone,ContactEmail,ContactUrl")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Event.Find(id);
            db.Event.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
