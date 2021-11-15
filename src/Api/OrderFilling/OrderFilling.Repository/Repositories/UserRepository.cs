using Microsoft.EntityFrameworkCore;
using OrderFilling.Entities;
using OrderFilling.IRepository.IRepository;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFilling.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly DbContextAll _dbContext;

        public UserRepository(DbContextAll dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> GetById(Guid Id)
        {
            return await _dbContext.Set<Order>().FindAsync(Id);
        }

        public async Task<bool> GetProductInventoryById(Guid ProductId)
        {
            var inventory = await _dbContext.Set<Products>().FindAsync(ProductId);
            if (inventory.Inventory <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> GetUsersByIdAsync(Guid id,Guid ProductId)
        {
            var userdetail =  await _dbContext.Set<Users>().FindAsync(id);
            var credit = await _dbContext.Set<Products>().FindAsync(ProductId);
            if (userdetail.Credit < credit.ProductPrice)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<string> ScheduleOrder(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            var scheduleTime = await _dbContext.Set<Order>().FindAsync(order.OrderId);
            return scheduleTime.DeliveryBy;
        }

        public async Task shipProduct(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
             await _dbContext.SaveChangesAsync();
        }

        public async Task<Users> GetUserById(Guid Id)
        {
            return await _dbContext.Set<Users>().FindAsync(Id);
        }

        public async Task<Products> GetProductsById(Guid Id)
        {
            return await _dbContext.Set<Products>().FindAsync(Id);
        }

        public async Task ReduceInventory(Products prod)
        {
            var prods = prod;
            var inv = prods.Inventory;
            if (inv > 0)
            {
                inv = inv - 1;
            }
            prods.Inventory = inv;
            _dbContext.Entry(prods).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
