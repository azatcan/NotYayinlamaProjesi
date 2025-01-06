namespace ÜNY.WebAPI.Model.AccountModel
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string IdNumber { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
