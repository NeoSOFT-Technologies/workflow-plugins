using Microsoft.EntityFrameworkCore;
using System;
using TicketProcessingRestApi.Entities;

namespace TicketProcessingRestApi.Repository
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var firstUserGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var secondUserGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");

            modelBuilder.Entity<Ticket>().HasData(new Ticket
            {
                TicketId = firstUserGuid,
                Priority="High",
                TicketType = "IT",
                createdBy = "Anshul@gmail.com",
                CreateDate =DateTime.Now,
                Discription = "new query",
                Attachment="query.pdf",
                assignManager= "neha.haridas@gmail.com",
                assignPerson= "vinay@xyz.com",
                isResolved=false,

                
            });
        }
    }
}
   
      /*  */

    
