using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Domain.Entities;

namespace ÜNY.Application.Services.Abstract
{
    public interface IExamGradeService : IGenericService<ExamGrade, ExamGradeDTO>
    {
        Task<ExamGradeDTO> GetUserCoursesWithGradesAsync(Guid userId);
        Task AddGrade(AdminExamGradeDTO model);
    }
}
