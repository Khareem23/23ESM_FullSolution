using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Core;
using KaylaaShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaylaaShop.Pages.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IKaylaaRepository<ProductCategory> repo;

        public CategoryController(IKaylaaRepository<ProductCategory> _repo)
        {
            this.repo = _repo;
        }



        [HttpPost]
        public IActionResult Add([FromBody]ProductCategory ProductCategory)
        {

            if (ProductCategory.Id == 0)
            {
                if (repo.IsNameExist(ProductCategory.Name))
                {
                    var payload2 = new { name = ProductCategory.Name, status = "Already Exist" };
                    return Ok(payload2);
                }
                var newCategory = repo.Add(ProductCategory);
                repo.Commit();
                var payload = new { name = newCategory.Name, status = "Added" };
                return Ok(payload);     // Status 200 : OK , 

            }
            else
            {
                var updatedCategory = repo.Update(ProductCategory);
                repo.Commit();
                var payload = new { name = updatedCategory.Name, status = "Updated" };
                return Ok(payload);
            }


        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var allcategories = repo.GetById(id);
            return Ok(allcategories);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var AllCategories = repo.GetAll();
            return Ok(AllCategories);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repo.Delete(id);
            repo.Commit();
            return NoContent();
        }
    }
}