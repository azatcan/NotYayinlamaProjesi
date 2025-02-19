using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;

namespace ÜNY.Application.DTOs
{
    public class UnitİnformationDTO
    {
        public Guid Id { get; set; }
        public string FacultyName { get; set; }
        public string UnitName { get; set; }
        public List<CourseUnitInformation> CourseUnitInformations { get; set; }
    }
}
