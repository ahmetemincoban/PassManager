using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PassManager.Models;

namespace PassManager.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }

        public DbSet<PassManager.Models.PassModel> Passwords { get; set; }
        public DbSet<PassManager.Models.GroupModel> Groups { get; set; }
    }
}
