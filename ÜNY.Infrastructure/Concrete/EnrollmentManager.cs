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
    public class EnrollmentManager : IEnrollmentService
    {
        IEnrollmentRepository _enrollmentRepository;

        public EnrollmentManager(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public void Add(Enrollment p)
        {
            _enrollmentRepository.Add(p);
        }

        public void Delete(Enrollment p)
        {
            _enrollmentRepository?.Delete(p);
        }

        public Enrollment GetById(Guid id)
        {
            return _enrollmentRepository.GetById(id);
        }

        public List<Enrollment> List()
        {
            return _enrollmentRepository.List();
        }

        public List<Enrollment> List(Expression<Func<Enrollment, bool>> filter)
        {
            return _enrollmentRepository.List(filter);
        }

        public void Update(Enrollment p)
        {
            _enrollmentRepository.Update(p);
        }
    }
}
