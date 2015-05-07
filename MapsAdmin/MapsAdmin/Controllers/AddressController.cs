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
    using EntityState = System.Data.Entity.EntityState;

    public class AddressController : Controller
    {
        private MapsDB db = new MapsDB();

        // GET: /Address
        public ActionResult Index(int profileId)
        {
            //return this.View(db.Profiles.)
            return View(db.Addresses.Include("Profile").Where(a => a.Profile.ProfileId == profileId).ToList());
            //var profile = db.Profiles.FirstOrDefault(a => a.ProfileId == id);
            //return this.View(db.Profiles.Include("Address").Where(a => a.ProfileId == id).ToList());
        }

        // GET: /Address/Details/5
        public ActionResult Details(int profileId, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Include("Profile").FirstOrDefault(a => a.AddressId == id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: /Address/Create
        public ActionResult Create(int profileId)
        {
            return View();
        }

        // POST: /Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AddressId,AddressLine1,AddressLine2,City,PostalCode,Province,Region,Country,isActive")] Address address)
        {
            //Profile profile = null;
            
            
            int profileId;
            int.TryParse(Request["profileId"], out profileId);
            if (profileId == 0)
                return RedirectToAction("Error", "Address", new { errormsg = "Error creating an address. Profile is null." });
            //}
            var profile = db.Profiles.FirstOrDefault(p => p.ProfileId == profileId);
            address.Profile = profile;
            if (ModelState.IsValid)
            {
                address.StartDate = DateTime.Now;
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index", "Address", new { profileId});
            }

            return View(address);
        }

        // GET: /Address/Edit/5
        public ActionResult Edit(int profileId, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Include("Profile").FirstOrDefault(a => a.AddressId == id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: /Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AddressId,AddressLine1,AddressLine2,City,PostalCode,Province,Region,Country,StartDate,isActive,isDeleted")] Address address)
        {
            int profileId;
            int.TryParse(Request["profileId"], out profileId);
            if (profileId == 0)
                return RedirectToAction("Error", "Address", new { errormsg = "Error editing an address. Profile is null." });

            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Address", new { profileId });
            }
            return View(address);
        }

        // GET: /Address/Delete/5
        public ActionResult Delete(int profileId, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Include("Profile").FirstOrDefault(a => a.AddressId == id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: /Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int profileId, int id)
        {
            Address address = db.Addresses.Include("Profile").FirstOrDefault(a => a.AddressId == id);
            db.Addresses.Remove(address);
            db.SaveChanges();
            return RedirectToAction("Index", "Address", new { profileId });
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
