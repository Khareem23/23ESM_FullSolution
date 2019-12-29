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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KaylaaShop.Pages.Kaylaa
{

    [KaylaCustomAuth("AdminStaff")]
    public class OrdersModel : PageModel
    {
        IKaylaaRepository<Shop> shopRepo;
        public List<SelectListItem> allShops;

        public OrdersModel(IKaylaaRepository<Shop> shopRepo)
        {
            this.shopRepo = shopRepo;
        }

        public void OnGet()
        {
            allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();
        }
    }
}