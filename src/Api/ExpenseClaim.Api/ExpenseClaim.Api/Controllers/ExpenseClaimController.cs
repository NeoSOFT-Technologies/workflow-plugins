using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseClaim.Entities;
using ExpenseClaim.IRepositories.IRepositories;
using System.Net.Http;
using Newtonsoft.Json;

namespace ExpenseClaim.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseClaimController : ControllerBase
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IClaimTypeRepository _claimTypeRepository;

        public ExpenseClaimController(IClaimRepository claimRepository, IClaimTypeRepository claimTypeRepository)
        {
            _claimRepository = claimRepository;
            _claimTypeRepository = claimTypeRepository;
    }


        [HttpGet("GetClaimById")]
        public async Task<ActionResult> GetById(int claimId)
        {
            var response = await _claimRepository.GetClaimDetailsById(claimId);
            return Ok(response);
        }

        [HttpGet("GetClaimTypesById")]
        public async Task<ActionResult> GetClaimTypesById(int claimTypeId)
        {
            var response = await _claimTypeRepository.GetByIdAsync(claimTypeId);
            return Ok(response);
        }


        [HttpPost("AddExpenseClaim")]
        public async Task<Claim> Create(Claim claimObj)
        {
            var response =  await _claimRepository.AddAsync(claimObj);
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(response);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = client.PostAsync("https://localhost:5001/approve-expense-claim", httpContent).Result;
            }
            return response;
        }



        [HttpPut("UpdateExpenseClaimById")]
        public async Task<ActionResult> UpdateExpenseClaimById(int claimId)
        {
            Claim claimObj = await _claimRepository.GetClaimDetailsById(claimId);
            claimObj.SanctionedAmount = claimObj.TotalClaimAmount;
            claimObj.IsSanctioned = true;
            await _claimRepository.UpdateAsync(claimObj);

            foreach (var item in claimObj.ClaimItemList)
            {
                item.IsSanctioned = true;
                await _claimTypeRepository.UpdateAsync(item);
            }
            
            return Ok(claimObj);
        }


        [HttpPut("RejectedItemsUpdate/{claimId}")]
        public async Task<ActionResult> RejectedItemsUpdate(int claimId, [FromBody]IEnumerable<string> claimTypeIds)
        {
            Claim claimObj = await _claimRepository.GetClaimDetailsById(claimId);
            List<int> rejectedItems = new List<int>();
            foreach(var id in claimTypeIds)
            {
                int temp = Convert.ToInt16(id);
                rejectedItems.Add(temp);
            }

            foreach(var item in claimObj.ClaimItemList)
            {
                item.IsSanctioned = true;
                await _claimTypeRepository.UpdateAsync(item);
            }

            decimal rejectedAmount = 0;
            foreach (var id in rejectedItems)
            {
                ClaimType response = await _claimTypeRepository.GetByIdAsync(id);
                response.IsSanctioned = false;
                rejectedAmount = rejectedAmount + response.ClaimAmount;
                await _claimTypeRepository.UpdateAsync(response);
            }

            claimObj.SanctionedAmount = claimObj.TotalClaimAmount - rejectedAmount;
            claimObj.IsSanctioned = true;
            await _claimRepository.UpdateAsync(claimObj);

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(claimObj);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = await client.PostAsync("https://localhost:5001/rejected-items", httpContent);
            }
            return Ok(claimObj);

        }
    }
}
