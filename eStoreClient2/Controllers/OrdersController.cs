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
using Microsoft.AspNetCore.Authorization;
using eStoreClient2.Models;
using System.Net.Http.Json;

namespace eStoreClient2.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        HttpResponseMessage response;
        string responseString;

        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public IActionResult Index()
        {
            return View();
        }

        // GET: HistoryOrder
        public IActionResult HistoryOrder()
        {
            return View();
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                response = GobalVariables.WebAPIClient.GetAsync($"OrderDetails/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    responseString = response.Content.ReadAsStringAsync().Result;
                    List<OrderDetail> orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(responseString);
                    return View(orderDetails);
                }
            }
            catch { }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles =ApplicationRole.Admim)]
        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "UserName");
            return View();
        }

        [Authorize(Roles = ApplicationRole.Admim)]
        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,MemberId,OrderDate,RequiredDate,ShippedDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    response = GobalVariables.WebAPIClient.PostAsJsonAsync("Orders", order).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch { }
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", order.MemberId);
            return RedirectToAction(nameof(Create));
        }
    }
}
