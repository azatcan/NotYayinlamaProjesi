using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Domain.Entities;

namespace ÜNY.Application.Services.Abstract
{
    public interface IEnrollmentService : IGenericService<Enrollment , EnrollmentDTO>
    {
        Task<(bool Success, string Message, List<Guid> EnrolledCourses)> EnrollCoursesAsync(EnrollmentDTO model, ClaimsPrincipal user);
        Task<List<Enrollment>> GetPendingEnrollmentsAsync();
        Task ApproveEnrollmentAsync(EnrollAproveDTO model);
        Task<List<EnrollDTO>> GetEnrolledStudentsAsync(Guid examId);
    }
}
