using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ÜNY.Domain.Entities
{
    public class Unitİnformation
    {
        public Guid Id { get; set; }
        public string FacultyName { get; set; }
        public string UnitName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Users> Users { get; set; }
        [JsonIgnore]
        public virtual ICollection<CourseUnitInformation> CourseUnitInformations { get; set; }
    }
}
