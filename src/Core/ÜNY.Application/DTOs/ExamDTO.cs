using ÜNY.Domain.Entities;

namespace ÜNY.Application.DTOs
{
    public class ExamDTO
    {
        public Guid ExamId { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
        public Guid CourseId { get; set; }
    }
}
