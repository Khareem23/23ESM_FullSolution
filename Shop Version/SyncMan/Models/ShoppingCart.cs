using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SyncMan.Core
{
    public class ShoppingCart: Sync
    {
        public string Id { get; set; }
        public DateTime salesdate { get; set; }
        public int totalQuantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal totalPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal totalProfit { get; set; }

        public string StaffId { get; set; }

        public int  CustomerId { get; set; }

        public int shopId { get; set; }
        //public Shop shop { get; set; }


        ////Navigational Properties 
        //public IEnumerable<ShoppingCartItem> shoppingItems { get; set; }


        //public staff staff { get; set; }
        //public Customer customer { get; set; }
    }
}
