using InvoiceCustomerApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceCustomerApi.IRepositories
{
    public interface IInvoiceRepository
    {
        Task<IReadOnlyList<Invoice>> GetInvoice();
        Task<Invoice> AddInvoice(Invoice invoice);
        Task UpdateInvoice(Invoice invoice);
        Task<Invoice> GetInvoiceById(Guid id);
        Task<bool> checkInvoice(Guid id);
    }
}
