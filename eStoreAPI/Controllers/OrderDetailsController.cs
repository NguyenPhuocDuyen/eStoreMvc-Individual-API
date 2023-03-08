using BusinessObject;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailRepository repository;

        public OrderDetailsController(IOrderDetailRepository repository)
        {
            this.repository = repository;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<OrderDetail>> GetOrderDetails() => repository.GetOrderDetails();

        // GET api/<OrderDetailsController>/5
        [HttpGet("{orderId}")]
        public ActionResult<List<OrderDetail>> GetListOrderDetailByOrderId(int orderId)
        {
            var list = repository.GetOrderDetails().Where(x=>x.OrderId == orderId);
            if (list == null)
            {
                return NotFound();
            }
            return list.ToList();
        }

        //[HttpPost]
        //public IActionResult PostOrderDetail(OrderDetail orderDetail)
        //{
        //    repository.SaveOrderDetail(orderDetail);
        //    return NoContent();
        //}

        //[HttpDelete("{orderId}/{productId}")]
        //public IActionResult DeleteOrderDetail(int orderId, int productId)
        //{
        //    var p = repository.GetOrderDetailById(orderId, productId);
        //    if (p == null)
        //    {
        //        return NotFound();
        //    }
        //    repository.DeleteOrderDetail(p);
        //    return NoContent();
        //}

        //[HttpPut("{orderId}/{productId}")]
        //public IActionResult UpdateOrderDetail(int orderId, int productId, OrderDetail orderDetail)
        //{
        //    var temp = repository.GetOrderDetailById(orderId, productId);

        //    if (temp == null)
        //        return NotFound();

        //    repository.UpdateOrderDetail(orderDetail);
        //    return NoContent();
        //}
    }
}
