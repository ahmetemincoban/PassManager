using System;
using Microsoft.AspNetCore.Identity;

namespace PassManager.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedDate { get; set; }=DateTime.Now;
    }
}