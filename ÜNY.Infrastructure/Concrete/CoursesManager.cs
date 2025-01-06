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
    public class CoursesManager : ICoursesService
    {
        ICoursesRepository _coursesRepository;

        public CoursesManager(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public void Add(Courses p)
        {
            _coursesRepository.Add(p);
        }

        public void Delete(Courses p)
        {
            _coursesRepository.Delete(p);
        }

        public Courses GetById(Guid id)
        {
            return _coursesRepository.GetById(id);
        }

        public List<Courses> List()
        {
            return _coursesRepository.List();
        }

        public List<Courses> List(Expression<Func<Courses, bool>> filter)
        {
            return _coursesRepository.List(filter);
        }

        public void Update(Courses p)
        {
            _coursesRepository.Update(p);
        }
    }
}
