using ÜNY.Domain.Entities;

namespace ÜNY.WebAPI.Model.AdminExamModel
{
    public class AdminExemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Guid CourseId { get; set; }
    }
}
