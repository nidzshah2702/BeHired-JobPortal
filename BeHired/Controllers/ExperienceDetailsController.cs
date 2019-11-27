using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeHired.Models;

namespace BeHired.Controllers
{
    public class ExperienceDetailsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: ExperienceDetails
        public ActionResult Index()
        {
            var experienceDetails = db.ExperienceDetails.Include(e => e.Profile);
            return View(experienceDetails.ToList());
        }

        // GET: ExperienceDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExperienceDetail experienceDetail = db.ExperienceDetails.Find(id);
            if (experienceDetail == null)
            {
                return HttpNotFound();
            }
            return View(experienceDetail);
        }

        // GET: ExperienceDetails/Create
        public ActionResult Create()
        {
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "Id");
            return View();
        }

        // POST: ExperienceDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExperienceDetailId,ProfileId,start_date,end_data,job_title,company_name,job_location_city,job_location_state,job_location_country,description")] ExperienceDetail experienceDetail)
        {
            if (ModelState.IsValid)
            {
                db.ExperienceDetails.Add(experienceDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "Id", experienceDetail.ProfileId);
            return View(experienceDetail);
        }

        // GET: ExperienceDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExperienceDetail experienceDetail = db.ExperienceDetails.Find(id);
            if (experienceDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "Id", experienceDetail.ProfileId);
            return View(experienceDetail);
        }

        // POST: ExperienceDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExperienceDetailId,ProfileId,start_date,end_data,job_title,company_name,job_location_city,job_location_state,job_location_country,description")] ExperienceDetail experienceDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(experienceDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "Id", experienceDetail.ProfileId);
            return View(experienceDetail);
        }

        // GET: ExperienceDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExperienceDetail experienceDetail = db.ExperienceDetails.Find(id);
            if (experienceDetail == null)
            {
                return HttpNotFound();
            }
            return View(experienceDetail);
        }

        // POST: ExperienceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExperienceDetail experienceDetail = db.ExperienceDetails.Find(id);
            db.ExperienceDetails.Remove(experienceDetail);
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
