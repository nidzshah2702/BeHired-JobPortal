using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class JobSkillSet
    {
        [Key]
        public int JobSkillId { get; set; }
        [ForeignKey("JobPost")]
        public int JobId { get; set; }
        public string skill { get; set; }
        public string skill_level { get; set; }
        public virtual JobPost JobPost { get; set; }
    }
}