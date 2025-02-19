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
    public class UnitİnformationRepository : GenericRepository<Unitİnformation, DataContext>, IUnitİnformationRepository
    {
        private readonly DataContext _context;

        public UnitİnformationRepository(DataContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Unitİnformation> GetUserUnitInformationAsync(Guid userId)
        {
            return await _context.Unitİnformation
            .Include(u => u.CourseUnitInformations)
            .ThenInclude(cui => cui.Course)
            .FirstOrDefaultAsync(u => u.Users.Any(u => u.Id == userId));
        }
    }
}
