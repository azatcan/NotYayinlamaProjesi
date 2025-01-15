using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Domain.Entities
{
    public class Feeİnformation
    {
        public Guid Id { get; set; }

        public int YourReferenceNumber { get; set; }
        public decimal YourCurrentDebt { get; set; }
        public DateTime period {  get; set; }
        public string DebtType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public Guid UserId { get; set; }
        public virtual Users User { get; set; }


    }
}
