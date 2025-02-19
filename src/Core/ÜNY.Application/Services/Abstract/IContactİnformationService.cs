using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Domain.Entities;

namespace ÜNY.Application.Services.Abstract
{
    public interface IContactİnformationService : IGenericService<Contactİnformation, ContactİnformationDTO>
    {
        Task<ContactİnformationDTO> AddContactInformationAsync(ContactİnformationDTO entityDto, Guid userId);
        Task<ContactİnformationDTO> UpdateContactInformationAsync(ContactİnformationDTO model, Users user);
    }
}
