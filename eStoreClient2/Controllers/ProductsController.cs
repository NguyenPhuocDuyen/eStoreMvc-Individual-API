using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using eStoreClient2.Data;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using eStoreClient2.Models;

namespace eStoreClient2.Controllers
{
    public class ProductsController : Controller
    {
        HttpResponseMessage response;
        string responseString;

        // GET: Products
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = ApplicationRole.Admim)]
        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                response = GobalVariables.WebAPIClient.GetAsync($"Products/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    responseString = response.Content.ReadAsStringAsync().Result;
                    Product product = JsonConvert.DeserializeObject<Product>(responseString);
                    return View(product);
                }
            }
            catch { }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = ApplicationRole.Admim)]
        // GET: Products/Create
        public IActionResult Create()
        {
            try
            {
                response = GobalVariables.WebAPIClient.GetAsync("Categories").Result;
                if (response.IsSuccessStatusCode)
                {
                    responseString = response.Content.ReadAsStringAsync().Result;
                    List<Category> carts = JsonConvert.DeserializeObject<List<Category>>(responseString);
                    ViewData["CategoryId"] = new SelectList(carts, "CategoryId", "CategoryName");
                    return View();
                }
            }
            catch { }

            return RedirectToAction(nameof(Index));
        }

        // POST: Products/Create
        [Authorize(Roles = ApplicationRole.Admim)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitInStock")] Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }
            try
            {
                response = GobalVariables.WebAPIClient.PostAsJsonAsync("Products", product).Result;
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            }
            catch { }

            return RedirectToAction(nameof(Create));
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                response = GobalVariables.WebAPIClient.GetAsync("Categories").Result;
                if (response.IsSuccessStatusCode)
                {
                    responseString = response.Content.ReadAsStringAsync().Result;
                    List<Category> carts = JsonConvert.DeserializeObject<List<Category>>(responseString);
                    ViewData["CategoryId"] = new SelectList(carts, "CategoryId", "CategoryName", id);
                }

                response = GobalVariables.WebAPIClient.GetAsync($"Products/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    responseString = response.Content.ReadAsStringAsync().Result;
                    Product product = JsonConvert.DeserializeObject<Product>(responseString);
                    return View(product);
                }
            }
            catch { }

            return RedirectToAction(nameof(Index));
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitInStock")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            try
            {
                response = GobalVariables.WebAPIClient.PutAsJsonAsync($"Products/{id}", product).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch { }
            return RedirectToAction(nameof(Edit), new { id });
        }
    }
}
