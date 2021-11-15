using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            var emailAddress = Request.Form["comment"];
            // do something with emailAddress
            using (HttpClient client = new HttpClient())
            {

                StringContent httpContent = new StringContent(emailAddress, System.Text.Encoding.UTF8, "text/plain");

                HttpResponseMessage response = await client.PostAsync("https://localhost:44341/api/Invoice/Comment", httpContent);
            }
            return null;
            
        }
    }
}
