using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;
using URL.Domain.Repositories;

namespace ÜNY.Domain.Repositories
{
    public interface IExamRepository : IRepository<Exam>
    {
        Task<IEnumerable<Exam>> GetuserExamsAsync();
    }
}
