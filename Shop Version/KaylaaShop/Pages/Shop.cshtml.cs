using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KaylaaShop.Pages.Kaylaa
{

    [KaylaCustomAuth("AdminStaff")]
    public class ShopModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}