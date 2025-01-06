using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;

namespace ÜNY.Domain.Data
{
    public class DataContext : IdentityDbContext<Users,Roles,Guid>
    {
        public DataContext() 
        { 
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Unitİnformation> Unitİnformation {  get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Feeİnformation> Feeİnformation { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Contactİnformation> Contactİnformation { get; set; }
    }
}
