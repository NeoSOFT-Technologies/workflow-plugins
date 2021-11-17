using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElsaWorkflowServer.Model
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public decimal TotalClaimAmount { get; set; }
        public decimal SanctionedAmount { get; set; }
        public List<ClaimType> ClaimItemList { get; set; }
    }
}
