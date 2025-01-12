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
        public string IdNumber { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? BirthPlace { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public Guid GenderId { get; set; }
        public Gender Gender { get; set; }
        public Guid UnitİnformationId { get; set; }
        public bool IsApproved { get; set; }
        public Unitİnformation Unitİnformation { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Contactİnformation> Contactİnformation { get; set; }
        public ICollection<Feeİnformation> Feeİnformation { get; set; }

        public string ImagePath { get; set; }     
    }
}
