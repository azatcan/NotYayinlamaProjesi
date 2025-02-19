using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;
using ÜNY.Domain.Repositories;
using ÜNY.Infrastructure.Persistence.Data;

namespace URL.Infrastructure.Repositories
{
    public class ContactİnformationRepository : GenericRepository<Contactİnformation,DataContext>,IContactİnformationRepository
    {
        private readonly DataContext _context;

        public ContactİnformationRepository(DataContext context) : base(context) 
        {
            _context = context;
        }
    }
}
