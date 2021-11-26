using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Elsa.Server.Pages
{
    public class CommentToUpdateModel : PageModel
    {
        public Guid InvoiceId { get; set; }
       
        public void OnGetAsync(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var comment = Request.Form["comment"].ToString();
            var invoiceId = Guid.Parse(Request.Form["id"]);

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(comment);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://localhost:44341/api/Invoice/Comment/" + invoiceId, httpContent);
            }
            return null;

        }
    }
}
