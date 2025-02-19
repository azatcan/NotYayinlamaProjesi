using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Domain.Repositories;
using URL.Domain.Repositories;

namespace ÜNY.Application.Services.Concrete
{
    public class ExamManager : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamManager(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(ExamDTO examDto)
        {
            if (examDto == null)
                throw new ArgumentException("Invalid data.");

            var exam = new Exam
            {
                Id = Guid.NewGuid(),
                Name = examDto.ExamName,
                Date = examDto.ExamDate,
                CourseId = examDto.CourseId,

            };

            await _examRepository.AddAsync(exam);
        }

        public async Task DeleteAsync(Guid id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam != null)
            {
                await _examRepository.DeleteAsync(exam);
            }
        }

        public async Task<ExamDTO?> FindAsync(Expression<Func<Exam, bool>> predicate)
        {
            var exam = await _examRepository.FindAsync(predicate);
            return _mapper.Map<ExamDTO>(exam);
        }

        public async Task<IEnumerable<ExamDTO>> GetAllAsync()
        {
            var exams = await _examRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ExamDTO>>(exams);
        }

        public async Task<ExamDTO?> GetByIdAsync(Guid id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            return _mapper.Map<ExamDTO>(exam);
        }

        public async Task<IEnumerable<CourseExamDTO>> GetByUserExamsAsync()
        {
            var examUser = await _examRepository.GetuserExamsAsync();
            return _mapper.Map<IEnumerable<CourseExamDTO>>(examUser);
        }

        public async Task SaveChangesAsync()
        {
            await _examRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ExamDTO examDto)
        {
            var exam = await _examRepository.GetByIdAsync(examDto.ExamId);
            if (exam != null)
            {
                _mapper.Map(examDto, exam);
                await _examRepository.UpdateAsync(exam);
            }
        }
    }
}
