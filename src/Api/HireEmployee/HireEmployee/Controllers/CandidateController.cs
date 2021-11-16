using HireEmployee.Entities;
using HireEmployee.IRepositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HireEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : Controller
    {
        readonly ICandidateRepository _candidateRepository;

        public CandidateController(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        [HttpPost("AddCandidate")]
        public async Task<Candidate> Create(Candidate candidate)
        {
            candidate.Job = null;
            candidate.Review = false;
            candidate.PhoneScreening = false;
            candidate.Interview = false;
            candidate.OfferAccepted = false;
            var candidateResult = await _candidateRepository.AddCandidate(candidate);
            //HttpClient is used to send response to workflow instance
            using (HttpClient client = new HttpClient())
            {
                string json = JsonSerializer.Serialize(candidateResult);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = client.PostAsync("https://localhost:5001/initiateHiring", httpContent).Result;
            }
            return candidateResult;
        }

        [HttpPost("setReviewStatus")]
        public async Task<bool> UpdateReview(Guid id, bool status)
        {
            Candidate candidate = await _candidateRepository.GetByIdAsync(id);
            candidate.Review = status;
            await _candidateRepository.UpdateAsync(candidate);
            using (HttpClient client = new HttpClient())
            {
                string json = JsonSerializer.Serialize(candidate);
                //Headers are used to specify candidate Id for corelate in workflow instance
                client.DefaultRequestHeaders.Add("X-Correlation-Id", candidate.Id.ToString());
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response1 = client.PostAsync("https://localhost:5001/handleReview", httpContent).Result;
            }
            return status;
        }

        [HttpPost("setPhoneScreenStatus")]
        public async Task<bool> UpdatePhoneScreen(Guid id, bool status)
        {
            Candidate candidate = await _candidateRepository.GetByIdAsync(id);
            candidate.PhoneScreening = status;
            await _candidateRepository.UpdateAsync(candidate);
            using (HttpClient client = new HttpClient())
            {
                string json = JsonSerializer.Serialize(candidate);
                client.DefaultRequestHeaders.Add("X-Correlation-Id", candidate.Id.ToString());
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response1 = client.PostAsync("https://localhost:5001/handlePhoneScreening", httpContent).Result;
            }
            return status;
        }


        [HttpPost("ScheduleInterview")]
        public void ScheduleInterview(Guid id, string interviewerName, string interviewerEmail, string dateTime)
        {
            Interview detail = new Interview() { };
            detail.Name = interviewerName;
            detail.Email = interviewerEmail;
            detail.DateNTime = dateTime;
            using (HttpClient client = new HttpClient())
            {
                string json = JsonSerializer.Serialize(detail);
                client.DefaultRequestHeaders.Add("X-Correlation-Id", id.ToString());
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response1 = client.PostAsync("https://localhost:5001/interviewSchedule", httpContent).Result;
            }
        }

        [HttpPost("setInterviewStatus")]
        public async Task<bool> UpdateInterview(Guid id, bool status, string comment)
        {
            Candidate candidate = await _candidateRepository.GetByIdAsync(id);
            candidate.Interview = status;
            //comment can be processed as required
            await _candidateRepository.UpdateAsync(candidate);
            using (HttpClient client = new HttpClient())
            {
                string json = JsonSerializer.Serialize(candidate);
                client.DefaultRequestHeaders.Add("X-Correlation-Id", candidate.Id.ToString());
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response1 = client.PostAsync("https://localhost:5001/handleInterview", httpContent).Result;
            }
            return status;
        }

        [HttpPut("setOfferAcceptedStatus")]
        public async Task<bool> UpdateOfferAccepted(Guid id, bool status)
        {
            Candidate candidate = await _candidateRepository.GetByIdAsync(id);
            candidate.OfferAccepted = status;
            await _candidateRepository.UpdateAsync(candidate);
            return status;
        }

    }
    public class Interview
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string DateNTime { get; set; }
    }
}
