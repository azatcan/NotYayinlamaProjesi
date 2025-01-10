﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            modelBuilder.Entity<Users>()
            .HasOne(u => u.Gender)
            .WithMany(g => g.Users)
            .HasForeignKey(u => u.GenderId);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Unitİnformation)
                .WithMany(ui => ui.Users)
                .HasForeignKey(u => u.UnitİnformationId);

            modelBuilder.Entity<CourseUnitInformation>()
                .HasKey(cui => new { cui.CourseId, cui.UnitInformationId });

            modelBuilder.Entity<CourseUnitInformation>()
                .HasOne(cui => cui.Course)
                .WithMany(c => c.CourseUnitInformations)
                .HasForeignKey(cui => cui.CourseId);

            modelBuilder.Entity<CourseUnitInformation>()
                .HasOne(cui => cui.Unitİnformation)
                .WithMany(ui => ui.CourseUnitInformations)
                .HasForeignKey(cui => cui.UnitInformationId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Users)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Contactİnformation>()
                .HasOne(ci => ci.User)
                .WithMany(u => u.Contactİnformation)
                .HasForeignKey(ci => ci.UserId);

            modelBuilder.Entity<Feeİnformation>()
                .HasOne(fi => fi.User)
                .WithMany(u => u.Feeİnformation)
                .HasForeignKey(fi => fi.UserId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Unitİnformation> Unitİnformation {  get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Feeİnformation> Feeİnformation { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Contactİnformation> Contactİnformation { get; set; }
        public DbSet<CourseUnitInformation> CoursesUnitInformation { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamGrade> ExamGrades { get; set; }

    }
}
