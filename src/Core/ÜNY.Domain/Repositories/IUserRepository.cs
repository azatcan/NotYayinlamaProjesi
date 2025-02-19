using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;
using URL.Domain.Repositories;

namespace ÜNY.Domain.Repositories
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<IEnumerable<Users>> GetPendingusersAsync();
        Task ApproveUserAsync(Guid Id);
        Task<Users?> GetUserByIdFeeAsync(Guid userId);
        Task<Users?> GetUserByIdAsync(Guid userId);
    }
}
