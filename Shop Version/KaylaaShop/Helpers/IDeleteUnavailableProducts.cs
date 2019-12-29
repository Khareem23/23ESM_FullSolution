using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using KaylaaShop.Infastructure;
using KaylaaShop.Core;
using KaylaaShop.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaylaaShop.Helpers
{
    public interface IDeleteUnavailableProducts
    {
        string DeleteProducts();
    }

    public class DeleteUnavailableProducts : IDeleteUnavailableProducts
    {
        private readonly IKaylaaRepository<Product> repo;
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private Cloudinary cloudinary;

        public DeleteUnavailableProducts(IKaylaaRepository<Product> _repo, IOptions<CloudinarySettings> _cloudinaryConfig)
        {
            repo = _repo;
            cloudinaryConfig = _cloudinaryConfig;

            Account acc = new Account(

              cloudinaryConfig.Value.CloudName,
              cloudinaryConfig.Value.ApiKey,
              cloudinaryConfig.Value.ApiSecret
              );

            cloudinary = new Cloudinary(acc);
        }
        public string DeleteProducts()
        {
            var AllProducts = repo.GetAll().ToList();

            string status = "No Products To Delete";

            foreach (var product in AllProducts)
            {
                if (product.quantityAvailable < 1)
                {
                    status = "Product To Delete Found";
                    var publicId = this.GetPublicId(product.productImageUrl);

                    var delParams = new DelResParams()
                    {
                        PublicIds = new List<string>() { publicId },
                        Invalidate = true
                    };

                    var deleteresult = cloudinary.DeleteResources(delParams);

                    repo.Delete(product.Id);
                    status += product.prodCode + " - Deleted";

                }
                repo.Commit();
            }
          
            return status;
        }

        private string GetPublicId(string url)
        {
            string[] allsubstrings = url.Split('/');
            int n = allsubstrings.Length;

            string[] PublicIdarray = allsubstrings[n - 1].Split('.');

            return PublicIdarray[0];
        }
    }
}
