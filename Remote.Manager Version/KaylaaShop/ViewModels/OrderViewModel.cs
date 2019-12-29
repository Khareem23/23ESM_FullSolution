using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaylaaShop.ViewModels
{
    public class OrderViewModel
    {
        public string ShoppingCartId { get; set; }
        public DateTime salesdate { get; set; }
        public int totalQuantity { get; set; }
        public decimal totalPrice { get; set; }
        public decimal totalProfit { get; set; }

        public string customerName { get; set; }
        public string staffName { get; set; }

        public int shopId { get; set; }

    }
}
