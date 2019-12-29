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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using KaylaaShop.Infastructure;
using KaylaaShop;

namespace KaylaaShop.Pages.Kaylaa
{
    [KaylaCustomAuth("")]
    public class dashboardModel : PageModel
    {
       // private readonly IDeleteUnavailableProducts idelete;
        private readonly IKaylaaRepository<Product> repo;
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private Cloudinary cloudinary;

        public dashboardModel(IKaylaaRepository<Product> repo, IOptions<CloudinarySettings> _cloudinaryConfig)
        {
            
            this.repo = repo;
            cloudinaryConfig = _cloudinaryConfig;
            Account acc = new Account(

               cloudinaryConfig.Value.CloudName,
               cloudinaryConfig.Value.ApiKey,
               cloudinaryConfig.Value.ApiSecret
               );

            cloudinary = new Cloudinary(acc);

            //this.idelete = idelete;
        }
        public void OnGet()
        {
          //  var isAuth = User.Identity.IsAuthenticated;
          //  idelete.DeleteProducts();
        }

        public IActionResult OnPostDelete()
        {
            var AllProducts = repo.GetAll();

          string status = "Nothing Deleted";

          if(AllProducts != null)
            {
                     foreach (var product in AllProducts)
                                {
                                    if (product.quantityAvailable < 1)
                                    {
                                        var publicId = this.GetPublicId(product.productImageUrl);
                         
                                        var BarcodeId = this.GetPublicId(product.productBarcodeUrl);

                                        var delParams = new DelResParams()
                                        {
                                            PublicIds = new List<string>() { publicId,BarcodeId },
                                            Invalidate = true
                                        };

                                        var deleteresult = cloudinary.DeleteResources(delParams);

                                      //  repo.Delete(product.Id);
                                        //status += product.prodCode + " - Deleted";

                                    }
                                }
                                status = "Soldout Items Assets Deleted Successfully";
                                repo.Commit();
            }
           
            ViewData["del_status"] = status;
            return Page();
        }


        public string GetPublicId(string url)
        {
            string[] allsubstrings = url.Split('/');
            int n = allsubstrings.Length;

            string[] PublicIdarray = allsubstrings[n - 1].Split('.');

            return PublicIdarray[0];
        }
    }
}