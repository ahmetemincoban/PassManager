using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PassManager.Models;

namespace PassManager.Data
{
    public class MyContext : IdentityDbContext<AppUser, AppRole,string>
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }

        public DbSet<PassManager.Models.PassModel> Passwords { get; set; }
        public DbSet<PassManager.Models.GroupModel> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
        }
    }
}
