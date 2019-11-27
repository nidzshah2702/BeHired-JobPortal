using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class EducationDetail
    {
        public int EducationDetailId { get; set; }
      
        public int ProfileId { get; set; }
        public string certificate_degree_name { get; set; }
        public string major { get; set; }
        public string institute_university_name { get; set; }
        public DateTime starting_date { get; set; }
        public DateTime completion_date { get; set; }
        public int percentage { get; set; }
        public float cgpa { get; set; }
        public virtual Profile Profile { get; set; }

    }
}