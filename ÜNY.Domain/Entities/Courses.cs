using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Domain.Entities
{
    public class Courses
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseUnitInformation> CourseUnitInformations { get; set; }
    }
}
