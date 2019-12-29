using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Core;
using KaylaaShop.Data;
using KaylaaShop.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KaylaaShop
{
    [KaylaCustomAuth("AdminStaff")]
    public class PrintBarcodeModel : PageModel
    {
        public PaginatedList<Product> ProductList { get; set; }
        public Shop shop;
        public List<SelectListItem> allShops;
        public IQueryable<Product> allproducts;
        private readonly IKaylaaRepository<Shop> shopRepo;
        private readonly IShopRepository shopspecificRepo;
        private readonly IKaylaaRepository<Product> repo;
        private readonly IProductRepo prodSpecificRepo;
        private readonly IHttpContextAccessor httpContextAccessor;

        [BindProperty]
        public string searchKey { get; set; }

        [BindProperty]
        public int shopId { get; set; }

        [BindProperty]
        public bool showUnprintedOnly { get; set; }

        public int totalProduct;

        IEnumerable<Product> allproductsInAShop;

        public PrintBarcodeModel(IKaylaaRepository<Shop> shopRepo, IShopRepository shopspecificRepo,
         IKaylaaRepository<Product> repo,IProductRepo prodSpecificRepo, IHttpContextAccessor httpContextAccessor)
        {
            this.shopRepo = shopRepo;
            this.shopspecificRepo = shopspecificRepo;
            this.repo = repo;
            this.prodSpecificRepo = prodSpecificRepo;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {

           // var shopInSession = SessionHelper.GetObjectFromJSON<Shop>(httpContextAccessor.HttpContext.Session, "shop");
            shop = SessionHelper.GetObjectFromJSON<Shop>(HttpContext.Session, "shop");

            if (shop != null)
                allShops = new List<SelectListItem> { new SelectListItem { Text = shop.ShopName, Value = shop.Id.ToString() } };
            else
                allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();


            int pageSize = 15;

            if (shop == null)
            {
                allproductsInAShop = repo.GetAll();
            }
            else
            {
               allproductsInAShop = prodSpecificRepo.GetProductsByShopId(shop.Id);
                
            }

            showUnprintedOnly = Convert.ToBoolean(TempData["showUnprintedOnly"]);

            if (showUnprintedOnly && shop !=null)
            {
                allproductsInAShop = prodSpecificRepo.GetUnprintedProductInShop(shop.Id);
               
            }


            if( TempData["searchKey"] != null)
            {
                searchKey = TempData["searchKey"].ToString();
                allproductsInAShop = prodSpecificRepo.FindProductByKeyword(searchKey.ToLower());
                pageSize = allproductsInAShop.Count();
            }

            allproducts = allproductsInAShop.AsQueryable();


            totalProduct = allproducts.Count();


            if (allproducts.Count()<1)
            {
                ViewData["status"] = "No Product Found In this Shop To Print !";
                return Page();
            }
          
            ProductList = await PaginatedList<Product>.CreateAsync(allproducts.AsNoTracking(), pageIndex ?? 1, pageSize);
            return Page();

        }


        public IActionResult OnPostShopBarcodes()
        {
            Shop shop = shopspecificRepo.GetShopById(shopId);

            TempData["showUnprintedOnly"] = showUnprintedOnly;

            SessionHelper.SetObjectAsJSON(HttpContext.Session, "shop", shop);

            allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();
            return RedirectToPage("PrintBarcode");
        }


        public IActionResult OnPostFindProductByKeyword()
        {
            TempData["searchKey"] = searchKey;

            return RedirectToPage("PrintBarcode");

        }
    }
}
