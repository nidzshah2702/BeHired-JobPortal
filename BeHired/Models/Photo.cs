using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeHired.Models
{
    public enum FileType
    {
        Avatar = 1, Photo
    }
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}