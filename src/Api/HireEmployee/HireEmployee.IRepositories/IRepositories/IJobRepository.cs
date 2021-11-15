using HireEmployee.IRepositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HireEmployee.Entities;

namespace HireEmployee.IRepositories
{
    public interface IJobRepository: IAsyncRepository<Job>
    {
        public Task<Job> AddJob(Job job);
    }
}
