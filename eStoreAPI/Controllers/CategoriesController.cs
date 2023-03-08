using BusinessObject;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository repository;

        public CategoriesController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategorys() => repository.GetCategories();

        // GET api/<CategorysController>/5
        //[HttpGet("{id}")]
        //public ActionResult<Category> Get(int id)
        //{
        //    var p = repository.GetCategoryById(id);
        //    if (p == null)
        //    {
        //        return NotFound();
        //    }
        //    return p;
        //}

        [HttpPost]
        public IActionResult PostCategory(Category category)
        {
            repository.SaveCategory(category);
            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteCategory(int id)
        //{
        //    var p = repository.GetCategoryById(id);
        //    if (p == null)
        //    {
        //        return NotFound();
        //    }
        //    repository.DeleteCategory(p);
        //    return NoContent();
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateCategory(int id, Category category)
        //{
        //    var temp = repository.GetCategoryById(id);

        //    if (temp == null)
        //        return NotFound();
            
        //    repository.UpdateCategory(category);
        //    return NoContent();
        //}
    }
}
