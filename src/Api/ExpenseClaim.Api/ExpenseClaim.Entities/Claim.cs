using System;
using System.Collections.Generic;

namespace ExpenseClaim.Entities
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public decimal TotalClaimAmount { get; set; }
        public decimal SanctionedAmount { get; set; }
        public bool IsSanctioned { get; set; }
        public List<ClaimType> ClaimItemList { get; set; }
    }
}
