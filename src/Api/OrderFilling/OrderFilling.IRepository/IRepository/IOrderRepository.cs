using OrderFilling.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFilling.IRepository.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order entity);
    }
}
