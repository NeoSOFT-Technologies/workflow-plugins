using AutoMapper;
using InvoiceCustomerApi.Entities;
using InvoiceCustomerApi.IRepositories;
using InvoiceCustomerApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvoiceCustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoicerepo;
        private readonly IMapper _mapper;
        public InvoiceController(IInvoiceRepository invoicerepo, IMapper mapper)
        {
            _invoicerepo = invoicerepo;
            _mapper = mapper;
        }
        [HttpPost("AddInvoice")]
        public async Task<Invoice> Create(InvoiceDto invoice)
        {
            invoice.Date = DateTime.Now.Date;
            var invoicet = _mapper.Map<Invoice>(invoice);
            var invoices = await _invoicerepo.AddInvoice(invoicet);
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(invoices);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = client.PostAsync("https://localhost:44391/addinvoices", httpContent).Result;
            }
            return invoices;
        }
        [HttpPut("UpdateInvoice")]
        public async Task<ActionResult> Update(Invoice invoice)
        {

            _mapper.Map(invoice, typeof(InvoiceDto), typeof(Invoice));
            await _invoicerepo.UpdateInvoice(invoice);
            if (invoice.IsInvoicePaid == false)
            {
                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(invoice);

                    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("X-Correlation-Id", invoice.InvoiceId.ToString());
                    HttpResponseMessage response1 = await client.PutAsync("https://localhost:44391/updateAmountInvoice", httpContent);
                }
            }
            return Ok(invoice);
        }

        [HttpGet("checkinvoice")]
        public async Task<ActionResult<bool>> checkInvoice(Guid id)
        {
            var result = await _invoicerepo.checkInvoice(id);
            return Ok(result);
        }

        [HttpPut("UpdateIspaid")]
        public async Task<ActionResult> UpdateIsPaid(Invoice invoice)
        {
            invoice.IsInvoicePaid = true;
            _mapper.Map(invoice, typeof(InvoiceDto), typeof(Invoice));
            await _invoicerepo.UpdateInvoice(invoice);
            return Ok();
        }
     
        [HttpPost("Comment")]
        public IActionResult CommentToUpdate(string comment)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(comment);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = client.PostAsync("https://localhost:44391/comments", httpContent).Result;
            }
            return Ok();
        }


    }
}
