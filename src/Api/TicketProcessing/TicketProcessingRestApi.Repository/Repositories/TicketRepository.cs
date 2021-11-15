using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketProcessingRestApi.Entities;
using TicketProcessingRestApi.Repository;

namespace TicketProcessingRestApi.IRepository.IRepository
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketDbContext dbContext) : base(dbContext)
        {
        }

    }
}
