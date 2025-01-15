using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Domain.Entities
{
    public class Exam
    {
        public Guid Id { get; set; }
        public string Name { get; set; }  
        public DateTime Date { get; set; }
        public Guid CourseId { get; set; }
        public virtual Courses Course { get; set; }
    }
}
