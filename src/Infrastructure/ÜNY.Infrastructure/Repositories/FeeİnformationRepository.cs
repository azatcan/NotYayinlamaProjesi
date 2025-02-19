using Microsoft.EntityFrameworkCore;
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
    public class FeeİnformationRepository : GenericRepository<Feeİnformation, DataContext>, IFeeİnformationRepository
    {
        private readonly DataContext _context;

        public FeeİnformationRepository(DataContext context):base(context) 
        {
            _context = context;
        }

        public async Task<Feeİnformation> GetUserFeeByIdAsync(Guid userId)
        {
            var feeİnfo =  await _context.Feeİnformation
            .Include(f => f.User)  
            .Where(f => f.UserId == userId)
            .FirstOrDefaultAsync();

            return feeİnfo;
        }
    }
}
