using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Domain.Entities
{
    public class Users : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string SurName { get; set; }   
        public string UserName { get; set; }
        public string IdNumber { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? BirthPlace { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public Guid? GenderId { get; set; }
        public Gender? Gender { get; set; }
        public Guid? UnitId { get; set; }
        public Unitİnformation? Unit {  get; set; }
        public Guid? CoursesId { get; set; }
        public Courses? courses { get; set; }
        public Guid? ContactId { get; set; }
        public Contactİnformation? Contactİnformation { get; set; }

        public string ImagePath { get; set; }     
    }
}
