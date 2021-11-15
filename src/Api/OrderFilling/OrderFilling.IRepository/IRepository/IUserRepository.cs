using OrderFilling.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFilling.IRepository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> GetUsersByIdAsync(Guid id,Guid ProductId);
        Task<bool> GetProductInventoryById(Guid ProductId);
        Task<string> ScheduleOrder(Order order);
        Task<Order> GetById(Guid Id);
        Task shipProduct(Order order);
        Task<Users> GetUserById(Guid Id);

        Task<Products> GetProductsById(Guid Id);
        Task ReduceInventory(Products prod);
    }
}
