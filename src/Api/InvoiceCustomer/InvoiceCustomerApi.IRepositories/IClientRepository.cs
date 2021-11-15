using InvoiceCustomerApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceCustomerApi.IRepositories
{
   public  interface IClientRepository
    {
        Task<Client> GetClientById(Guid id);
        Task<string> GetManagerEmail(Guid id);
    }
}
