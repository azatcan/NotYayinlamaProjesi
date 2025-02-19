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
    public class UnitİnformationManager : IUnitİnformationService
    {
        private readonly IUnitİnformationRepository  _unitİnformationRepository;
        private readonly IMapper _mapper;

        public UnitİnformationManager(IUnitİnformationRepository unitİnformationRepository, IMapper mapper)
        {
            _unitİnformationRepository = unitİnformationRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(UnitİnformationDTO entityDto)
        {
            Unitİnformation unit = new Unitİnformation
            {
                Id = Guid.NewGuid(),
                FacultyName = entityDto.FacultyName,
                UnitName = entityDto.UnitName,
            };
            await _unitİnformationRepository.AddAsync(unit);
        }

        public async Task DeleteAsync(Guid id)
        {
            var findİd = await _unitİnformationRepository.GetByIdAsync(id);
            if (findİd == null)
            {
                throw new NotFoundException("Unit Not found!!");
            }
            await _unitİnformationRepository.DeleteAsync(findİd);
        }

        public Task<UnitİnformationDTO?> FindAsync(Expression<Func<Unitİnformation, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UnitİnformationDTO>> GetAllAsync()
        {
            var unit = await _unitİnformationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UnitİnformationDTO>>(unit);
        }

        public async Task<UnitİnformationDTO?> GetByIdAsync(Guid id)
        {
            var unit = await _unitİnformationRepository.GetByIdAsync(id);
            return _mapper.Map<UnitİnformationDTO>(unit);
        }

        public async Task<UnitİnformationDTO> GetUserUnitInformationAsync(Guid userId)
        {
            var unit = await _unitİnformationRepository.GetUserUnitInformationAsync(userId);
            return _mapper.Map<UnitİnformationDTO>(unit);
        }

        public async Task SaveChangesAsync()
        {
            await _unitİnformationRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UnitİnformationDTO entityDto)
        {
            var unit = await _unitİnformationRepository.GetByIdAsync(entityDto.Id);
            if (unit == null)
            {
                throw new NotFoundException("Unit not found");
            }

            unit.UnitName = entityDto.UnitName;
            unit.FacultyName = entityDto.FacultyName;

            await _unitİnformationRepository.UpdateAsync(unit);
        }
    }
}
