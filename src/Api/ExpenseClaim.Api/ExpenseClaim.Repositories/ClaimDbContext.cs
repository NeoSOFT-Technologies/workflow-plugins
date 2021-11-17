using Microsoft.EntityFrameworkCore;
using System;
using ExpenseClaim.Entities;

namespace ExpenseClaim.Repositories
{
    public class ClaimDbContext : DbContext
    {
        public ClaimDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Claim> ExpenseClaims { get; set; }
        public DbSet<ClaimType> ClaimTypes { get; set; }
    }
}
