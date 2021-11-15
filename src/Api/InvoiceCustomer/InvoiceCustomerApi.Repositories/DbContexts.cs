using InvoiceCustomerApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace InvoiceCustomerApi.Repositories
{
    public class DbContexts:DbContext
    {
        public DbContexts(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Invoice1Guid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var Invoice2Guid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var Client1Guid = Guid.Parse("{1213179A-7837-473A-A4D5-A5571B43E6A6}");
            var Client2Guid = Guid.Parse("{2413179A-7837-493A-A4D5-A5571B43E6A6}");

            modelBuilder.Entity<Invoice>().HasData(
             new Invoice
            {
                InvoiceId = Invoice1Guid,
                ClientId = Client1Guid,
                Amount = 2000,
                NumberofResource = 10,
                Date= DateTime.Now.Date,
                IsInvoicePaid=false
            },
            new Invoice
            {
                InvoiceId = Invoice2Guid,
                ClientId = Client2Guid,
                Amount = 3000,
                NumberofResource = 9,
                Date = DateTime.Now.Date,
                IsInvoicePaid = false
            });
            modelBuilder.Entity<Client>().HasData(
            new Client
            {
                ClientId = Client1Guid,
                ClientName = "Ankit",
                Email = "ankit@gmail.com",
                Phone = "9873892812"
            },
            new Client
            {
                ClientId = Client2Guid,
                ClientName = "Neha",
                Email = "neha@gmail.com",
                Phone = "9173292812"
            });
        }
    }
}
