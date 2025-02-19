using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Application.Exceptions;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using URL.Domain.Repositories;


namespace URL.Application.Services.Concrete
{
    public class EnrollmentManager : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly UserManager<Users> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitİnformationRepository _unit;

        public EnrollmentManager(IEnrollmentRepository enrollmentRepository, UserManager<Users> userManager, IMapper mapper, IUnitİnformationRepository unit)
        {
            _enrollmentRepository = enrollmentRepository;
            _userManager = userManager;
            _mapper = mapper;
            _unit = unit;
        }

        public Task AddAsync(EnrollmentDTO entityDto)
        {
            throw new NotImplementedException();
        }

        public async Task ApproveEnrollmentAsync(EnrollAproveDTO model)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(model.Id);

            if (enrollment == null)
                throw new KeyNotFoundException("Enrollment not found.");

            enrollment.Status = true;
            await _enrollmentRepository.UpdateEnrollmentAsync(enrollment);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool Success, string Message, List<Guid> EnrolledCourses)> EnrollCoursesAsync(EnrollmentDTO model, ClaimsPrincipal user)
        {
            if (model.CourseIds == null || !model.CourseIds.Any())
            {
                return (false, "No courses selected.", null);
            }

            var currentUser = await _userManager.GetUserAsync(user);
            if (currentUser == null)
            {
                return (false, "User not found.", null);
            }

            var userUnit = await _unit.GetUserUnitInformationAsync(currentUser.Id);

            if (userUnit == null)
            {
                return (false, "User's unit information not found.", null);
            }

            var allowedCourseIds = userUnit.CourseUnitInformations.Select(cui => cui.CourseId).ToList();
            var invalidCourses = model.CourseIds.Except(allowedCourseIds).ToList();
            if (invalidCourses.Any())
            {
                return (false, "Some selected courses are not available for the user's unit.", invalidCourses);
            }

            var enrollments = model.CourseIds.Select(courseId => new Enrollment
            {
                Id = Guid.NewGuid(),
                UserId = currentUser.Id,
                CourseId = courseId,
                Status = false,
            }).ToList();

            await _enrollmentRepository.AddEnrollmentsAsync(enrollments);

            return (true, "Courses enrolled successfully. Waiting for admin approval.", enrollments.Select(e => e.CourseId).ToList());
        }
        

        public Task<EnrollmentDTO?> FindAsync(Expression<Func<Enrollment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnrollmentDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EnrollmentDTO?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EnrollDTO?>> GetEnrolledStudentsAsync(Guid examId)
        {
            var enrollments = await _enrollmentRepository.GetEnrolledStudentsAsync(examId);

            if (enrollments == null || !enrollments.Any())
            {
                return new List<EnrollDTO>(); 
            }

            return enrollments
                .Select(en => new EnrollDTO
                {
                    UserId = en.UserId,
                    Name = en.Users.Name
                })
                .ToList();

        }

        public async Task<List<Enrollment>> GetPendingEnrollmentsAsync()
        {
            return await _enrollmentRepository.GetPendingEnrollmentsAsync();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EnrollmentDTO entityDto)
        {
            throw new NotImplementedException();
        }
    }
}
