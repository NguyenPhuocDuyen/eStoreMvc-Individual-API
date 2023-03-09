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
    }
}
