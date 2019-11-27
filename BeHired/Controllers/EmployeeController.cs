using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeHired.Models;
using System.Web.Mail;

using Microsoft.AspNet.Identity;


namespace BeHired.Controllers
{
    public class EmployeeController : Controller
    {
        MyDbContext db = new MyDbContext();

        // GET: Employee
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
            var jobs = db.JobPosts.Include(c => c.Company).Include(c => c.Category).Include(c => c.JobSkillSets).ToList();
            ViewBag.appliedjobs = db.JobApplications.Include(c => c.JobPost).Include(c => c.Profile).Where(c => c.Profile.Id == userid).ToList();
            ViewBag.count = db.JobPosts.Count<JobPost>();
            return View("BrowseJobs",jobs);
        }
        //GET:Profile
        public ActionResult ViewProfile()
        {
            string userid = User.Identity.GetUserId();

            var profile = db.Profiles.Include(c=>c.UserAccount).Include(c => c.EducationDetails).Include(c => c.ExperienceDetails)
                .Include(c => c.Skills).SingleOrDefault(x => x.Id == userid);
            var educationdetails = db.EducationDetails.Where(x => x.ProfileId == profile.ProfileId);
           // ViewBag.educationdetails=
            if (profile == null)
            {
                return RedirectToAction("CreateProfile");
            }
            return View(profile);
        }
        //Create Profile
        public ActionResult CreateProfile()
        {
            
            return View();
        }
        [HttpPost]
        
        public ActionResult CreateProfile(Profile model)
        {
            string userid = User.Identity.GetUserId();
           
            model.Id = User.Identity.GetUserId();
            model.UserAccount = db.Users.SingleOrDefault(x => x.Id == userid);
            db.Profiles.Add(model);
            db.SaveChanges();
            return RedirectToAction("ViewProfile");
        }

        public ActionResult EditProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            
            return View(profile);
        }

        // POST: Profiles/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "ProfileId,Id,firstname,lastname,current_salary,currency,currentpost,currentcompany,aboutme,linkedinProfileLink,facebooklink,twitterlink,instagramlink,githublink")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewProfile");
            }
           
            return View(profile);
        }

        //Add educationdetail
        public ActionResult AddEducationDetail()
        {
            return View();
        }
        [HttpPost]
       public ActionResult AddEducationDetail(EducationDetail model)
        {
            string userid = User.Identity.GetUserId();

            var profile = db.Profiles.SingleOrDefault(x => x.Id == userid);

            model.Profile = profile;
            model.ProfileId = profile.ProfileId;
            db.EducationDetails.Add(model);
            db.SaveChanges();
            return RedirectToAction("ViewProfile");
        }

        public ActionResult EditEducationDetail(int? id)
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

        // POST: EducationDetails/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEducationDetail([Bind(Include = "EducationDetailId,ProfileId,certificate_degree_name,major,institute_university_name,starting_date,completion_date,percentage,cgpa")] EducationDetail educationDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(educationDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewProfile");
            }
            
            return View(educationDetail);
        }

        // GET: EducationDetails/Delete/5
        public ActionResult DeleteEducationDetail(int? id)
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
        [HttpPost, ActionName("DeleteEducationDetail")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EducationDetail educationDetail = db.EducationDetails.Find(id);
            db.EducationDetails.Remove(educationDetail);
            db.SaveChanges();
            return RedirectToAction("ViewProfile");
        }

        //Add experiencedetail
        public ActionResult AddExperienceDetail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddExperienceDetail(ExperienceDetail model)
        {
            string userid = User.Identity.GetUserId();

            var profile = db.Profiles.SingleOrDefault(x => x.Id == userid);

            model.Profile = profile;
            model.ProfileId = profile.ProfileId;
            db.ExperienceDetails.Add(model);
            db.SaveChanges();
            return RedirectToAction("ViewProfile");
        }
        // GET: ExperienceDetails/Edit/5
        public ActionResult EditExperienceDetail(int? id)
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

        // POST: ExperienceDetails/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExperienceDetail([Bind(Include = "ExperienceDetailId,ProfileId,start_date,end_data,job_title,company_name,job_location_city,job_location_state,job_location_country,description")] ExperienceDetail experienceDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(experienceDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewProfile");
            }
            return View(experienceDetail);
        }

        // GET: ExperienceDetails/Delete/5
        public ActionResult DeleteExperienceDetail(int? id)
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
        [HttpPost, ActionName("DeleteExperienceDetail")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            ExperienceDetail experienceDetail = db.ExperienceDetails.Find(id);
            db.ExperienceDetails.Remove(experienceDetail);
            db.SaveChanges();
            return RedirectToAction("ViewProfile");
        }

        //Add SKill
        public ActionResult AddSkill()
        {
            string[] levels = { "Begineer", "Intermediate", "Advanced" };

            ViewData["skill_level"] = new SelectList(levels.AsEnumerable<string>());
            TempData["Msg"] = "Data has been saved succeessfully";
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddSkill(Skill model)
        {
            string userid = User.Identity.GetUserId();

            var profile = db.Profiles.SingleOrDefault(x => x.Id == userid);

            model.Profile = profile;
            model.ProfileId = profile.ProfileId;
            db.Skills.Add(model);
            db.SaveChanges();
            return RedirectToAction("ViewProfile");

            
        }
        // GET: Skills/Edit/5
        public ActionResult EditSkill(int? id)
        {
            string[] levels = { "Begineer", "Intermediate", "Advanced" };

            ViewData["skill_level"] = new SelectList(levels.AsEnumerable<string>());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Skills/Edit/5
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSkill([Bind(Include = "SkillId,ProfileId,skill_name,skill_level")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewProfile");
            }
            return View(skill);
        }

        // GET: Skills/Delete/5
        public ActionResult DeleteSkill(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("DeleteSkill")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed3(int id)
        {
            Skill skill = db.Skills.Find(id);
            db.Skills.Remove(skill);
            db.SaveChanges();
            return RedirectToAction("ViewProfile");
        }

        public ActionResult ApplyNow(int id)
        {
            string userid = User.Identity.GetUserId();
            JobApplication ja = new JobApplication();
            ja.JobId = id;
           var job = db.JobPosts.Include(x =>x.Company).SingleOrDefault(x => x.JobId == id);
            var profile = db.Profiles.Include(x =>x.UserAccount).SingleOrDefault(x => x.Id == userid);
            if (profile == null)
            {
                return RedirectToAction("CreateProfile");
            }
               ja.ProfileId=  profile.ProfileId;

            db.JobApplications.Add(ja);
            db.SaveChanges();
             string body = "Dear " + profile.firstname + ",\n\n You havw successfully applied for JobId:" + id + "for the post of " + job.job_title + "at " + job.Company.company_name + ". The company will get back to you soon.";
            //string body = "Hello";
            MailSender.SendEmail("behiredjobsportal@gmail.com","BeHired@123",profile.UserAccount.Email,"BeHired:Your have succesfully applied",body,MailFormat.Text,"");
            return View();
        }

        public ActionResult BrowseJobs()
        {
            string userid = User.Identity.GetUserId();
            var jobs = db.JobPosts.Include(c => c.Company).Include(c=>c.Category).Include(c=>c.JobSkillSets).ToList();
            ViewBag.appliedjobs = db.JobApplications.Include(c => c.JobPost).Include(c => c.Profile).Where(c => c.Profile.Id == userid).ToList();
            ViewBag.count = db.JobPosts.Count<JobPost>();
            return View(jobs);
        }
        [HttpPost]
        public ActionResult BrowseJobs(List<string> period)
        {
            string userid = User.Identity.GetUserId();
            ViewBag.appliedjobs = db.JobApplications.Include(c => c.JobPost).Include(c => c.Profile).Where(c => c.Profile.Id == userid).ToList();
            ViewBag.count = db.JobPosts.Count<JobPost>();
            ViewBag.period = period;

            if (period== null)
            {
               var  jobs = db.JobPosts.Include(c => c.Company).Include(c => c.Category).Include(c => c.JobSkillSets).ToList();
                return View("BrowseJobs", jobs);


            }
            else
            {
                var jobs = db.JobPosts.Include(c => c.Company).Include(c => c.Category).Include(c => c.JobSkillSets).Where(x => period.Contains(x.period)).ToList();
                return View("BrowseJobs", jobs);

            }

        }

        
    }
}