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
    public class FeeİnformationRepository : GenericRepository<Feeİnformation,DataContext>,IFeeİnformationRepository
    {
        private DataContext _context;

        public FeeİnformationRepository(DataContext context):base(context) 
        {
            _context = context;
        }
    }
}
