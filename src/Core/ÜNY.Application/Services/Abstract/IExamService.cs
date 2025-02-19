﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Domain.Entities;

namespace ÜNY.Application.Services.Abstract
{
    public interface IExamService : IGenericService<Exam , ExamDTO>
    {
        Task<IEnumerable<CourseExamDTO>> GetByUserExamsAsync();
    }
}
