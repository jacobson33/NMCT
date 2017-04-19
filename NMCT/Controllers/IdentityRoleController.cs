using Microsoft.AspNet.Identity.EntityFramework;
using NMCT.CustomAttribute;
using NMCT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NMCT.Controllers
{
    public class IdentityRoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: IdentityRole
        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }
                
        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator")]
        public ActionResult Details(string id = null)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
            {
                IdentityRole role = db.Roles.Find(id);
                if (role == null)
                    return HttpNotFound();
                else
                {
                    ViewBag.RoleName = role.Name;
                    return View(role);
                }                    
            }
        }

        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id, Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator")]
        public ActionResult Edit(string id = null)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            IdentityRole role = db.Roles.Find(id);

            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id, Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator")]
        public ActionResult Delete(string id = null)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            IdentityRole role = db.Roles.Find(id);

            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(string id = null)
        {
            IdentityRole identityRoleTemp = db.Roles.Find(id);
            db.Roles.Remove(identityRoleTemp);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}