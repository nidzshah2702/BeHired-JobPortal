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
    public class EducationDetailsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: EducationDetails
        public ActionResult Index()
        {
            var educationDetails = db.EducationDetails.Include(e => e.Profile);
            return View(educationDetails.ToList());
        }

        // GET: EducationDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationDetail educationDetail = db.EducationDetails.Find(id);
            if (educationDetail == null)
            {
                return HttpNotFound();
            }
            return View(educationDetail);
        }

        // GET: EducationDetails/Create
        public ActionResult Create()
        {
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "Id");
            return View();
        }

        // POST: EducationDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EducationDetailId,ProfileId,certificate_degree_name,major,institute_university_name,starting_date,completion_date,percentage,cgpa")] EducationDetail educationDetail)
        {
            if (ModelState.IsValid)
            {
                db.EducationDetails.Add(educationDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "Id", educationDetail.ProfileId);
            return View(educationDetail);
        }

        // GET: EducationDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationDetail educationDetail = db.EducationDetails.Find(id);
            if (educationDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "Id", educationDetail.ProfileId);
            return View(educationDetail);
        }

        // POST: EducationDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EducationDetailId,ProfileId,certificate_degree_name,major,institute_university_name,starting_date,completion_date,percentage,cgpa")] EducationDetail educationDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(educationDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "Id", educationDetail.ProfileId);
            return View(educationDetail);
        }

        // GET: EducationDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationDetail educationDetail = db.EducationDetails.Find(id);
            if (educationDetail == null)
            {
                return HttpNotFound();
            }
            return View(educationDetail);
        }

        // POST: EducationDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EducationDetail educationDetail = db.EducationDetails.Find(id);
            db.EducationDetails.Remove(educationDetail);
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
