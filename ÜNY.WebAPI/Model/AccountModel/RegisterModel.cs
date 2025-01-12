using Microsoft.AspNetCore.Mvc.Rendering;

namespace ÜNY.WebAPI.Model.AccountModel
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateofBirth { get; set; }
        public string BirthPlace { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public Guid GenderId { get; set; }
        public Guid UnitİnformationId { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public IFormFile ImagePath { get; set; }

        //public IEnumerable<SelectListItem> GenderList { get; set; }
        //public IEnumerable<SelectListItem> UnitList { get; set; }

        //public List<SelectListItem> Units { get; set; }
        //public List<SelectListItem> Genders { get; set; }
    }
}
