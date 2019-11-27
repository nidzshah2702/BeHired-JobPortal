using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeHired.Models;
using Microsoft.AspNet.Identity;


namespace BeHired.Controllers
{
    public class CompaniesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Companies
        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.BusinessStream).Include(c => c.UserAccount);
            return View(companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            ViewBag.count = db.JobPosts.Where(x => x.CompanyId == id).Count<JobPost>();
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.BusinessStreamId = new SelectList(db.BusinessStreams, "BusinessStreamId", "business_stream_name");
            ViewBag.Id = new SelectList(db.Users, "Id", "user_type");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyId,Id,BusinessStreamId,company_name,establishment_date,address,contact,company_website_url,description,company_image")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessStreamId = new SelectList(db.BusinessStreams, "BusinessStreamId", "business_stream_name", company.BusinessStreamId);
            ViewBag.Id = new SelectList(db.Users, "Id", "user_type", company.Id);
            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.BusinessStreamId = new SelectList(db.BusinessStreams, "BusinessStreamId", "business_stream_name", company.BusinessStreamId);
            ViewBag.Id = new SelectList(db.Users, "Id", "user_type", company.Id);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyId,Id,BusinessStreamId,company_name,establishment_date,address,contact,company_website_url,description,company_image")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessStreamId = new SelectList(db.BusinessStreams, "BusinessStreamId", "business_stream_name", company.BusinessStreamId);
            ViewBag.Id = new SelectList(db.Users, "Id", "user_type", company.Id);
            return View(company);
        }

        // GET: Companies/Delete/5
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

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ViewJobs(int id)
        {
            var company = db.Companies.Find(id);
            ViewBag.Company = company;
            var jobs = db.JobPosts.Include(c=>c.Company).Include(c=>c.Category).Where(x => x.CompanyId == id).ToList();
            var count = db.JobPosts.Include(c => c.Company).Include(c => c.Category).Where(x => x.CompanyId == id).Count<JobPost>();
            if (User.Identity.IsAuthenticated)
            {
                var userid = User.Identity.GetUserId();

                ViewBag.appliedjobs = db.JobApplications.Include(c => c.JobPost).Include(c => c.Profile).Where(c => c.Profile.Id == userid).ToList();
            }else
            {
                ViewBag.appliedjobs = null;
            }
            ViewBag.count = count;
            return View(jobs);
        }
        public ActionResult JobDetails(int id)
        {
            var job = db.JobPosts.Include(x => x.Category).Include(x => x.Company).Include(x => x.JobSkillSets).SingleOrDefault(x=>x.JobId==id);
            return View(job);

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
