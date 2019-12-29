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
    public class CountryController : ControllerBase
    {
        private readonly IKaylaaRepository<ProductCountry> repo;

        public CountryController(IKaylaaRepository<ProductCountry> _repo)
        {
            this.repo = _repo;
        }



        [HttpPost]
        public IActionResult Add([FromBody]ProductCountry ProductCountry)
        {

            if (ProductCountry.Id == 0)
            {
                if (repo.IsNameExist(ProductCountry.Name))
                {
                    var payload2 = new { name = ProductCountry.Name, status = "Already Exist" };
                    return Ok(payload2);
                }

                var newCountry = repo.Add(ProductCountry);
                repo.Commit();
                var payload = new { name = newCountry.Name, status = "Added" };
                return Ok(payload);     // Status 200 : OK , 

            }
            else
            {
                var updatedCountry = repo.Update(ProductCountry);
                repo.Commit();
                var payload = new { name = updatedCountry.Name, status = "Updated" };
                return Ok(payload);
            }


        }

        [HttpGet("{id}")]
        public IActionResult GetCountry(int id)
        {
            var allcountries = repo.GetById(id);
            return Ok(allcountries);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var AllCountrys = repo.GetAll();
            return Ok(AllCountrys);
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