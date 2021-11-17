using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseClaim.Entities
{
    public class RejectedItems
    {
        public int ClaimId { get; set; }
        public decimal TotalClaimAmount { get; set; }
        public decimal SanctionedAmount { get; set; }

        public int ClaimTypeId { get; set; }
        public string ClaimTypeName { get; set; }
        public decimal ClaimAmount { get; set; }
    }
}
