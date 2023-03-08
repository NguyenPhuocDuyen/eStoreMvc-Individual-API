using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        public static List<Order> GetOrders()
        {
            var list = new List<Order>();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Orders.Include(x => x.OrderDetails).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static List<Order> GetOrdersByUserId(string userId)
        {
            var list = new List<Order>();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Orders.Include(x => x.OrderDetails).Where(x=>x.MemberId == userId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        
        public static Order FindOrderById(int orderId)
        {
            Order order = new();
            try
            {
                using (var context = new MyDbContext())
                {
                    order = context.Orders.Include(x => x.OrderDetails).SingleOrDefault(x => x.OrderId == orderId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public static void SaveOrder(Order order)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateOrder(Order order)
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

        public static void DeleteOrder(Order order)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var x = context.Orders.SingleOrDefault(
                        x => x.OrderId == order.OrderId);
                    context.Orders.Remove(x);
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
