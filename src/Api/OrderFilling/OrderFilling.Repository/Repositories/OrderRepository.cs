using OrderFilling.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFilling.Repository.Repositories
{
    public class OrderRepository : IRepository.IRepository.IOrderRepository
    {
        protected readonly DbContextAll _dbContext;

        public OrderRepository(DbContextAll dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Order> AddOrderAsync(Order entity)
        {
            await _dbContext.Set<Order>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
