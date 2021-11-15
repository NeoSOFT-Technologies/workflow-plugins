using InvoiceCustomerApi.Entities;
using InvoiceCustomerApi.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceCustomerApi.Repositories.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DbContexts _context;
        public ClientRepository(DbContexts context)
        {
            _context = context;
        }
        public async Task<Client> GetClientById(Guid id)
        {
            var client1 = await _context.Clients.FindAsync(id);
            return client1;
        }
        public async Task<string> GetManagerEmail(Guid id)
        {
            string managerEmail = "";
            var client1 = await _context.Clients.FindAsync(id);
            var clientId1 = Guid.Parse("{1213179A-7837-473A-A4D5-A5571B43E6A6}");
            var clientId2 = Guid.Parse("{2413179A-7837-493A-A4D5-A5571B43E6A6}");
            if (client1.ClientId == clientId1)
            {
                managerEmail = "manager1@gmail.com";
            }
            else if (client1.ClientId == clientId2)
            {
                managerEmail = "manager2@gmail.com";
            }
            return managerEmail;
        }
    }
}
