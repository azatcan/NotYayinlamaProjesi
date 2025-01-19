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
        public virtual Users Users { get; set; }
        public Guid CourseId { get; set; }
        public virtual Courses Course { get; set; }
        public bool Status {  get; set; } 
    }
}
