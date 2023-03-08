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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.GetOrderDetails();
        public OrderDetail GetOrderDetailById(int orderId, int productId) => OrderDetailDAO.FindOrderDetailById(orderId, productId);
        public void SaveOrderDetail(OrderDetail order) => OrderDetailDAO.SaveOrderDetail(order);
        public void UpdateOrderDetail(OrderDetail order) => OrderDetailDAO.UpdateOrderDetail(order);
        public void DeleteOrderDetail(OrderDetail order) => OrderDetailDAO.DeleteOrderDetail(order);
    }
}
