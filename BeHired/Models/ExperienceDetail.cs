using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class ExperienceDetail
    {
        public int ExperienceDetailId { get; set; }
      
        public int ProfileId { get; set; }
        public char is_current_job { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_data { get; set; }
        public string job_title { get; set; }
        public string company_name { get; set; }
        public string job_location_city { get; set; }
        public string job_location_state { get; set; }
        public string job_location_country { get; set; }
        public string description { get; set; }
        public virtual Profile Profile { get; set; }



    }
}