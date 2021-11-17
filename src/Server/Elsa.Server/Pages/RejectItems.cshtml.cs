using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ElsaWorkflowServer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ElsaWorkflowServer.Pages
{
    public class RejectItemsModel : PageModel
    {
        public Claim ClaimObj { get; set; }


        public async Task<IActionResult> OnGetAsync(int claimId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(claimId);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.GetAsync("https://localhost:44385/api/ExpenseClaim/GetClaimById?claimId=" + claimId).Result;

                var claim  =  await response.Content.ReadAsStringAsync();
                ClaimObj = JsonConvert.DeserializeObject<Claim>(claim);
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var rejectedItems = Request.Form["RejectedItems"];
            var id = Convert.ToInt32(Request.Form["ClaimId"].ToString());

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(rejectedItems);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = await client.PutAsync("https://localhost:44385/api/ExpenseClaim/RejectedItemsUpdate/" + id, httpContent);
            }
            return RedirectToPage("Message");
        }
    }
}
