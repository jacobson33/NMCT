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

namespace NMCT.Controllers
{
    public class TrailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trails
        public ActionResult Index()
        {
            return View(db.Trail.ToList());
        }

        // GET: Trails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trail trail = db.Trail.Find(id);
            if (trail == null)
            {
                return HttpNotFound();
            }
            return View(trail);
        }

        // GET: Trails/Create
        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Admin,Manager")]
        public ActionResult Create([Bind(Include = "TrailID,Name,County,Longitude,Latitude,Description,URL")] Trail trail)
        {
            if (ModelState.IsValid)
            {
                db.Trail.Add(trail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trail);
        }

        // GET: Trails/Edit/5
        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trail trail = db.Trail.Find(id);
            if (trail == null)
            {
                return HttpNotFound();
            }
            return View(trail);
        }

        // POST: Trails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager")]
        public ActionResult Edit([Bind(Include = "TrailID,Name,County,Longitude,Latitude,Description,URL")] Trail trail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trail);
        }

        // GET: Trails/Delete/5
        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trail trail = db.Trail.Find(id);
            if (trail == null)
            {
                return HttpNotFound();
            }
            return View(trail);
        }

        // POST: Trails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Trail trail = db.Trail.Find(id);
            db.Trail.Remove(trail);
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
