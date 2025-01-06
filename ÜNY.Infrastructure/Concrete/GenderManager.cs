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
    public class GenderManager : IGenderService
    {
        IGenderRepository _genderRepository;

        public GenderManager(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public void Add(Gender p)
        {
            _genderRepository.Add(p);
        }

        public void Delete(Gender p)
        {
            _genderRepository.Delete(p);
        }

        public Gender GetById(Guid id)
        {
            return _genderRepository.GetById(id);
        }

        public List<Gender> List()
        {
            return _genderRepository.List();
        }

        public List<Gender> List(Expression<Func<Gender, bool>> filter)
        {
            return _genderRepository.List(filter);
        }

        public void Update(Gender p)
        {
            _genderRepository.Update(p);
        }
    }
}
