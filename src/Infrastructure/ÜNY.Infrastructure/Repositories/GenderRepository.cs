using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;
using URL.Domain.Repositories;

namespace URL.Infrastructure.Repositories
{
    public class GenderRepository : GenericRepository<Gender,DataContext>,IGenderRepository
    {
        private DataContext _context;

        public GenderRepository(DataContext context) : base(context) 
        {
            _context = context;
        }
    }
}
