using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Users Users { get; set; }
        public Guid CourseId { get; set; }
        public Courses Course { get; set; }
    }
}
