using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;
using ÜNY.Domain.Repositories;
using ÜNY.Infrastructure.Persistence.Data;
using URL.Infrastructure.Repositories;

namespace ÜNY.Infrastructure.Repositories
{
    public class CourseUnitInformationRepository : GenericRepository<CourseUnitInformation,DataContext> , ICourseUnitInformationRepository
    {
        private readonly DataContext _context;

        public CourseUnitInformationRepository(DataContext context) : base(context) 
        {
            _context = context;
        }
    }
}
