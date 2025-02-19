using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Domain.Entities
{
    public class CourseUnitInformation
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public virtual Courses Course { get; set; }
        public Guid UnitInformationId { get; set; }
        public virtual Unitİnformation Unitİnformation { get; set; }
    }
}
