using ÜNY.WebAPI.DTOs.Base;

namespace ÜNY.WebAPI.DTOs
{
    public class UserDTO : BaseDTO
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UnitName { get; set; }
        public string BirthPlace { get; set; }
        public string ImagePath { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string IdNumber { get; set; }
        public string MotherName { get; set; }
        public string FacultyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Addrees { get; set; }

    }
}
