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
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetProducts() => ProductDAO.GetProducts();
        public Product GetProductById(int id) => ProductDAO.FindProductById(id);
        public void SaveProduct(Product product) => ProductDAO.SaveProduct(product);
        public void UpdateProduct(Product product) => ProductDAO.UpdateProduct(product);
        public void DeleteProduct(Product product) => ProductDAO.DeleteProduct(product);
    }
}
