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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo productRepo;

        public ProductController(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }
        [HttpGet]
        [Route("setprinted/{productId}")]
        public IActionResult SetPrinted(int productId)
        {
            string result = "";
            bool flag = productRepo.setProductToPrinted(productId);
            result = flag == true ? " Product Set to Printed" : "Failed to Set to Printed";
            return Ok(result);
        }
    }
}