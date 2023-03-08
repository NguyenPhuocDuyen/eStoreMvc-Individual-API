using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        List<Order> GetOrdersByUserId(string userId);
        Order GetOrderById(int id);
        void SaveOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
