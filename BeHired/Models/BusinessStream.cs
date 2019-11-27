using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public class BusinessStream
    {
        public int BusinessStreamId { get; set; }
        public string business_stream_name { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}