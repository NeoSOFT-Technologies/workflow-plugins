using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketProcessingRestApi.Entities;

namespace TicketProcessingRestApi.IRepository.IRepository
{
    public interface ITicketRepository : IAsyncRepository<Ticket>
    {

    }
}
