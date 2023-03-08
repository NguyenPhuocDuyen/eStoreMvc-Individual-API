using BusinessObject;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository repository;
        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderDetailRepository orderDetailRepository;

        public OrdersController(IOrderRepository repository, 
            ICartRepository cartRepository, 
            IProductRepository productRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            this.repository = repository;
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders() => repository.GetOrders();

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var p = repository.GetOrderById(id);
            if (p == null)
            {
                return NotFound();
            }
            return p;
        }

        [HttpGet("OrderProducts/{userId}")]
        public IActionResult OrderProducts(string userId)
        {
            var carts = cartRepository.GetCartsByUserId(userId);

            if (carts.Any())
            {
                Order order = new()
                {
                    MemberId = userId
                };
                repository.SaveOrder(order);

                var orderList = repository.GetOrders();

                var orderNew = orderList.OrderByDescending(x => x.OrderId).FirstOrDefault();

                foreach (var item in carts)
                {
                    var pro = productRepository.GetProductById(item.ProductId);

                    pro.UnitInStock -= (int) item.Quantity;
                    productRepository.UpdateProduct(pro);

                    orderDetailRepository.SaveOrderDetail(new OrderDetail
                    {
                        ProductId = item.ProductId,
                        OrderId = orderNew.OrderId,
                        Quantity = (int)item.Quantity,
                        UnitPrice = item.Product.UnitPrice
                    });
                    cartRepository.DeleteCart(item);
                }
                
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult PostOrder(Order order)
        {
            repository.SaveOrder(order);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            var temp = repository.GetOrderById(id);

            if (temp == null)
                return NotFound();

            repository.UpdateOrder(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var p = repository.GetOrderById(id);
            if (p == null)
            {
                return NotFound();
            }
            repository.DeleteOrder(p);
            return NoContent();
        }
    }
}
