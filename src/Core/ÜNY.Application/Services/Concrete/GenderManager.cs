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
    public class GenderManager : IGenderService
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public GenderManager(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(GenderDTO entityDto)
        {
            Gender gender = new Gender
            {
                Id = Guid.NewGuid(),
                Name = entityDto.Name,
            };
            await _genderRepository.AddAsync(gender);
        }

        public async Task DeleteAsync(Guid id)
        {
            var findId = await _genderRepository.GetByIdAsync(id);
            if (findId != null) 
            {
                throw new NotFoundException("Gender Not Found");
            }
            await _genderRepository.DeleteAsync(findId);
        }

        public Task<GenderDTO?> FindAsync(Expression<Func<Gender, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GenderDTO>> GetAllAsync()
        {
            var gender = await _genderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GenderDTO>>(gender);

        }

        public async Task<GenderDTO?> GetByIdAsync(Guid id)
        {
            var gender = await _genderRepository.GetByIdAsync(id);
            return _mapper.Map<GenderDTO>(gender);
        }

        public async Task SaveChangesAsync()
        {
            await _genderRepository.SaveChangesAsync();
        }

        public Task UpdateAsync(GenderDTO entityDto)
        {
            throw new NotImplementedException();
        }
    }
}
