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
    public class CompanyController : Controller
    {
        private MapsDB db = new MapsDB();
        
        // GET: /Company/
        public ActionResult Index()
        {
            return View(db.Companies.Include("Profile").ToList());
        }

        // GET: /Company/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Include("Profile").FirstOrDefault(c => c.CompanyId == id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: /Company/Create
        public ActionResult Create()
        {
            //var companies = db.AllCompanies;
            var allActiveCompanies = db.Companies.Where(c => c.Profile.IsActive && !c.Profile.IsDeleted);
            //return View(companies);
            ViewBag.AllActiveCompanies = allActiveCompanies;
            return View();
        }

        // POST: /Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CompanyId,CompanyName,License,StartDate,isActive,isDeleted")] Company company)
        {
            if (ModelState.IsValid)
            {
                company.Profile = new Profile { StartDate = DateTime.Today, IsDeleted = false };
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: /Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Include("Profile").FirstOrDefault(c => c.CompanyId == id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: /Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CompanyId,CompanyName,LicenseId,Profile.StartDate,Profile.IsActive,Profile")] Company company, Profile profile)
        {
            if (ModelState.IsValid)
            {
                Profile profileToUpdate = null;
                Company companyToUpdate = null;
                try
                {
                    companyToUpdate = db.Companies.Include("Profile").FirstOrDefault(c => c.CompanyId == company.CompanyId);
                    if (companyToUpdate != null)
                    profileToUpdate = companyToUpdate.Profile;
                    
                }
                catch
                {
                    RedirectToAction("Error", "Company", new { errormsg = "Error editing a company." });
                }

                
                if (profileToUpdate == null) return RedirectToAction("Error", "Company", new { errormsg = "Error editing a company." });
                profileToUpdate.IsActive = profile.IsActive;
                companyToUpdate.CompanyName = company.CompanyName;
                companyToUpdate.LicenseId = company.LicenseId;

                db.Entry(companyToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.Entry(profileToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: /Company/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: /Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            //db.Companies.Remove(company);
            //check if the company is assigned to a user, if yes, show a message, if not set isDeleted flag to true
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

        public ActionResult Error(string errormsg)
        {
            ViewBag.ErrMessage = errormsg;
            return this.View();
        }
    }
}
