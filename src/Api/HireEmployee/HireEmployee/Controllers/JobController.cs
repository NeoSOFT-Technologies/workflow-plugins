using HireEmployee.Entities;
using HireEmployee.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HireEmployee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        readonly IJobRepository _jobRepository;

        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpPost("AddJob")]
        public async Task<string> Create(Job job)
        {
            job.Candidates = null;
            Job res = await _jobRepository.AddJob(job);
            return $"Job Added with Guid {res.Id}";
        }
    }
}
