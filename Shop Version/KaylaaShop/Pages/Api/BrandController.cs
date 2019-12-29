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
    public class BrandController : ControllerBase
    {
        private readonly IKaylaaRepository<ProductBrand> repo;

        public BrandController(IKaylaaRepository<ProductBrand> _repo)
        {
            this.repo = _repo;
        }
        [HttpPost]
        public IActionResult Add([FromBody]ProductBrand productBrand)
        {

            if(productBrand.Id == 0 )
            {

                if(repo.IsNameExist(productBrand.Name))
                {
                   
                    var payloaddata = new { name= productBrand.Name, status = "Already Exist" };
                    return Ok(payloaddata);
                    //return StatusCode(StatusCodes.Status409Conflict,payloaddata);
                }

                var newBrand = repo.Add(productBrand);
                repo.Commit();
                var payload = new {name = newBrand.Name , status = "Added" };
                return Ok(payload);     // Status 200 : OK , 

            }
            else
            {
                var updatedBrand = repo.Update(productBrand);
                repo.Commit();
                var payload = new { name = updatedBrand.Name, status = "Updated" };
                return Ok(payload);
            }
         
           
        }

        [HttpGet("{id}")]
        public IActionResult GetBrand(int id)
        {
            var allbrands = repo.GetById(id);
            return Ok(allbrands);
        }



        [HttpGet]
        public IActionResult GetAll()
        {
           var AllBrands =  repo.GetAll();
            return Ok(AllBrands);
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