using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HireEmployee.Entities;

namespace HireEmployee.IRepositories.IRepositories
{
    public interface ICandidateRepository:IAsyncRepository<Candidate>
    {
    public Task<Candidate> AddCandidate(Candidate candidate);
        public Task<IEnumerable<Candidate>> getAllCandidate(Guid id);
    }
}
