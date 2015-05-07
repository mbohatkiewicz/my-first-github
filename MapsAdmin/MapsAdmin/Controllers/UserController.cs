using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entities;
using DataAccess;

namespace MapsAdmin.Controllers
{
    using MapsAdmin.Filters;

    using Services;

    public class UserController : Controller
    {
        private MapsDB db = new MapsDB();

        // GET: /User/
        public ActionResult Index()
        {

            return View(db.Users.Include("Profile").ToList());
        }

        // GET: /User/Details/5
        [Log]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Include("Profile").FirstOrDefault(u => u.UserId == id);
            ViewBag.AllActiveCompanies = GetActiveCompanyList();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            ViewBag.AllActiveCompanies = GetActiveCompanyList();
            
            return View();
        }

        public List<SelectListItem> GetActiveCompanyList()
        {
            var companyList = new List<SelectListItem>();
            companyList.Add(new SelectListItem { Text = "Select Company", Value = "" });
            foreach (Company c in db.Companies.Where(c => c.Profile.IsActive && !c.Profile.IsDeleted))
            {
                //companyList.Add(new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString() });
                companyList.Add(new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString() });
            }
            return companyList;
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Create([Bind(Include="UserId,FirstName,LastName,Username,Password,Email,AccessLevel,IsOwner,StartDate,isActive,isDeleted")] User user)
        {
            
            //user.Company = companyId;
            if (ModelState.IsValid)
            {
                var companyId = 0;
                int.TryParse(Request["Company"], out companyId);
                if (companyId == 0) return RedirectToAction("Error", "User", new { errormsg = "Error creating a user. Company is null." });

                user.Company.CompanyId = companyId;
                user.Profile.StartDate = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllActiveCompanies = GetActiveCompanyList();
            return View(user);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Include("Profile").FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var selectList = new SelectList(db.Companies.Where(c => c.Profile.IsActive && !c.Profile.IsDeleted), "CompanyId", "CompanyName", user.Company);
            ViewData["ActiveCompanies"] = selectList;
            
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Edit([Bind(Include="UserId,FirstName,LastName,Username,Password,Email,AccessLevel,IsOwner,StartDate,isActive,isDeleted")] User user)
        {
            
            if (ModelState.IsValid)
            {
                var companyId = 0;
                int.TryParse(Request["Company"], out companyId);
                if (companyId == 0)
                    return RedirectToAction("Error");

                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Error(string errormsg)
        {
            ViewBag.ErrMessage = errormsg;
            return this.View();
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
