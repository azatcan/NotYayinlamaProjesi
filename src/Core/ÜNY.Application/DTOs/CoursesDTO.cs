using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;

namespace ÜNY.Application.DTOs
{
    public class CoursesDTO
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        //[JsonIgnore]
        //public virtual ICollection<CourseUnitInformation> CourseUnitInformations { get; set; } = new List<CourseUnitInformation>();
        //[JsonIgnore]
        //public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
