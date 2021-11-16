using HireEmployee.Entities;
using HireEmployee.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
            //HttpClient is used to send response to workflow instance
            using (HttpClient client = new HttpClient())
            {
                string json = JsonSerializer.Serialize(res);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response1 = client.PostAsync("https://localhost:5001/jobAdded", httpContent).Result;
            }
            return $"Job Added with Guid {res.Id}";
        }
    }
}
