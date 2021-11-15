using Microsoft.EntityFrameworkCore;
using OrderFilling.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFilling.Repository
{
    public class DbContextAll : DbContext
    {
        public DbContextAll(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
