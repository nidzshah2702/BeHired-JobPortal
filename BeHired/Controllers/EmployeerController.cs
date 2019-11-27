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
    public class EmployeerController : Controller
    {
        // GET: Employeer
        MyDbContext db = new MyDbContext();
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Employeer"))
            {

                return Redirect("/Account/Login");
            }
            string userid = User.Identity.GetUserId();
            var id = db.Companies.SingleOrDefault(x => x.Id == userid).CompanyId;
            var company = db.Companies.Find(id);
            ViewBag.Company = company;
            var jobs = db.JobPosts.Include(c => c.Company).Include(c => c.Category).Where(x => x.CompanyId == id).ToList();
            var count = db.JobPosts.Include(c => c.Company).Include(c => c.Category).Where(x => x.CompanyId == id).Count<JobPost>();
            ViewBag.count = count;
            return View(jobs);
        }
        public ActionResult ViewProfile()
        {
            string userid = User.Identity.GetUserId();

            var company = db.Companies.Include(c => c.BusinessStream).Include(c => c.UserAccount).SingleOrDefault(x => x.Id == userid);

            if (company == null)
            {
                return RedirectToAction("CreateProfile");
            }
            return View(company);
        }
        public ActionResult CreateProfile()
        {
            var bs = db.BusinessStreams.ToList();
            ViewData["BusinessStreamId"] = new SelectList(bs, "BusinessStreamId", "business_stream_name");

            return View();
        }
        [HttpPost]
        public ActionResult CreateProfile([Bind(Include = "CompanyId,company_name,address,contact,BusinessStreamId,company_website_url,company_image,company_description,establishment_date")]Company model)
        {
            string userid = User.Identity.GetUserId();
            Company c = new Company();
            model.Id = User.Identity.GetUserId();
            model.UserAccount = db.Users.SingleOrDefault(x => x.Id == userid);
            db.Companies.Add(model);
            db.SaveChanges();
            return RedirectToAction("ViewProfile");
        }

        public ActionResult PostJob()
        {
            string[] periods = { "Full Time", "Part Time", "Internship", "Trainee","Other" };
            string[] experience = { "0 Year", "1 Year", "2 Year", "3 Year", "4 Year", "5 Year", "6 Year", "7 Year", "8 Year", "9 Year", "10 Year" };
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "category_name");
            ViewData["period"] = new SelectList(periods.AsEnumerable<string>());
            ViewData["experience"] = new SelectList(experience.AsEnumerable<string>());
            return View();
        }
        [HttpPost]
        public ActionResult PostJob(JobPost model)
        {
            string userid = User.Identity.GetUserId();
            var company = db.Companies.SingleOrDefault(x => x.Id == userid);
            model.CompanyId = company.CompanyId;
            model.Company = company;
            db.JobPosts.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: JobPosts/Edit/5
        public ActionResult EditJobPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobPost = db.JobPosts.Find(id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "category_name", jobPost.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Id", jobPost.CompanyId);
            return View(jobPost);
        }

        // POST: JobPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobPost([Bind(Include = "JobId,CompanyId,CategoryId,job_title,period,last_application_date,experience,job_description")] JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "category_name", jobPost.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Id", jobPost.CompanyId);
            return View(jobPost);
        }

        // GET: JobPosts/Delete/5
        public ActionResult DeleteJobPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobPost = db.JobPosts.Find(id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            return View(jobPost);
        }

        // POST: JobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPost jobPost = db.JobPosts.Find(id);
            db.JobPosts.Remove(jobPost);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult JobDetails(int id)
        {
            var job = db.JobPosts.Include(x => x.Category).Include(x => x.Company).Include(x => x.JobSkillSets).SingleOrDefault(x => x.JobId == id);


            return View(job);
        }
        public ActionResult AddJobSkill(int id)
        {
            string[] levels = { "Begineer", "Intermediate", "Advanced" };
            ViewBag.JobId = id;
            ViewData["skill_level"] = new SelectList(levels.AsEnumerable<string>());
            TempData["Msg"] = "Data has been saved succeessfully";
            return View();
        }
        [HttpPost]
        public ActionResult AddJobSkill(JobSkillSet model)
        {
            

            
            db.JobSkillSets.Add(model);
            db.SaveChanges();
            return Redirect("/Employeer/JobDetails/"+model.JobId);


        }
        // GET: Skills/Edit/5
        public ActionResult EditJobSkill(int? id)
        {
            string[] levels = { "Begineer", "Intermediate", "Advanced" };

            ViewData["skill_level"] = new SelectList(levels.AsEnumerable<string>());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobSkillSet skill = db.JobSkillSets.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Skills/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobSkill([Bind(Include = "JobSkillId,JobId,skill,skill_level")] JobSkillSet skill)
        {
            string[] levels = { "Begineer", "Intermediate", "Advanced" };

            ViewData["skill_level"] = new SelectList(levels.AsEnumerable<string>());
            if (ModelState.IsValid)
            {
                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Employeer/JobDetails/"+skill.JobId);
            }
            return View(skill);
        }

        // GET: Skills/Delete/5
        public ActionResult DeleteJobSkill(int? id)
        {
            JobSkillSet skill = db.JobSkillSets.Find(id);
            db.JobSkillSets.Remove(skill);
            db.SaveChanges();
            return Redirect("/Employeer/JobDetails/"+skill.JobId);
        }

        public ActionResult ViewApplicants()
        {
            string userid = User.Identity.GetUserId();
            var jobApplications = db.JobApplications.Include(j => j.JobPost).Include(j => j.Profile).Include(x=>x.JobPost.Company).Where(x=>x.JobPost.Company.Id==userid);

            return View(jobApplications);
        }
        // POST: Skills/Delete/5
       
        public ActionResult ApplicantProfile(int? id)
        {
            int profileid = db.JobApplications.Find(id).ProfileId;
           var j= db.JobApplications.Find(id);
            j.neworold = "no";
            db.Entry(j).State = EntityState.Modified;
            db.SaveChanges();
            var p = db.Profiles.Include(x => x.EducationDetails).Include(x => x.ExperienceDetails).Include(x => x.UserAccount).Include(x=>x.Skills).SingleOrDefault(x => x.ProfileId == profileid);
            
            return View(p);
        }
    }
}