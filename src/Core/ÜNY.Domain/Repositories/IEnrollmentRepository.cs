using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;

namespace URL.Domain.Repositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task<List<Enrollment>> GetEnrollmentsByUserIdAsync(Guid userId);
        Task AddEnrollmentsAsync(List<Enrollment> enrollments);
        Task<List<Enrollment>> GetPendingEnrollmentsAsync();
        Task<Enrollment?> GetEnrollmentByIdAsync(Guid id);
        Task UpdateEnrollmentAsync(Enrollment enrollment);
        Task<List<Enrollment?>> GetEnrolledStudentsAsync(Guid examId);
    }
}
