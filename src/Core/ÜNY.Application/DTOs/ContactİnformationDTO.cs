using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Application.DTOs
{
    public class ContactİnformationDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Addrees { get; set; }
        public Guid UserId { get; set; }
    }
}
