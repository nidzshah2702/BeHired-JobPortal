using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }
       
        public string Id { get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        public string current_salary { get; set; }
        public string currency { get; set; }
        public string currentpost { get; set; }
        public string currentcompany { get; set; }
        public string aboutme { get; set; }

        public string linkedinProfileLink { get; set; } = "#";
        public string facebooklink { get; set; } = "#";
        public string twitterlink { get; set; } = "#";
        public string instagramlink { get; set; } = "#";
        public string githublink { get; set; } = "#";

        public virtual UserAccount UserAccount { get; set; }
        public ICollection<EducationDetail> EducationDetails { get; set; }
        public ICollection<ExperienceDetail> ExperienceDetails { get; set; }
        public ICollection<Skill> Skills { get;  set;}
        public ICollection<JobApplication> JobApplications { get; set; }
    }
}