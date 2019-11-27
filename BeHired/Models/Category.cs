using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string category_name { get; set; }
        public virtual ICollection<JobPost> JobPosts { get; set; }
    }
}