using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CartDAO
    {
        public static List<Cart> GetCarts()
        {
            var list = new List<Cart>();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Carts.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static Cart FindCartById(int cartId)
        {
            Cart cart = new();
            try
            {
                using (var context = new MyDbContext())
                {
                    cart = context.Carts.SingleOrDefault(x => x.CartId == cartId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cart;
        }

        public static List<Cart> GetCartsByUserId(string userId)
        {
            List<Cart> list = new();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Carts.Include(x=>x.Product).Where(x => x.MemberId == userId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static Cart FindCartByUserIdAndProductId(string userId, int productId)
        {
            Cart cart = new();
            try
            {
                using (var context = new MyDbContext())
                {
                    cart = context.Carts.Include(x => x.Product) .SingleOrDefault(x => x.MemberId == userId
                    && x.ProductId == productId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cart;
        }

        public static void SaveCart(Cart cart)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Carts.Add(cart);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateCart(Cart cart)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry(cart).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCart(Cart cart)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var p = context.Carts.SingleOrDefault(
                        c => c.CartId == cart.CartId);
                    context.Carts.Remove(p);
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
