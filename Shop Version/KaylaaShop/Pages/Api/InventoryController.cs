using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
//using Kaylaa.DTOs;
using KaylaaShop.Infastructure;
using KaylaaShop.Core;
using KaylaaShop.Data;
using KaylaaShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace KaylaaShop.Pages.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {

        private readonly IKaylaaRepository<Product> repo;
       

        public InventoryController(IKaylaaRepository<Product> _repo)
        {
            this.repo = _repo;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
           
            var allProducts = repo.GetAll();
         
            return Ok(allProducts);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int  id)
        {
            var allProducts = repo.GetById(id);
            return Ok(allProducts);
        }

        [HttpPost]
        public IActionResult Add([FromBody]Product product)
        {

            if (product.Id == 0)
            {
                var newproduct = repo.Add(product);
                repo.Commit();
                var payload = new { name = newproduct.Name, status = "Added" };
                return Ok(payload);     // Status 200 : OK , 

            }
            else
            {
                var existingProduct = repo.GetById(product.Id);


                existingProduct.Id = product.Id;
                existingProduct.Name = product.Name;
                existingProduct.costPrice = product.costPrice;
                existingProduct.productBarcodeUrl = product.productBarcodeUrl;
                existingProduct.productImageUrl = product.productImageUrl;
                existingProduct.prodCode = product.prodCode;
                existingProduct.quantityAvailable = product.quantityAvailable;
                existingProduct.lastStockDate = product.lastStockDate;
                existingProduct.prodSize = product.prodSize;
                existingProduct.discountSellingPrice = product.discountSellingPrice;
                existingProduct.NormalSellingPrice = product.NormalSellingPrice;
                existingProduct.salesDiscount = product.salesDiscount;
                existingProduct.shopId = product.shopId;
               
                var updatedproduct = repo.Update(existingProduct);
               
                repo.Commit();
                var payload = new { name = updatedproduct.Name, status = "Updated" };
                return Ok(payload);
            }


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