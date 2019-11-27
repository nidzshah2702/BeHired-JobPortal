using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BeHired.Models
{
    public class UserAccount:IdentityUser
    {
        
        public string user_type { get; set; }
        public string date_of_birth { get; set; }
        public string gender { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string user_image { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }
        public virtual ICollection<Company> Company { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserAccount> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}