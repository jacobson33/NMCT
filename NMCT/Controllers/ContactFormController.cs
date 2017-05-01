using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NMCT.Models;
using NMCT.CustomAttribute;
using NMCT.Models.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net;

namespace NMCT.Controllers
{
    public class ContactFormController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public ActionResult Index(int? status, string search = null)
        {
            var contactForms = db.ContactForms.ToList();

            ViewBag.Employees = GetEmployees();

            return View(contactForms);
        }

        [NonAction]
        public List<SelectListItem> GetEmployees()
        {
            var managers = db.Database.SqlQuery<UserByRoleViewModel>("dbo.GetUsersByRole 'Manager'").ToList();
            var moderators = db.Database.SqlQuery<UserByRoleViewModel>("dbo.GetUsersByRole 'Moderator'").ToList();

            var employees = new List<SelectListItem>();

            employees.Add(new SelectListItem() { Text = "Choose user to assign...", Value = "" });

            managers.ForEach(m => employees.Add(new SelectListItem() { Text = m.UserName, Value = m.UserName }));
            moderators.ForEach(m => employees.Add(new SelectListItem() { Text = m.UserName, Value = m.UserName }));

            return employees;
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            bool success = false;
            var userid = User.Identity.GetUserId();
            string userName = db.Users.Find(userid).UserName;

            ContactForm form = new ContactForm();

            form.SubmissionDate = DateTime.Now;
            form.UserName = userName;
            form.FormStatus = FormStatus.Submitted;
            form.Title = collection["Title"];
            form.Content = collection["Content"];
            FormCategory cat;
            success = Enum.TryParse(collection["FormCategory"], out cat);
            if (success)
            {
                form.FormCategory = cat;

                db.ContactForms.Add(form);
                db.SaveChanges();

                return RedirectToAction("Submitted");
            }

            ViewBag.Message = "Something went wrong.";
            return View(form);
        }

        [HttpGet]
        public ActionResult Submitted()
        {
            return View();
        }

        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public ActionResult ManageContactForm(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ContactForm form = db.ContactForms.Find(id);

            if (form == null)
                return HttpNotFound();

            ViewBag.Employees = GetEmployees();

            return View(form);
        }

        [HttpPost]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public async Task<JsonResult> AssignUser(string userName, int formID)
        {
            if (userName == null)
                return Json("User Name not supplied. Please contact an administrator if this issue keeps occurring.");

            ContactForm form = db.ContactForms.FirstOrDefault(f => f.FormID == formID);

            if (form == null)
                return Json("Couldn't find form.  Please contact an administrator if this issue keeps occurring.");

            if (form.FormStatus == FormStatus.Submitted && userName != "")
                form.FormStatus = FormStatus.Assigned;
            else if (form.FormStatus == FormStatus.Assigned & userName == "")
                form.FormStatus = FormStatus.Submitted;                

            form.AssignedUserName = userName;
            db.SaveChanges();

            if (userName == "")
                return Json("This form has been unassigned.");

            return Json("This form has been assigned to " + userName + ".");
        }

        [HttpPost]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Manager,Moderator")]
        public async Task<JsonResult> SetStatus(int status, int formID)
        {
            ContactForm form = db.ContactForms.FirstOrDefault(f => f.FormID == formID);

            if (form == null)
                return Json("Couldn't find form. Please contact an administrator if this issue keeps occurring.");

            if ((FormStatus)status == FormStatus.Resolved)
                form.ResolvedDate = DateTime.Now;
            else
                form.ResolvedDate = null;

            form.FormStatus = (FormStatus)status;
            db.SaveChanges();

            return Json("This form has been changed to a " + form.FormStatus.ToString() + " status");
        }
    }
}