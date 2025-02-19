using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Domain.Entities;

namespace ÜNY.Application.Services.Abstract
{
    public interface IUserService : IGenericService<Users , UserDTO>
    {
        Task<IEnumerable<UserDTO>> GetPendingusersAsync();
        Task ApproveUserAsync(Guid Id);
        Task<UserDTO?> GetUserByIdAsync(Guid userId);
    }
}
