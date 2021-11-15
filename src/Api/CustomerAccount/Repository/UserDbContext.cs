using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserDbContext :DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserDetails> UsersDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var firstUserGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var secondUserGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");

            modelBuilder.Entity<UserDetails>().HasData(new UserDetails
            {
                UserId = firstUserGuid,
                Name = "Vivek",
                Email = "vivek@xyz.com",
                MobileNumber = "7754951743",
                AccountNumber = "721702010010565",
                DocumentName = "Adhar Card"
                

            }, new UserDetails
            {
                UserId = secondUserGuid,
                Name = "Amisha",
                Email = "amisha@xyz.com",
                MobileNumber = "7754951745",
                AccountNumber = "721702010010566",
                DocumentName = "Adhar Card"
            });
        }
    }
}
