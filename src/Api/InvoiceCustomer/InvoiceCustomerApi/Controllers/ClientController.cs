using InvoiceCustomerApi.Entities;
using InvoiceCustomerApi.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceCustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _client;
        public ClientController(IClientRepository client)
        {
            _client=client;
        }
        [HttpGet("GetClienytById")]
        public Task<Client> GetClientById(Guid id)
        {
            var client = _client.GetClientById(id);
            return client;
        }
        [HttpGet("GetManagerEmail")]
        public async Task<ActionResult<string>> ManagerEmail(Guid id)
        {
            var managerEmail =await _client.GetManagerEmail(id);
            return Ok(managerEmail);
        }
       
    }
}
