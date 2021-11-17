using System;
using System.Collections.Generic;
using System.Text;
using ExpenseClaim.IRepositories.IRepositories;
using ExpenseClaim.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExpenseClaim.Repositories.Repositories
{
    public class ClaimRepository : BaseRepository<Claim>, IClaimRepository
    {
        public ClaimRepository(ClaimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Claim> GetClaimDetailsById(int claimId)
        {
            var claim = await _dbContext.ExpenseClaims.Include(x => x.ClaimItemList).FirstOrDefaultAsync(x => x.ClaimId == claimId);
            return claim;
        }
    }
}
