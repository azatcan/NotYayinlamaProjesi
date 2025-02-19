using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Application.Services.Abstract
{
    public interface IGenericService<T, TDto> where T : class, new() where TDto : class, new()
    {
        Task<TDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(TDto entityDto);
        Task UpdateAsync(TDto entityDto);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
