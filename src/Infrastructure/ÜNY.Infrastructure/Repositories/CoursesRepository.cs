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
    public class CoursesRepository : GenericRepository<Courses,DataContext>,ICoursesRepository
    {
        private readonly DataContext _context;

        public CoursesRepository(DataContext context):base(context) 
        {
            _context = context;
        }
    }
}
