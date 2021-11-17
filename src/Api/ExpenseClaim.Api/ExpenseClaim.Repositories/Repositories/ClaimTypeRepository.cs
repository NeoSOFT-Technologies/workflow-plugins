using System;
using System.Collections.Generic;
using System.Text;
using ExpenseClaim.IRepositories.IRepositories;
using ExpenseClaim.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExpenseClaim.Repositories.Repositories
{
    public class ClaimTypeRepository : BaseRepository<ClaimType>, IClaimTypeRepository
    {
        public ClaimTypeRepository(ClaimDbContext dbContext) : base(dbContext)
        {
        }
    }
}
