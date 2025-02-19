using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Application.Exceptions;
using ÜNY.Domain.Entities;
using ÜNY.Domain.Repositories;
using ÜNY.Infrastructure.Persistence.Data;
using URL.Infrastructure.Repositories;

namespace ÜNY.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<Users, DataContext>, IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext) : base(dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task ApproveUserAsync(Guid Id)
        {
            var user = _dataContext.Users.SingleOrDefault(u => u.Id == Id);
            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı");

            user.IsApproved = true;
            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Users>> GetPendingusersAsync()
        {
            var pendingUsers = await _dataContext.Users
                .Where(u => u.IsApproved == false)
                .Select(u => new Users
                {
                    Id = u.Id,
                    Name= u.Name,
                    SurName =u.SurName,
                    UserName = u.UserName,
                    BirthPlace = u.BirthPlace,
                    DateofBirth = u.DateofBirth
                })
                .ToListAsync();

            return pendingUsers;
        }

        public async Task<Users?> GetUserByIdAsync(Guid userId)
        {
            var userWithDetails = await _dataContext.Users
                .Include(u => u.Unitİnformation)
                .Include(u => u.Contactİnformation)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (userWithDetails == null)
            {
                throw new NotFoundException("UserDetails Not Found!");
            }

            return userWithDetails; 
        }

        public async Task<Users?> GetUserByIdFeeAsync(Guid userId)
        {
            return await _dataContext.Users
            .Include(u => u.Feeİnformation)
            .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
