using BeHired.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Mail;


namespace BeHired.Controllers
{
    public class HomeController : Controller
    {
        MyDbContext db = new MyDbContext();
        public ActionResult Index()
        {
            var jobs = db.JobPosts.Include(x=>x.Company).ToList().RandomSample<JobPost>(4,false);
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "category_name");
            ViewBag.jobs = jobs;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Check()
        {
            if (User.IsInRole("Employee"))
            {
                return Redirect("/Employee");
            }else
            {
                return Redirect("/Employeer");
            }
        }

        public ActionResult SearchResult(string companyorskill,int CategoryId,string experience)
        {
            var jobs = db.JobPosts.Include(c => c.Company).Include(c => c.Category).Include(c => c.JobSkillSets).Where(x=>x.experience==experience).ToList();
            ViewBag.count = jobs.Count<JobPost>();
            ViewBag.company_name = companyorskill;
            ViewBag.experience = experience;
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "category_name");


            return View(jobs);
        }
        [HttpPost]
        public  ActionResult Subscribe(string email)
        {
            string body = "Hello user! you have successfully subscribed to our website for newsletter. For more benefits login to our website.";
            //string body = "Hello";
            MailSender.SendEmail("behiredjobsportal@gmail.com", "BeHired@123", email, "BeHired:Subscribed To Our Website", body, MailFormat.Text, "");
            return RedirectToAction("Index");
        }
    }
}