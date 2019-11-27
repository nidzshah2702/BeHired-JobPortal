using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
       
        public string Id { get; set; }
         public int BusinessStreamId { get; set; }
        public string company_name { get; set; }
        public DateTime establishment_date { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string company_website_url { get; set; }
        public string description { get; set; }
        public string company_image { get; set; }
        public virtual ICollection<JobPost> JobPosts { get; set; }
        public virtual BusinessStream BusinessStream { get; set; }
        public virtual UserAccount UserAccount { get; set; }

    }
}