using KaylaaShop.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KaylaaShop.Data
{
    public interface IProductRepo
    {
        bool CheckProductCode(string code);
       Product GetProductByCode(string code);

        int GetProductQuantity(int id);

        IEnumerable<Product> GetProductsByShopId(int id);

        int GetNoItemsUnavailable();

        IEnumerable<Product> GetUnprintedProductInShop(int shopid);

        IEnumerable<Product> FindProductByKeyword(string keyword);

        bool setProductToPrinted(int productId);

        //bool DeleteUnavailableProducts();

    }

    public class ProductRepo : IProductRepo
    {
      
        private readonly KaylaaDataContext context;

        public IKaylaaRepository<Product> genericRepo { get; }

        public ProductRepo(KaylaaDataContext _context, IKaylaaRepository<Product> genericRepo)
        {
            this.context = _context;
            this.genericRepo = genericRepo;
        }

        public bool CheckProductCode(string code)
        {
            bool status = false;

           var selectedProductCode = from r in context.Products
                        where r.prodCode == code
                        select r.prodCode;

            if (selectedProductCode.ToList().Count == 0 )
            {
                status = true; // True means Not available 
                return status; 
            }

            return status;
        }

        public int GetNoItemsUnavailable()
        {
          return  context.Set<Product>().Where(x => x.quantityAvailable < 1).Count();
        }

        public Product GetProductByCode(string code)
        {
            var productsFound = context.Set<Product>().Where(c => c.prodCode.Trim() == code.Trim()).Select(c => c).FirstOrDefault();

            return productsFound;

        }

        public int GetProductQuantity(int id)
        {
            return context.Products.Where(x => x.Id == id).Select(x => x.quantityAvailable).FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByShopId(int id)
        {
           return context.Set<Product>().Where(x => x.shopId == id).Select(x=>x);
           // return context.Products.Where(x => x.shopId == id).Select(x => x).ToList();
        }

        public IEnumerable<Product> FindProductByKeyword(string keyword)
        {
            var AllProductswithSearchKey = context.Set<Product>().
                   Where(x => x.Name.ToLower().Contains(keyword) || x.Category.ToLower().Contains(keyword)
                    || x.Color.ToLower().Contains(keyword) || x.prodCode.ToLower().Contains(keyword)).Select(x => x);
            return AllProductswithSearchKey;

        }

        public IEnumerable<Product> GetUnprintedProductInShop(int shopid)
        {
            return context.Set<Product>().Where(x => x.shopId == shopid && x.isPrinted == false).Select(x => x);


        }

        public bool setProductToPrinted(int productId)
        {
            var prodToSet = context.Set<Product>().Where(x => x.Id == productId).Select(x => x).FirstOrDefault();
            prodToSet.isPrinted = true;
            genericRepo.Update(prodToSet);
            if (genericRepo.Commit() > 0)
            {
                return true;
            }
            return false;


        }





    }
}
