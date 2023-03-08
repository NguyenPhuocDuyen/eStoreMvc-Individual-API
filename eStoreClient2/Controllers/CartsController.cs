using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using eStoreClient2.Data;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace eStoreClient2.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        HttpResponseMessage response;
        string responseString;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Member> userManager;

        public CartsController(ApplicationDbContext context, UserManager<Member> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Carts
        public IActionResult Index()
        {
            try
            {
                // Lấy tên người dùng hiện tại
                var username = User.Identity.Name;

                // Tìm người dùng có tên đó trong cơ sở dữ liệu
                var user = _context.Users.FirstOrDefault(u => u.UserName == username);

                // Lấy User ID từ đối tượng ApplicationUser
                var userId = user.Id;

                response = GobalVariables.WebAPIClient.GetAsync($"Carts/GetCartsUser/{userId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    responseString = response.Content.ReadAsStringAsync().Result;
                    List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(responseString);
                    return View(carts);
                }
            }
            catch { }

            return RedirectToAction(nameof(Index), "Product");
        }

        public IActionResult OrderProducts()
        {
            try
            {
                // Lấy tên người dùng hiện tại
                var username = User.Identity.Name;

                // Tìm người dùng có tên đó trong cơ sở dữ liệu
                var user = _context.Users.FirstOrDefault(u => u.UserName == username);

                // Lấy User ID từ đối tượng ApplicationUser
                var userId = user.Id;


                response = GobalVariables.WebAPIClient.GetAsync($"Orders/OrderProducts/{userId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "Products");
                }
            }
            catch { }

            return RedirectToAction(nameof(Index));
        }
        //// GET: Carts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cart = await _context.Carts
        //        .Include(c => c.Member)
        //        .Include(c => c.Product)
        //        .FirstOrDefaultAsync(m => m.CartId == id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cart);
        //}

        //// GET: Carts/Create
        //public IActionResult Create()
        //{
        //    ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
        //    ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
        //    return View();
        //}

        //// POST: Carts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CartId,MemberId,ProductId,Quantity")] Cart cart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(cart);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", cart.MemberId);
        //    ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", cart.ProductId);
        //    return View(cart);
        //}

        //// GET: Carts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cart = await _context.Carts.FindAsync(id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", cart.MemberId);
        //    ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", cart.ProductId);
        //    return View(cart);
        //}

        //// POST: Carts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CartId,MemberId,ProductId,Quantity")] Cart cart)
        //{
        //    if (id != cart.CartId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(cart);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CartExists(cart.CartId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", cart.MemberId);
        //    ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", cart.ProductId);
        //    return View(cart);
        //}

        //// GET: Carts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cart = await _context.Carts
        //        .Include(c => c.Member)
        //        .Include(c => c.Product)
        //        .FirstOrDefaultAsync(m => m.CartId == id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cart);
        //}

        //// POST: Carts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var cart = await _context.Carts.FindAsync(id);
        //    _context.Carts.Remove(cart);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CartExists(int id)
        //{
        //    return _context.Carts.Any(e => e.CartId == id);
        //}
    }
}
