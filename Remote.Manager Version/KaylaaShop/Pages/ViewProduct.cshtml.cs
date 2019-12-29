using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Core;
using KaylaaShop.Data;
using KaylaaShop.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KaylaaShop.Pages.Kaylaa
{

    [KaylaCustomAuth("")]
   // [Authorize]
    public class ViewProductModel : PageModel
    {

        private readonly IKaylaaRepository<Product> productRepo;

        private readonly IKaylaaRepository<ProductCategory> catRepo;
        private readonly IKaylaaRepository<ProductBrand> brandRepo;
        private readonly IKaylaaRepository<ProductColor> colorRepo;
        private readonly IKaylaaRepository<ProductCountry> countryRepo;

        public Product product { get; set; }

        public SalesProductViewModel productVM { get; set; }

        public ViewProductModel(IKaylaaRepository<Product> productRepo, IKaylaaRepository<ProductCategory> catRepo , 
            IKaylaaRepository<ProductBrand> brandRepo , IKaylaaRepository<ProductColor> colorRepo ,
            IKaylaaRepository<ProductCountry> countryRepo )
        {
            this.productRepo = productRepo;
            this.catRepo = catRepo;
            this.brandRepo = brandRepo;
            this.colorRepo = colorRepo;
            this.countryRepo = countryRepo;
        }
        public IActionResult OnGet(int productid)
        {
            


            product = productRepo.GetById(productid);

            var category = catRepo.GetById(product.Category);
            var color = colorRepo.GetById(product.Color);

           /// var brand = brandRepo.GetById(product.Brand);

           // var country = countryRepo.GetById(product.Country);



            productVM = new SalesProductViewModel()
            {
                Id=product.Id ,
                Name = product.Name ,
                NormalSellingPrice = product.NormalSellingPrice ,
                discountSellingPrice = product.discountSellingPrice,
                salesDiscount = (double) product.salesDiscount,
                prodSize = product.prodSize,
                prodCode = product.prodCode ,
                productImageUrl = product.productImageUrl , 
                quantityAvailable = product.quantityAvailable ,
                Status = product.status,
                laststockDate = product.lastStockDate ,
                CategoryName = product.Category , 
             //   BrandName = product.Brand ,
              //  CountryName = product.Country , 
              //  ColorName = product.Country ,
                ShopId = product.shopId
            };


            if (product != null)
            {
                return Page();
            }
            else return RedirectToPage("../NotFound");
        }


        public class SalesProductViewModel
        {

            public int Id { get; set; }
            public string Name { get; set; }

            public  double salesDiscount { get; set; }


            public Decimal discountSellingPrice { get; set; }

            public Decimal NormalSellingPrice { get; set; }

            public int ShopId { get; set; }
            public string prodSize { get; set; }
            public String productImageUrl { get; set; }
            public string prodCode { get; set; }
            public int quantityAvailable { get; set; }

            public bool Status { get; set; }

            public DateTime laststockDate { get; set; }


            public string CategoryName { get; set; }
            public string CountryName { get; set; }
            public string ColorName { get; set; }
            public string BrandName { get; set; }
        }
    }
}