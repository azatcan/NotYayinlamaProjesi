using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Abstract;
using ÜNY.Domain.Data;

namespace ÜNY.Domain.Concrete
{
    public class GenericRepository<T, TContext> : IRepository<T> where T : class, new() where TContext : DataContext,new()
    {
        private DataContext context = new DataContext();
        Microsoft.EntityFrameworkCore.DbSet<T> _object;

        public GenericRepository(DataContext context)
        {
            this.context = context;
            _object = context.Set<T>();
        }

        public void Add(T p)
        {
            _object.Add(p);
            context.SaveChanges();
        }

        public void Delete(T p)
        {
            _object.Remove(p);
            context.SaveChanges();
        }

        public T GetById(Guid id)
        {
            return _object.Find(id);
        }

        public List<T> List()
        {
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T p)
        {
            _object.Update(p);
            context.SaveChanges();
        }
    }
}
