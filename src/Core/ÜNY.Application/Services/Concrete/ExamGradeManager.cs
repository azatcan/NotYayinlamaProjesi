using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

namespace ÜNY.Application.Services.Concrete
{
    public class ExamGradeManager : IExamGradeService
    { 
        private readonly IExamGradeRepository _repository;
        private readonly UserManager<Users> _userManager;
        private readonly IMapper _mapper;

        public ExamGradeManager(IExamGradeRepository repository, UserManager<Users> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public Task AddAsync(ExamGradeDTO entityDto)
        {
            throw new NotImplementedException();
        }

        public async Task AddGrade(AdminExamGradeDTO model)
        {       
            var grade = _mapper.Map<ExamGrade>(model);
            await _repository.AddAsync(grade);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ExamGradeDTO?> FindAsync(Expression<Func<ExamGrade, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExamGradeDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ExamGradeDTO?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ExamGradeDTO> GetUserCoursesWithGradesAsync(Guid userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId.ToString());
            if (currentUser == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }

            var examGrade = await _repository.GetUserCoursesWithGradesAsync(currentUser.Id);
            if (examGrade == null) 
            {
                throw new NotFoundException("Kullanıcıya ait sınav bilgisi.");
            }

            var examGradeDTO = _mapper.Map<ExamGradeDTO>(examGrade);
            return examGradeDTO;
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }

        public Task UpdateAsync(ExamGradeDTO entityDto)
        {
            throw new NotImplementedException();
        }
    }
}
