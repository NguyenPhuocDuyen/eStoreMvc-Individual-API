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
    public class CartRepository : ICartRepository
    {
        public List<Cart> GetCarts() => CartDAO.GetCarts();
        public Cart GetCartById(int id) => CartDAO.FindCartById(id);
        public List<Cart> GetCartsByUserId(string userId)
            => CartDAO.GetCartsByUserId(userId);
        public Cart FindCartByUserIdAndProductId(string userId, int productId) 
            => CartDAO.FindCartByUserIdAndProductId(userId, productId);
        public void SaveCart(Cart cart) => CartDAO.SaveCart(cart);
        public void UpdateCart(Cart cart) => CartDAO.UpdateCart(cart);
        public void DeleteCart(Cart cart) => CartDAO.DeleteCart(cart);
    }
}
