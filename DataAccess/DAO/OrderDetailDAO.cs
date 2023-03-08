using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetOrderDetails()
        {
            var list = new List<OrderDetail>();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.OrderDetails.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static OrderDetail FindOrderDetailById(int orderId, int productId)
        {
            OrderDetail order = new();
            try
            {
                using (var context = new MyDbContext())
                {
                    order = context.OrderDetails.SingleOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public static void SaveOrderDetail(OrderDetail order)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.OrderDetails.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateOrderDetail(OrderDetail order)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry(order).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrderDetail(OrderDetail order)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var x = context.OrderDetails.SingleOrDefault(x => x.OrderId == order.OrderId 
                    && x.ProductId == order.ProductId);
                    context.OrderDetails.Remove(x);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
