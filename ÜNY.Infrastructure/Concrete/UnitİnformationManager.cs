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
    public class UnitİnformationManager : IUnitİnformationService
    {
        IUnitİnformationRepository  _unitİnformationRepository;

        public UnitİnformationManager(IUnitİnformationRepository unitİnformationRepository)
        {
            _unitİnformationRepository = unitİnformationRepository;
        }

        public void Add(Unitİnformation p)
        {
            _unitİnformationRepository.Add(p);
        }

        public void Delete(Unitİnformation p)
        {
            _unitİnformationRepository.Delete(p);
        }

        public Unitİnformation GetById(Guid id)
        {
            return _unitİnformationRepository.GetById(id);
        }

        public List<Unitİnformation> List()
        {
            return _unitİnformationRepository.List();
        }

        public List<Unitİnformation> List(Expression<Func<Unitİnformation, bool>> filter)
        {
            return _unitİnformationRepository.List(filter);
        }

        public void Update(Unitİnformation p)
        {
            _unitİnformationRepository.Update(p);
        }
    }
}
