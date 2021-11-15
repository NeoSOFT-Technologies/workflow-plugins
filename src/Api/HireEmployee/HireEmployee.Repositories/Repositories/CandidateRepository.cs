using HireEmployee.Entities;
using HireEmployee.IRepositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireEmployee.Repositories.Repositories
{
    public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
    {

        public CandidateRepository(HireEmployeeDbContext dbContext) : base(dbContext)
        {
        }


        async Task<Candidate> ICandidateRepository.AddCandidate(Candidate candidate)
        {
            await _dbContext.AddAsync(candidate);
            await _dbContext.SaveChangesAsync();
            return candidate;
        }
        async Task<IEnumerable<Candidate>> getAllCandidate(Guid id)
        {
            var candidateList = await _dbContext.Candidates.Where(x => x.JobId == id).ToListAsync();
            return candidateList;
        }

        Task<IEnumerable<Candidate>> ICandidateRepository.getAllCandidate(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
