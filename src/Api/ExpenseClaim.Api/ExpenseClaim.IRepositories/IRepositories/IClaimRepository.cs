using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExpenseClaim.Entities;

namespace ExpenseClaim.IRepositories.IRepositories
{
    public interface IClaimRepository : IAsyncRepository<Claim>
    {
       Task<Claim> GetClaimDetailsById(int claimId);
    }
}
