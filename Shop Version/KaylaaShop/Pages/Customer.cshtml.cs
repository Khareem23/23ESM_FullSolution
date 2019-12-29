using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Core;
using KaylaaShop.Data;
using KaylaaShop.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KaylaaShop.Pages.Kaylaa
{
    [KaylaCustomAuth("")]
    //[Authorize]
    public class CustomerModel : PageModel
    {
        IKaylaaRepository<Shop> shopRepo;
        private readonly IHttpContextAccessor httpContextAccessor;
        public List<SelectListItem> allShops;

        //public int shopId;

        public CustomerModel(IKaylaaRepository<Shop> shopRepo, IHttpContextAccessor httpContextAccessor)
        {
            this.shopRepo = shopRepo;
            this.httpContextAccessor = httpContextAccessor;
        }


        public void OnGet()
        {
            var shopInSession = SessionHelper.GetObjectFromJSON<Shop>(httpContextAccessor.HttpContext.Session, "shop");

            if (shopInSession != null)
                allShops = new List<SelectListItem> { new SelectListItem { Text = shopInSession.ShopName, Value = shopInSession.Id.ToString() } };
            else
                allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();
        }
    }
}