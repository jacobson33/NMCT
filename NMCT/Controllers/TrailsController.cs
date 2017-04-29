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

            //get total stats for trail reviews
            TrailReviewStatsViewModel stats = new TrailReviewStatsViewModel(id);
            var reviews = db.Review.Where(t => t.TrailID == id).ToList();

            stats.Rating1 = reviews.Where(t => t.Rating == 1).Count();
            stats.Rating2 = reviews.Where(t => t.Rating == 2).Count();
            stats.Rating3 = reviews.Where(t => t.Rating == 3).Count();
            stats.Rating4 = reviews.Where(t => t.Rating == 4).Count();
            stats.Rating5 = reviews.Where(t => t.Rating == 5).Count();

            stats.Rating = ((stats.Rating1) + (stats.Rating2 * 2) + (stats.Rating3 * 3) + (stats.Rating4 * 4) + (stats.Rating5 * 5)) / (reviews.Count() == 0 ? 1 : reviews.Count());

            ViewBag.Stats = stats;
            ViewBag.TrailID = trail.TrailID;

            return View(trail);
        }

        public ActionResult ListOfReviewsByTrail(int id, int? rating)
        {
            var trailReviews = new List<Review>();

            if (rating != null && rating >= 1 && rating <= 5)
                trailReviews = db.Review.Where(a => a.TrailID == id).Where(a => a.Rating == rating).ToList();
            else
                trailReviews = db.Review.Where(a => a.TrailID == id).ToList();

            trailReviews.OrderByDescending(a => a.DateCreated);

            return PartialView(trailReviews);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("UserCreateReview")]
        [AuthorizeOrRedirectAttribute]
        public async Task<JsonResult> UserCreateReview(FormCollection collection)
        {
            var review = new Review();
            var user = User.Identity;
            string mess = "";

            int rating;
            int trailid;
            bool valid = true;

            //validate information
            if (!int.TryParse(collection["Rating"], out rating))
            {
                valid = false;
            }
            if (!int.TryParse(collection["TrailID"], out trailid))
            {
                valid = false;
            }

            if (!valid)
                return Json(new { status = "Error", message = "Review is invalid."});

            review.UserName = user.Name;
            review.DateCreated = DateTime.Now;
            review.Rating = rating;
            review.Title = collection["Title"];
            review.TrailID = trailid;
            review.Content = collection["Content"];

            if (ModelState.IsValid)
            {
                db.Review.Add(review);
                db.SaveChanges();
                return Json(new { status = "Success", message = "Success" });
            }

            return Json(new { status = "Error", message = "ModelState Not Valid" });
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
