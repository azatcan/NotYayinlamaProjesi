using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
    public class FeeİnformationManager : IFeeİnformationService
    {
        private readonly IFeeİnformationRepository _feeİnformationRepository;
        private readonly IMapper _mapper;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Users> _userManager;
        private readonly IUserRepository _userRepository;

        public FeeİnformationManager(IFeeİnformationRepository feeİnformationRepository, IMapper mapper, UserManager<Users> userManager, IUserRepository userRepository)
        {
            _feeİnformationRepository = feeİnformationRepository;
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task AddAsync(FeeİnfoDTO entityDto)
        {
            if (entityDto == null)
                throw new ArgumentException("Invalid data.");

            var feeInformation = new Feeİnformation
            {
                Id = Guid.NewGuid(),
                YourReferenceNumber = entityDto.YourReferenceNumber,
                YourCurrentDebt = entityDto.YourCurrentDebt,
                period = entityDto.period,
                DebtType = entityDto.DebtType,
                Amount = entityDto.Amount,
                Description = entityDto.Description,
                Status = false,
                UserId = entityDto.UserId
            };

            await _feeİnformationRepository.AddAsync(feeInformation);

            var user = await _userRepository.GetUserByIdFeeAsync(entityDto.UserId);
            if (user != null)
            {
                user.FeeİnformationId = feeInformation.Id;
                await _userManager.UpdateAsync(user);
            }
        }


        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<FeeİnfoDTO?> FindAsync(Expression<Func<Feeİnformation, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FeeİnfoDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FeeİnfoDTO?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<FeeİnfoDTO> GetUserFeeAsync(Guid userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId.ToString());
            if (currentUser == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }

            var fee = await _feeİnformationRepository.GetUserFeeByIdAsync(userId);

            if (fee == null)
            {
                throw new NotFoundException("Kullanıcıya ait borç bilgisi bulunamadı.");
            }

            return new FeeİnfoDTO
            {
                YourCurrentDebt = fee.YourCurrentDebt,
                YourReferenceNumber = fee.YourReferenceNumber,
                period = fee.period,
                DebtType = fee.DebtType,
                Amount = fee.Amount,
                Description = fee.Description,
                Status = fee.Status,
            };
        }

        public async Task SaveChangesAsync()
        {
            await _feeİnformationRepository.SaveChangesAsync();
        }

        public Task UpdateAsync(FeeİnfoDTO entityDto)
        {
            throw new NotImplementedException();
        }
    }
}
