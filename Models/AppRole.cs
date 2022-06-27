using System;
using Microsoft.AspNetCore.Identity;

namespace PassManager.Models
{
    public class AppRole : IdentityRole
    {
        public DateTime CreatedDate { get; set; }=DateTime.Now;
    }
}