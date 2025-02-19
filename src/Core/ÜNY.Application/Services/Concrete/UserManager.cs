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

namespace ÜNY.Application.Services.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task AddAsync(UserDTO entityDto)
        {
            throw new NotImplementedException();
        }

        public async Task ApproveUserAsync(Guid Id)
        {
            await _userRepository.ApproveUserAsync(Id);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO?> FindAsync(Expression<Func<Users, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public Task<UserDTO?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDTO>> GetPendingusersAsync()
        {
            var user = await _userRepository.GetPendingusersAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(user);
        }

        public async Task<UserDTO?> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            var userDTO = new UserDTO
            {
                BirthPlace = user.BirthPlace,
                ImagePath = user.ImagePath,
                DateofBirth = user.DateofBirth,
                FatherName = user.FatherName,
                GenderName = user.Gender.Name,
                IdNumber = user.IdNumber,
                MotherName = user.MotherName,
                Id = user.Id,
                Name = user.Name,
                SurName = user.SurName,
                UnitName = user.Unitİnformation.UnitName,
                FacultyName = user.Unitİnformation.FacultyName,
                Email = user.Contactİnformation?.Email,
                Phone = user.Contactİnformation?.Phone,
                Addrees = user.Contactİnformation?.Addrees,
            };

            return userDTO;
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserDTO entityDto)
        {
            throw new NotImplementedException();
        }
    }
}
