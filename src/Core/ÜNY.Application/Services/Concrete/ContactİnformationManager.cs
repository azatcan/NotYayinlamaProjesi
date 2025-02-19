using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Domain.Repositories;

namespace URL.Application.Services.Concrete
{
    public class ContactİnformationManager : IContactİnformationService
    {
        private readonly IContactİnformationRepository _contactİnformationRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<Users> _userManager;

        public ContactİnformationManager(IContactİnformationRepository contactİnformationRepository, IMapper mapper, UserManager<Users> userManager)
        {
            _contactİnformationRepository = contactİnformationRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task AddAsync(ContactİnformationDTO entityDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ContactİnformationDTO> AddContactInformationAsync(ContactİnformationDTO entityDto, Guid userId)
        {
            var contact = _mapper.Map<Contactİnformation>(entityDto);
            contact.UserId = userId;
            contact.Id = Guid.NewGuid();
            await _contactİnformationRepository.AddAsync(contact);

            return _mapper.Map<ContactİnformationDTO>(contact);
        }

        public async Task DeleteAsync(Guid id)
        {
            var contact = await _contactİnformationRepository.GetByIdAsync(id);
            if (contact != null)
            {
                await _contactİnformationRepository.DeleteAsync(contact);
            }
        }

        public Task<ContactİnformationDTO?> FindAsync(Expression<Func<Contactİnformation, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContactİnformationDTO>> GetAllAsync()
        {
            var contact = await _contactİnformationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactİnformationDTO>>(contact);
        }

        public async Task<ContactİnformationDTO?> GetByIdAsync(Guid id)
        {
            var contact = await _contactİnformationRepository.GetByIdAsync(id);
            return _mapper.Map<ContactİnformationDTO>(contact);
        }

        public async Task SaveChangesAsync()
        {
            await _contactİnformationRepository.SaveChangesAsync();
        }

        public Task UpdateAsync(ContactİnformationDTO entityDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ContactİnformationDTO> UpdateContactInformationAsync(ContactİnformationDTO model, Users user)
        {
            var contact = await _contactİnformationRepository.GetByIdAsync(user.ContactİnformationId.GetValueOrDefault(Guid.Empty));
            if (contact == null || contact.Id == Guid.Empty)
            {
                return null;
            }

            contact.Email = model.Email;
            contact.Addrees = model.Addrees;
            contact.Phone = model.Phone;

            await _contactİnformationRepository.UpdateAsync(contact);
            await _contactİnformationRepository.SaveChangesAsync();

            var contactDto = _mapper.Map<ContactİnformationDTO>(contact);

            return contactDto;

        }
    }
}
