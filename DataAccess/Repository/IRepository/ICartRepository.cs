using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ICartRepository
    {
        List<Cart> GetCarts();
        Cart GetCartById(int id);
        List<Cart> GetCartsByUserId(string userId);
        Cart FindCartByUserIdAndProductId(string userId, int productId);
        void SaveCart(Cart cart);
        void UpdateCart(Cart cart);
        void DeleteCart(Cart cart);
    }
}
