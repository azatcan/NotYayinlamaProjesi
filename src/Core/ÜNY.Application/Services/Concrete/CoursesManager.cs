using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Application.Exceptions;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Domain.Repositories;
using URL.Domain.Repositories;


namespace URL.Application.Services.Concrete
{
    public class CoursesManager : ICoursesService
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IMapper _mapper;

        public CoursesManager(ICoursesRepository coursesRepository, IMapper mapper)
        {
            _coursesRepository = coursesRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CoursesDTO entityDto)
        {
            Courses courses = new Courses
            {
                Id = Guid.NewGuid(),
                CourseName = entityDto.CourseName,
                Enrollments = new List<Enrollment>(), 
                CourseUnitInformations = new List<CourseUnitInformation>(), 
                Exams = new List<Exam>()
            };
            await _coursesRepository.AddAsync(courses);
        }

        public async Task DeleteAsync(Guid id)
        {
            var findİd =await  _coursesRepository.GetByIdAsync(id);
            if (findİd == null)
            {
                throw new NotFoundException("Courses Not Found");
            }
            await _coursesRepository.DeleteAsync(findİd);
        }

        public Task<CoursesDTO?> FindAsync(Expression<Func<Courses, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CoursesDTO>> GetAllAsync()
        {
            var courses = await _coursesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CoursesDTO>>(courses);
        }

        public async Task<CoursesDTO?> GetByIdAsync(Guid id)
        {
            var courses = await _coursesRepository.GetByIdAsync(id);
            return _mapper.Map<CoursesDTO>(courses);
        }

        public async Task SaveChangesAsync()
        {
            await _coursesRepository.SaveChangesAsync();
        }

        public Task UpdateAsync(CoursesDTO entityDto)
        {
            throw new NotImplementedException();
        }
    }
}
