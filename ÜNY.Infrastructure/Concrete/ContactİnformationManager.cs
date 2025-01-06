using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Abstract;

namespace ÜNY.Infrastructure.Concrete
{
    public class ContactİnformationManager : IContactİnformationService
    {
        IContactİnformationRepository _contactİnformationRepository;

        public ContactİnformationManager(IContactİnformationRepository contactİnformationRepository)
        {
            _contactİnformationRepository = contactİnformationRepository;
        }

        public void Add(Contactİnformation p)
        {
            _contactİnformationRepository.Add(p);
        }

        public void Delete(Contactİnformation p)
        {
            _contactİnformationRepository.Delete(p);
        }

        public Contactİnformation GetById(Guid id)
        {
            return _contactİnformationRepository.GetById(id);
        }

        public List<Contactİnformation> List()
        {
            return _contactİnformationRepository.List();
        }

        public List<Contactİnformation> List(Expression<Func<Contactİnformation, bool>> filter)
        {
            return _contactİnformationRepository.List(filter);
        }

        public void Update(Contactİnformation p)
        {
            _contactİnformationRepository.Update(p);
        }
    }
}
