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
    public class ShopController : ControllerBase
    {
        private readonly IKaylaaRepository<Shop> repo;
        private readonly IProductRepo prodRepo;

        public ShopController(IKaylaaRepository<Shop> _repo, IProductRepo prodRepo)
        {
            this.repo = _repo;
            this.prodRepo = prodRepo;
        }



        [HttpPost]
        public IActionResult Add([FromBody]Shop Shop)
        {

            if (Shop.Id == 0)
            {
                var newShop = repo.Add(Shop);
                repo.Commit();
                var payload = new { name = newShop.ShopName, status = "Added" };
                return Ok(payload);     // Status 200 : OK , 

            }
            else
            {
                var updatedShop = repo.Update(Shop);
                repo.Commit();
                var payload = new { name = updatedShop.ShopName, status = "Updated" };
                return Ok(payload);
            }


        }

        [HttpGet]
        [Route("getshopproducts/{shopId}")]
        public IActionResult GetShopProduct(int shopId)
        {
            var allproductsInaShop = prodRepo.GetProductsByShopId(shopId);
            return Ok(allproductsInaShop);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var AllShops = repo.GetAll();
            return Ok(AllShops);
        }


        [HttpGet("{id}")]
        public IActionResult GetShop(int id)
        {
            var allShop = repo.GetById(id);
            return Ok(allShop);
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