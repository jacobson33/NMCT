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
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public ActionResult Index(int trailid = 0, int sort = 0)
        {
            var trails = db.Trail.ToList();
            var model = new List<Review>();

            ViewBag.TrailList = trails;
            ViewBag.TrailID = trailid;

            if (trailid == 0)
                return View(model);

            var reviews = db.Review.Where(r => r.TrailID == trailid);            

            switch (sort)
            {
                case 1:
                    model = reviews.OrderBy(r => r.Title).ToList();
                    break;
                case 2:
                    model = reviews.OrderByDescending(r => r.Rating).ToList();
                    break;
                case 3:
                    model = reviews.OrderBy(r => r.UserName).ToList();
                    break;
                case 4:
                    model = reviews.OrderBy(r => r.DateCreated).ToList();
                    break;
                default:
                    model = reviews.OrderByDescending(r => r.DateCreated).ToList();
                    break;
            }

            return View(model);
        }

        // GET: Reviews/Details/5
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }
        
        // GET: Reviews/Edit/5
        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public ActionResult Edit([Bind(Include = "ReviewID,Title,Content,UserName")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = review.TrailID });
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Review.Find(id);
            db.Review.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = review.TrailID });
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
