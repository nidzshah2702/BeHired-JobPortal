using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class JobApplication
    {
        public int JobApplicationId{get; set;}
        [ForeignKey("Profile")]
        [Column(Order = 1)]
        public int ProfileId { get; set; }
        [ForeignKey("JobPost")]
        [Column(Order = 2)]
        public int JobId { get; set; }
        [DefaultValue("UnderReview")]
        public string status { get; set; }
        [DefaultValue("yes")]

        public string neworold { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual JobPost JobPost { get; set; }

    }
}