using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }
      
        public int ProfileId { get; set; }
        public string skill_name { get; set; }
        public string skill_level { get; set; }
        public virtual Profile Profile { get; set; }
    }
}