using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Infrastructure.Persistence.Data;
using URL.Domain.Repositories;

namespace URL.Infrastructure.Repositories
{
    public class GenericRepository<T, TContext> : IRepository<T> where T : class, new() where TContext : DataContext,new()
    {
        private readonly DataContext context;
        Microsoft.EntityFrameworkCore.DbSet<T> _object;

        public GenericRepository(DataContext context)
        {
            this.context = context;
            _object = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _object.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
             _object.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _object.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _object.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _object.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _object.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
