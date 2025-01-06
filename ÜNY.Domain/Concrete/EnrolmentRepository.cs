using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Abstract;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;

namespace ÜNY.Domain.Concrete
{
    public class EnrolmentRepository : GenericRepository<Enrollment,DataContext>,IEnrollmentRepository
    {
        private DataContext _context;

        public EnrolmentRepository(DataContext context) : base(context) 
        {
            _context = context;
        }
    }
}
