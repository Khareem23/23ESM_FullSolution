using KaylaaShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaylaaShop.Data
{
    public interface IShopRepository
    {
        Shop GetShopById(int id);
    }

    public class ShopRepository : IShopRepository
    {
        private readonly KaylaaDataContext context;

        public ShopRepository(KaylaaDataContext _context )
        {
            context = _context;
        }


        public Shop GetShopById(int id)
        {
            return context.shops.Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
