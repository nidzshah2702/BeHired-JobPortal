using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class JobPost
    {
        [Key]
        public int JobId { get; set; }
        
       
       
        public int CompanyId { get; set; }
       
        public int CategoryId { get; set; }
        public string job_title { get; set; }
        public string period { get; set; }
        public DateTime last_application_date { get; set; }
        public string experience { get; set; }
        public string job_description { get; set; }
        public virtual Company Company { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<JobSkillSet> JobSkillSets { get; set; }
        public virtual ICollection<JobApplication> Candidates { get; set; }


    }
}