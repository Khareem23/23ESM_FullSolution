using System;
using System.Collections.Generic;
using System.Linq;
using KaylaaShop.Core;

namespace KaylaaShop.Data
{
    public interface IOrderRepository
    {
        IEnumerable<ShoppingCartItem> GetCartItems(string cartid);

        IEnumerable<ShoppingCart> GetAllCartsByShopId(int id);

        IEnumerable<ShoppingCartItem> GetSoldItemsByShop(int id, DateTime date);


    }

    public class OrderRepository : IOrderRepository
    {
        private readonly KaylaaDataContext dbContext;

        public OrderRepository(KaylaaDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ShoppingCart> GetAllCartsByShopId(int id)
        {
            var allcart = dbContext.ShoppingCarts.Where(c => c.shopId == id).ToList();
            return allcart;
        }

        public IEnumerable<ShoppingCartItem> GetCartItems(string cartId)
        {
            var allitems = dbContext.ShoppingCartitems.Where(c => c.ShoppingCartId == cartId).ToList();
            return allitems;
        }

        public IEnumerable<ShoppingCartItem> GetSoldItemsByShop(int id, DateTime date)
        {
            List<ShoppingCartItem> allSoldItemsInShop = new List<ShoppingCartItem>();

            var allcart = dbContext.ShoppingCarts.Where(c => c.shopId == id && c.salesdate.Month == date.Month).Select(c => c.Id).ToList();


            foreach (var cartid in allcart)
            {
                var allitems = dbContext.ShoppingCartitems.Where(c => c.ShoppingCartId == cartid).ToList();
                foreach (var item in allitems)
                {
                    allSoldItemsInShop.Add(item);
                }
            }

            return allSoldItemsInShop;

        }
    }
}