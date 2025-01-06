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
    public class FeeİnformationManager : IFeeİnformationService
    {
        IFeeİnformationRepository _feeİnformationRepository;

        public FeeİnformationManager(IFeeİnformationRepository feeİnformationRepository)
        {
            _feeİnformationRepository = feeİnformationRepository;
        }

        public void Add(Feeİnformation p)
        {
            _feeİnformationRepository.Add(p);
        }

        public void Delete(Feeİnformation p)
        {
            _feeİnformationRepository.Delete(p);
        }

        public Feeİnformation GetById(Guid id)
        {
            return _feeİnformationRepository.GetById(id);
        }

        public List<Feeİnformation> List()
        {
            return _feeİnformationRepository.List();
        }

        public List<Feeİnformation> List(Expression<Func<Feeİnformation, bool>> filter)
        {
            return _feeİnformationRepository.List(filter);
        }

        public void Update(Feeİnformation p)
        {
            _feeİnformationRepository.Update(p);
        }
    }
}
