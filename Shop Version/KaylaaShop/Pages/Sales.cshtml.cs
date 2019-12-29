using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KaylaaShop.Data;
using KaylaaShop.Core;
using KaylaaShop.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KaylaaShop.Pages.Kaylaa
{

    [KaylaCustomAuth("")]
   // [Authorize]
    public class SalesModel : PageModel
    {
        private readonly IKaylaaRepository<Product> productRepo;
        private readonly IShopRepository shopspecificRepo;
        private readonly IKaylaaRepository<Shop> shopRepo;
        private readonly IProductRepo prodspecificRepo;
        private readonly IHttpContextAccessor httpContextAccessor;
        public IQueryable<Product> allproducts;

        public IEnumerable<Product> allproductsToDisplay;

        [BindProperty(SupportsGet = true)]
        public ShoppingCartItem item { get; set; }

        public List<ShoppingCartItem> Cart;

        public Shop shop;

        public int totalProduct;

        public PaginatedList<Product> ProductList { get; set; }

        public IQueryable<Product> SingleProduct { get; set; }
        public List<SelectListItem> allShops;
        [BindProperty]
        public int shopId { get; set; }

        public SalesModel(IKaylaaRepository<Product> productRepo, IShopRepository shopspecificRepo, IKaylaaRepository<Shop> shopRepo, IProductRepo prodspecificRepo, IHttpContextAccessor httpContextAccessor)
        {
            this.productRepo = productRepo;
            this.shopspecificRepo = shopspecificRepo;
            this.shopRepo = shopRepo;
            this.prodspecificRepo = prodspecificRepo;
            this.httpContextAccessor = httpContextAccessor;
            allproducts = null;
        }


        public async Task<IActionResult> OnGet(int? pageIndex)
        {
            shop = SessionHelper.GetObjectFromJSON<Shop>(HttpContext.Session, "shop");

            if (shop != null)
                allShops = new List<SelectListItem> { new SelectListItem { Text = shop.ShopName, Value = shop.Id.ToString() } };
            else
                allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();


           // allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();

            Cart = SessionHelper.GetObjectFromJSON<List<ShoppingCartItem>>(HttpContext.Session, "Cart");

           // shop = SessionHelper.GetObjectFromJSON<Shop>(HttpContext.Session, "shop");

            if (shop == null)
            {
                allproductsToDisplay = productRepo.GetAll();
            }
            else
            {
                allproductsToDisplay  = prodspecificRepo.GetProductsByShopId(shop.Id);
            }

            allproducts = allproductsToDisplay.AsQueryable();

            totalProduct = allproducts.Count();

            if (allproducts.Count()<1)
            {
                ViewData["status"] = "No Product Found !";
            }
            int pageSize = 12;
            ProductList = await PaginatedList<Product>.CreateAsync(allproducts.AsNoTracking(), pageIndex ?? 1, pageSize);
            return Page();
        }

        public IActionResult OnPostSetCurrentShop()
        {
            if(shopId != 0)
            { 
                Shop shop = shopspecificRepo.GetShopById(shopId);
                SessionHelper.SetObjectAsJSON(HttpContext.Session, "shop", shop);
            }

            allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();

            return RedirectToPage("Sales");
        }

        public async Task<IActionResult> OnPostFindProductAsync(int qty, string prodCode)
        {
         
           Product  SingleProduct =  prodspecificRepo.GetProductByCode(prodCode);


            if(SingleProduct == null)
            {
                ViewData["status"] = "No Product with Such Code";
                return Page();
            }
               

            return RedirectToPage("ShoppingCart", "AddToCart", new { quantity = qty, ProductId = SingleProduct.Id });
          
        }

        public IActionResult OnGetFindProduct()
        { 
            return Page();
        }


        public async Task<IActionResult> OnPost(int qty,int prodId,int? pageIndex)
        {
            int pageSize = 12;
           
            int noofProductAvailable = prodspecificRepo.GetProductQuantity(prodId);


            if (shop == null)
            {
                allproductsToDisplay = productRepo.GetAll();
            }
            else
            {
                allproductsToDisplay = prodspecificRepo.GetProductsByShopId(shop.Id);
            }


            if (qty > noofProductAvailable)
            {
                ViewData["status"] = "The Quantity Of Product Requested Unavailable";

                allproducts = allproductsToDisplay.AsQueryable();

                ProductList = await PaginatedList<Product>.CreateAsync(allproducts.AsNoTracking(), pageIndex ?? 1, pageSize);

                return Page();
            }
            else
            {
                return RedirectToPage("ShoppingCart", "AddToCart", new { quantity = qty, ProductId = prodId });
            }
         
        }

    }

    
}