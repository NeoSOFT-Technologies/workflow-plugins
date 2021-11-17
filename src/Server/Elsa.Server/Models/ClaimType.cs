using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElsaWorkflowServer.Model
{
    public class ClaimType
    {
        public int ClaimTypeId { get; set; }
        public string ClaimTypeName { get; set; }
        public decimal ClaimAmount { get; set; }
        public string ClaimTypeDescription { get; set; }
        public bool IsSanctioned { get; set; }
    }
}
