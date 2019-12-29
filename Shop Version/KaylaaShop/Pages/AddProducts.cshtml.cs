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
using Microsoft.Extensions.Configuration;

namespace KaylaaShop.Pages
{

    [KaylaCustomAuth("AdminStaff")]
    public class AddProductsModel : PageModel
    {
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private readonly IProductRepo prodRepo;
        private readonly IKaylaaRepository<ProductColor> colorsRepo;
        private readonly IKaylaaRepository<ProductCategory> categoryRepo;
        private readonly IKaylaaRepository<ProductBrand> brandRepo;
        private readonly IKaylaaRepository<ProductCountry> countryRepo;
        private readonly IKaylaaRepository<Shop> shopRepo;
        // private readonly IProductRepo prodspecificRepo;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly IKaylaaRepository<Product> repo;
        private Cloudinary cloudinary;

        [BindProperty]
        public IFormFile uploadFile { get; set; }

        [BindProperty]
        public IFormFile uploadedBarCodeFile { get; set; }

        [BindProperty]
        public Product product { get; set; }

        public List<SelectListItem> allColors;
        public List<SelectListItem> allCountries;
        public List<SelectListItem> allBrands;
        public List<SelectListItem> allCategories;

        public List<SelectListItem> allShops;

        public List<SelectListItem> DiscountList;


       

        [BindProperty]
        public string prodcolor { get; set; }
        [BindProperty]
        public string prodcategory { get; set; }
        [BindProperty]
        public string prodcountry { get; set; }
        [BindProperty]
        public string prodbrand { get; set; }

        [BindProperty]
        public int prodshop { get; set; }

        [BindProperty]
        public string ProductCode { get; set; }

        public Shop shop;
        public int totalProduct;
        public IQueryable<Product> allproducts;
        public PaginatedList<Product> ProductList { get; set; }




        public AddProductsModel(IKaylaaRepository<Product> repo, IOptions<CloudinarySettings> _cloudinaryConfig, IProductRepo prodRepo,
        IKaylaaRepository<ProductColor> _colorsRepo, IKaylaaRepository<ProductCategory> _categoryRepo, IKaylaaRepository<ProductBrand>
        _brandRepo, IKaylaaRepository<ProductCountry> _countryrepo, IKaylaaRepository<Shop> shopRepo,
        IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.repo = repo;
            this.cloudinaryConfig = _cloudinaryConfig;
            this.prodRepo = prodRepo;
            this.colorsRepo = _colorsRepo;
            this.categoryRepo = _categoryRepo;
            this.brandRepo = _brandRepo;
            this.countryRepo = _countryrepo;
            this.shopRepo = shopRepo;

            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            Account acc = new Account(

                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
                );

            cloudinary = new Cloudinary(acc);

        }

        public IActionResult OnGet()
        {
           // allColors = colorsRepo.GetAll().Select(d => new SelectListItem() { Text = d.Name, Value = d.Name }).ToList();
          //  allCountries = countryRepo.GetAll().Select(d => new SelectListItem() { Text = d.Name, Value = d.Name }).ToList();
            allCategories = categoryRepo.GetAll().Select(d => new SelectListItem() { Text = d.Name, Value = d.Name }).ToList();
           // allBrands = brandRepo.GetAll().Select(d => new SelectListItem() { Text = d.Name, Value = d.Name }).ToList();
            allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();

           // TempData["allColors"] = ComplexTypeSerializerHelper.SerializeObject<SelectListItem>(allColors);
           // TempData["allCountries"] = ComplexTypeSerializerHelper.SerializeObject<SelectListItem>(allCountries);
            TempData["allCategories"] = ComplexTypeSerializerHelper.SerializeObject<SelectListItem>(allCategories);
           // TempData["allBrands"] = ComplexTypeSerializerHelper.SerializeObject<SelectListItem>(allBrands);
            TempData["allShops"] = ComplexTypeSerializerHelper.SerializeObject<SelectListItem>(allShops);

            ProductCode = this.GeneratetProductCode();

            if (string.IsNullOrEmpty(ProductCode))
                ViewData["Msg"] = "Failed to Generate Product Code, Try Again";
            else
                TempData["ProductCode"] = this.ProductCode;

            DiscountList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "0%", Value = "0.00"},
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


            return Page();


        }



        public string GeneratetProductCode()
        {
            string Prefix = "KB";
            int code = 0;
            string mycode = "";


            bool isCodeNotAvaialble = false;

            while (isCodeNotAvaialble == false)
            {

                Random rnd = new Random();
                code = rnd.Next(0, 999999);

                mycode = Prefix + code.ToString();

                isCodeNotAvaialble = prodRepo.CheckProductCode(mycode);
                if (isCodeNotAvaialble)
                {
                    isCodeNotAvaialble = true;
                    break;

                }
            }

            return mycode;
        }

        private string GeneratePng(string content, BarcodeFormat barcodeformat, int width, int height, int margin, string alt)
        {
            var qrWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = barcodeformat,
                Options = new EncodingOptions { Height = height, Width = width, Margin = margin }
            };


            var pixelData = qrWriter.Write(content);

            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB
            // the System.Drawing.Bitmap class is provided by the CoreCompat.System.Drawing package
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                // lock the data area for fast access
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                   System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                       pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return Convert.ToBase64String(ms.ToArray());
            }
        }






        public IActionResult OnPostEncode()
        {

            return Page();
        }
        public IActionResult OnPostAsync(Product product)
        {

            if (!ModelState.IsValid)
            {
                ViewData["msg"] = "Empty Inputs: Ensure you fill in all details of the Product";
                return Page();
            }
            product.Color = this.prodcolor;
            product.Category = this.prodcategory;
            product.shopId = this.prodshop;

            decimal amount = (decimal)product.salesDiscount * product.NormalSellingPrice;
            product.discountSellingPrice = product.NormalSellingPrice - amount;
            product.prodCode = this.ProductCode.Trim();

            int barheight = int.Parse(configuration.GetSection("ProjectSettings")["BarcodeHeight"]);

            int barwidth = int.Parse (configuration.GetSection("ProjectSettings")["BarcodeWidth"]);

            // var barcodeInBase64 = this.GeneratePng(product.prodCode, BarcodeFormat.CODE_128, 230, 50, 2, "img");
            var barcodeInBase64 = this.GeneratePng(product.prodCode, BarcodeFormat.CODE_128, barwidth, barheight, 2, "img");



            // Uploading Product Image to Cloud API Endpoints 
            var uploadResult = new ImageUploadResult();
            if (uploadFile == null && string.IsNullOrWhiteSpace(product.productImageUrl))
            {
                ViewData["msg"] = "No Product Picture Uploaded";
                return Page();
            }

            if( string.IsNullOrWhiteSpace(product.productImageUrl))
            {
                if (uploadFile.Length > 0)
                {
                    string UploadfileName = product.Name + product.prodCode;

                    using (var stream = uploadFile.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(UploadfileName, stream),
                            Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")

                        };

                        uploadResult = cloudinary.Upload(uploadParams);
                    }
                }

                product.productImageUrl = uploadResult.Uri.ToString();
            }
           

          




            // Uploading BarCode Image 

            var uploadBarcodeResult = new ImageUploadResult();

            if (barcodeInBase64 != null)
            {
                string UploadBarcodefileName = "Barcode-" + product.Name + product.prodCode;

                var uploadBarcodeParams = new ImageUploadParams()
                {
                    File = new FileDescription("data:image/png;base64," + barcodeInBase64)

                };

                uploadBarcodeResult = cloudinary.Upload(uploadBarcodeParams);
            }


            product.productBarcodeUrl = uploadBarcodeResult.Uri.ToString();

            ViewData["BarcodeImage"] = uploadBarcodeResult.Uri.ToString();
            ViewData["SellingPrice"] = string.Format("{0:N0}", product.NormalSellingPrice.ToString());


            repo.Add(product);
            repo.Commit();


            ViewData["msg"] = "Item with Product Code " + product.prodCode + "  Created Successfully !";

            return Page();


        }

    }
}