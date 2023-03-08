using BusinessObject;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository repository;

        public CartsController(ICartRepository repository)
        {
            this.repository = repository;
        }

        //// PUT: api/Carts/5
        //[HttpPut("PutCart")]
        //public IActionResult PutCart(Cart cart)
        //{
        //    var c = repository.GetCartById(cart.CartId);
        //    if (c == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        c.Quantity = cart.Quantity;

        //        if (c.Quantity > c.Product.UnitInStock)
        //        {
        //            c.Quantity = (int)c.Product.UnitInStock;
        //        }

        //        repository.UpdateCart(c);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (CartExists(cart.CartId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Ok(c);
        //}

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCart(int id)
        {
            var cart = repository.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }

            repository.DeleteCart(cart);
            return NoContent();
        }

        private bool CartExists(int id)
        {
            var cart = repository.GetCartById(id);
            if (cart == null)
            {
                return false;
            }
            return true;
        }

        // Khang - Post: AddProductToCart
        [HttpPost("AddProductToCart")]
        public async Task<IActionResult> AddCart([FromBody] Cart cart)
        {
            var oldCart = repository.FindCartByUserIdAndProductId(cart.MemberId, cart.ProductId);

            // Check cart is exist in database to create cart or upadate quantity
            if (oldCart == null)
            {
                cart.Quantity ??= 1;
                repository.SaveCart(cart);
            }
            else
            {
                if (cart.Quantity != null)
                {
                    oldCart.Quantity += cart.Quantity;
                }
                else
                {
                    oldCart.Quantity += 1;
                }
                repository.UpdateCart(oldCart);
            }
            return NoContent();
        }

        // GET: api/Carts/5
        [HttpGet("GetCartsUser/{userId}")]
        public async Task<ActionResult<List<Cart>>> GetCartsUser(string userId)
        {
            var carts = repository.GetCartsByUserId(userId);

            if (carts == null)
            {
                return NotFound();
            }

            return carts;
        }
    }
}
