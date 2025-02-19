using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;

namespace URL.Domain.Repositories
{
    public interface IFeeİnformationRepository : IRepository<Feeİnformation>
    {
        Task<Feeİnformation> GetUserFeeByIdAsync(Guid userId);
    }
}
