using KaylaaShop.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaylaaShop.Data
{
    public interface IShoppingCartRepo
    {
      
        ShoppingCart SaveCart(ShoppingCart cart);

        IEnumerable<ShoppingCart> GetAllCarts();

        bool SubtracFromStock(ShoppingCart cart);

        IEnumerable<ShoppingCartItem> GetCartItems(string cartid);

        DateTime GetSalesdateByCartId(string id);


        int Commit();
       
    }


    public class ShoppingCartRepo : IShoppingCartRepo
    {
      
        private readonly KaylaaDataContext dbContext;

        public ShoppingCartRepo(KaylaaDataContext dbContext)
        {
           
            this.dbContext = dbContext;
        }

        public IEnumerable<ShoppingCartItem> GetCartItems(string cartid)
        {
           var AllItemsInCart =  dbContext.ShoppingCartitems.Where(c=> c.ShoppingCartId == cartid).ToList();
            return AllItemsInCart;
        }

        public IEnumerable<ShoppingCart> GetAllCarts()
        {
            return dbContext.Set<ShoppingCart>();
        }

        public ShoppingCart SaveCart(ShoppingCart cart)
        {


            var newCart = new ShoppingCart()
            {
                salesdate = cart.salesdate,
                totalQuantity = cart.totalQuantity,
                totalPrice = cart.totalPrice,
                totalProfit = cart.totalProfit,
                StaffId = cart.staff.Id,
                CustomerId = cart.customer.Id,
                shopId = cart.shopId

            };

            dbContext.ShoppingCarts.Add(newCart);
            this.Commit();

            

            foreach (var item in cart.shoppingItems)
            {
               var newItem = new ShoppingCartItem()
                {
                   AmountSold = item.AmountSold,
                   quantity = item.quantity,
                    ProductId = item.product.Id,
                    ShoppingCartId = newCart.ShoppingCartId
                };

                dbContext.ShoppingCartitems.Add(newItem);
                this.Commit();


            };

            this.SubtracFromStock(cart);

            return cart;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
          
        }

        public bool SubtracFromStock(ShoppingCart cart)
        {

            foreach (var item in cart.shoppingItems)
            {
                this.DoSubstract(item);
            }
            return true;
        }

        public bool DoSubstract(ShoppingCartItem Item)
        {
            var OldNo_in_stock = dbContext.Products.Where(c => c.Id == Item.product.Id)
                     .Select(p => p.quantityAvailable).FirstOrDefault();

            if(OldNo_in_stock > 0)
            {
                int NewNoInStock = OldNo_in_stock - Item.quantity;
                var updatedproduct = dbContext.Products.Where(c => c.Id == Item.product.Id).First();
                updatedproduct.quantityAvailable = NewNoInStock;
                this.Commit();
            }
           
            return true;
        }

        public DateTime GetSalesdateByCartId(string cartid)
        {
            var all = dbContext.ShoppingCarts.Where(c => c.ShoppingCartId == cartid).Select(c => c.salesdate).FirstOrDefault();
            return all;
        }

    }


}
