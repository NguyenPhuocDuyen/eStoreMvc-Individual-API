using BusinessObject;
using DataAccess.DAO;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> GetOrders() => OrderDAO.GetOrders();
        public Order GetOrderById(int id) => OrderDAO.FindOrderById(id);
        public void SaveOrder(Order order) => OrderDAO.SaveOrder(order);
        public void UpdateOrder(Order order) => OrderDAO.UpdateOrder(order);
        public void DeleteOrder(Order order) => OrderDAO.DeleteOrder(order);
    }
}
