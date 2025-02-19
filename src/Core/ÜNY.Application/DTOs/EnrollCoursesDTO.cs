using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Application.DTOs
{
    public class EnrollCoursesDTO
    {
        public string UnitName { get; set; }
        public List<CoursesDTO> AvailableCourses { get; set; }
    }
}
