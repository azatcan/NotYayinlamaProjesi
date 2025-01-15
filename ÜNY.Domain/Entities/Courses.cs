using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ÜNY.Domain.Entities
{
    public class Courses
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [JsonIgnore]
        public virtual ICollection<CourseUnitInformation> CourseUnitInformations { get; set; }
    }
}
