using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class MyDbContext:IdentityDbContext<UserAccount>
    {
        public MyDbContext() : base("MyDbContextCS")
    {
            Configuration.ProxyCreationEnabled = false;
        }
       // public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<EducationDetail> EducationDetails { get; set; }
        public virtual DbSet<ExperienceDetail> ExperienceDetails { get; set; }
        public virtual DbSet<BusinessStream> BusinessStreams { get; set; }
        public virtual DbSet<JobPost> JobPosts { get; set; }
        public virtual DbSet<JobSkillSet> JobSkillSets { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }
        public virtual DbSet<Category> Categories { get;  set;}
        public static MyDbContext Create()
        {
            return new MyDbContext();
        }

      //  public System.Data.Entity.DbSet<BeHired.Models.UserAccount> UserAccounts { get; set; }

        //   public System.Data.Entity.DbSet<BeHired.Models.UserAccount> UserAccounts { get; set; }
    }
}