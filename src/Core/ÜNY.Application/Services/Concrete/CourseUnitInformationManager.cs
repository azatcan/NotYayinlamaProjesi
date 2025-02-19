using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Domain.Repositories;

namespace ÜNY.Application.Services.Concrete
{
    public class CourseUnitInformationManager : ICourseUnitInformationService
    {
        private readonly ICourseUnitInformationRepository _courseUnitInformationRepository;
        private readonly UserManager<Users> _userManager;

        public CourseUnitInformationManager(ICourseUnitInformationRepository courseUnitInformationRepository, UserManager<Users> userManager)
        {
            _courseUnitInformationRepository = courseUnitInformationRepository;
            _userManager = userManager;
        }

        public async Task AddAsync(CourseUnitInformationDTO entityDto)
        {
            var coursesUnitInformation = new CourseUnitInformation
            {
                Id = Guid.NewGuid(),
                CourseId = entityDto.CourseId,
                UnitInformationId = entityDto.UnitInformationId,
            };

            await _courseUnitInformationRepository.AddAsync(coursesUnitInformation);
        }

        public Task<bool> AddCourseToUnitAsync(CourseUnitInformationDTO model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CourseUnitInformationDTO?> FindAsync(Expression<Func<CourseUnitInformation, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseUnitInformationDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CourseUnitInformationDTO?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _courseUnitInformationRepository.SaveChangesAsync();
        }

        public Task UpdateAsync(CourseUnitInformationDTO entityDto)
        {
            throw new NotImplementedException();
        }
    }
}
