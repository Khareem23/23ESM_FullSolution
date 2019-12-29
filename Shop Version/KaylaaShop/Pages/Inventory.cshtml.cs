using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using KaylaaShop.Infastructure;
using KaylaaShop.Core;
using KaylaaShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using KaylaaShop.Helpers;
using ZXing;
using ZXing.Common;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace KaylaaShop.Pages.Kaylaa
{

    [KaylaCustomAuth("AdminStaff")]
    // [Authorize]
    public class InventoryModel : PageModel
    {

       // private readonly IOptions<CloudinarySettings> cloudinaryConfig;
       // private readonly IProductRepo prodRepo;
       // private readonly IKaylaaRepository<ProductColor> colorsRepo;
       // private readonly IKaylaaRepository<ProductCategory> categoryRepo;
       // private readonly IKaylaaRepository<ProductBrand> brandRepo;
       // private readonly IKaylaaRepository<ProductCountry> countryRepo;
        private readonly IKaylaaRepository<Shop> shopRepo;
       //// private readonly IProductRepo prodspecificRepo;
       // private readonly IHttpContextAccessor httpContextAccessor;
       
       // private readonly IKaylaaRepository<Product> repo;
       // private Cloudinary cloudinary;

       // [BindProperty]
       // public IFormFile uploadFile { get; set; }

       // [BindProperty]
       // public IFormFile uploadedBarCodeFile { get; set; }

       // [BindProperty]
       // public Product product { get; set; }

       // public List<SelectListItem> allColors;
       // public List<SelectListItem> allCountries;
       // public List<SelectListItem> allBrands;
       // public List<SelectListItem> allCategories;

        public List<SelectListItem> allShops;

        public List<SelectListItem> DiscountList;



       // [BindProperty]
       // public int prodcolor { get; set; }
       // [BindProperty]
       // public int prodcategory { get; set; }
       // [BindProperty]
       // public int prodcountry { get; set; }
       // [BindProperty]
       // public int prodbrand { get; set; }

       // [BindProperty]
       // public int prodshop { get; set; }

       // [BindProperty]
       // public string ProductCode { get; set; }

       // public Shop shop;
       // public int totalProduct;
       // public IQueryable<Product> allproducts;
       // public PaginatedList<Product> ProductList { get; set; }




        public InventoryModel(IKaylaaRepository<Shop> shopRepo)
        {
            //this.repo = repo;
            //this.cloudinaryConfig = _cloudinaryConfig;
            //this.prodRepo = prodRepo;
            //this.colorsRepo = _colorsRepo;
            //this.categoryRepo = _categoryRepo;
            //this.brandRepo = _brandRepo;
            //this.countryRepo = _countryrepo;
            this.shopRepo = shopRepo;
           
            //this.httpContextAccessor = httpContextAccessor;
           
            //Account acc = new Account(

            //    cloudinaryConfig.Value.CloudName,
            //    cloudinaryConfig.Value.ApiKey,
            //    cloudinaryConfig.Value.ApiSecret
            //    );

            //cloudinary = new Cloudinary(acc);

        }

        public IActionResult OnGet()
        {
           
            allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();

          

            DiscountList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "0%", Value = "0.0"},
                new SelectListItem(){ Text = "1%", Value = "0.01"},
                new SelectListItem(){ Text = "2%", Value = "0.02"},
                new SelectListItem(){ Text = "3%", Value = "0.03"},
                new SelectListItem(){ Text = "4%", Value = "0.04"},
                new SelectListItem(){ Text = "5%", Value = "0.05"},
                new SelectListItem(){ Text = "6%", Value = "0.06"},
                new SelectListItem(){ Text = "7%", Value = "0.07"},
                new SelectListItem(){ Text = "8%", Value = "0.08"},
                new SelectListItem(){ Text = "9%", Value = "0.09"},
                new SelectListItem(){ Text = "10%", Value = "0.1"},
                new SelectListItem(){ Text = "15%", Value = "0.15"},
                new SelectListItem(){ Text = "20%", Value = "0.2"},
                new SelectListItem(){ Text = "25%", Value = "0.25"},
                new SelectListItem(){ Text = "30%", Value = "0.3"},
                new SelectListItem(){ Text = "35%", Value = "0.35"},
                new SelectListItem(){ Text = "40%", Value = "0.4"},
                new SelectListItem(){ Text = "45%", Value = "0.45"},
                new SelectListItem(){ Text = "50%", Value = "0.5"},
                new SelectListItem(){ Text = "55%", Value = "0.55"},
                new SelectListItem(){ Text = "60%", Value = "0.6"},
                new SelectListItem(){ Text = "65%", Value = "0.65"},
                new SelectListItem(){ Text = "70%", Value = "0.70"},
            };


            ////shop = SessionHelper.GetObjectFromJSON<Shop>(HttpContext.Session, "shop");
            //// if (shop == null) return RedirectToPage("/AccessDenied");

            //var allproductsInAShop = repo.GetAll();
            //allproducts = allproductsInAShop.AsQueryable();
            //totalProduct = allproducts.Count();

            //if (allproducts == null)
            //{
            //    ViewData["status"] = "No Product Found !";
            //}
            //int pageSize = 12;
            //ProductList = await PaginatedList<Product>.CreateAsync(allproducts.AsNoTracking(), pageIndex ?? 1, pageSize);
            return Page();


        }



     

    }
}