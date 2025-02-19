using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Domain.Entities;

namespace URL.Domain.Repositories
{
    public interface IUnitİnformationRepository:IRepository<Unitİnformation>
    {
        Task<Unitİnformation> GetUserUnitInformationAsync(Guid userId);
    }
}
