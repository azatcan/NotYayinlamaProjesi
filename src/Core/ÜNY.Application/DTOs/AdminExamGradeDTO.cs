using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Application.DTOs
{
    public class AdminExamGradeDTO
    {
        public Guid UserId { get; set; }
        public Guid ExamId { get; set; }

        [Range(0, 100)]
        public decimal Grade { get; set; }
    }
}
