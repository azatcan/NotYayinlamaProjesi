using System.ComponentModel.DataAnnotations;
using ÜNY.WebAPI.DTOs;

namespace ÜNY.WebAPI.Model.AdminExamGrade
{
    public class AdminExamGradeViewModel
    {
        public Guid UserId { get; set; }
        public Guid ExamId { get; set; }

        [Range(0, 100)]
        public decimal Grade { get; set; }
    }
}
