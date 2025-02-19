using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;
using URL.Domain.Repositories;

namespace URL.Infrastructure.Repositories
{
    public class EnrolmentRepository : GenericRepository<Enrollment, DataContext>, IEnrollmentRepository
    {
        private readonly DataContext _context;

        public EnrolmentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddEnrollmentsAsync(List<Enrollment> enrollments)
        {
            await _context.Enrollment.AddRangeAsync(enrollments);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Enrollment?>> GetEnrolledStudentsAsync(Guid examId)
        {
            return await _context.Enrollment
                .Where(e => e.Course.Exams.Any(ex => ex.Id == examId) && e.Status)
                .ToListAsync();
        }

        public async Task<Enrollment?> GetEnrollmentByIdAsync(Guid id)
        {
            return await _context.Enrollment
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Enrollment>> GetEnrollmentsByUserIdAsync(Guid userId)
        {
            return await _context.Enrollment.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task<List<Enrollment>> GetPendingEnrollmentsAsync()
        {
            return await _context.Enrollment
             .Where(e => !e.Status)
             .ToListAsync();
        }

        public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollment.Update(enrollment);
            await _context.SaveChangesAsync();
        }
    }
}
