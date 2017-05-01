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
        public ActionResult Index(string sortby, string search, int trailid = 0)
        {
            var trails = db.Trail.ToList();

            ViewBag.TrailList = trails;
            ViewBag.TrailID = trailid;

            //filtering
            IEnumerable<Review> reviews = db.Review.Where(r => r.TrailID == trailid).ToList();
            if (!String.IsNullOrWhiteSpace(search))
                reviews = reviews.Where(r => r.UserName.ToUpper().Contains(search.ToUpper()));

            //sorting setup
            ViewBag.DateSort = sortby == "Date" ? "Date_desc" : "Date";
            ViewBag.RatingSort = sortby == "Rating" ? "Rating_desc" : "Rating";
            ViewBag.SortBy = sortby;

            //sorting
            switch (sortby)
            {
                case "Date":
                    reviews = reviews.OrderBy(r => r.DateCreated);
                    ViewBag.DateSortArrow = "glyphicon-chevron-up";
                    ViewBag.RatingSortArrow = null;
                    break;

                case "Date_desc":
                    reviews = reviews.OrderByDescending(r => r.DateCreated);
                    ViewBag.DateSortArrow = "glyphicon-chevron-down";
                    ViewBag.RatingSortArrow = null;
                    break;

                case "Rating":
                    reviews = reviews.OrderBy(r => r.Rating);
                    ViewBag.RatingSortArrow = "glyphicon-chevron-up";
                    ViewBag.DateSortArrow = null;
                    break;

                case "Rating_desc":
                    reviews = reviews.OrderByDescending(r => r.Rating);
                    ViewBag.RatingSortArrow = "glyphicon-chevron-down";
                    ViewBag.DateSortArrow = null;
                    break;

                default:
                    reviews = reviews.OrderByDescending(r => r.DateCreated);
                    ViewBag.NameSortArrow = null;
                    ViewBag.CountySortArrow = null;
                    break;
            }

            return View(reviews);
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
