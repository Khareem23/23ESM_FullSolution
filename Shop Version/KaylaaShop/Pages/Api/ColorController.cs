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
    public class ColorController : ControllerBase
    {
        private readonly IKaylaaRepository<ProductColor> repo;

        public ColorController(IKaylaaRepository<ProductColor> _repo)
        {
            this.repo = _repo;
        }



        [HttpPost]
        public IActionResult Add([FromBody]ProductColor ProductColor)
        {

            if (ProductColor.Id == 0)
            {
                if (repo.IsNameExist(ProductColor.Name))
                {
                    var payload2 = new { name = ProductColor.Name, status = "Already Exist" };
                    return Ok(payload2);
                }

                var newColor = repo.Add(ProductColor);
                repo.Commit();
                var payload = new { name = newColor.Name, status = "Added" };
                return Ok(payload);     // Status 200 : OK , 

            }
            else
            {
                var updatedColor = repo.Update(ProductColor);
                repo.Commit();
                var payload = new { name = updatedColor.Name, status = "Updated" };
                return Ok(payload);
            }


        }

        [HttpGet("{id}")]
        public IActionResult GetColor(int id)
        {
            var allcolors = repo.GetById(id);
            return Ok(allcolors);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var AllColors = repo.GetAll();
            return Ok(AllColors);
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