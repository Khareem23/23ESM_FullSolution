using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SyncMan.Core
{
    public class ShoppingCartItem: Sync
    {
        public int Id { get; set; }
        public int quantity { get; set; }
      
        public int ProductId { get; set; }
        public string ShoppingCartId { get; set; }

        public Decimal AmountSold { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal profit { get; set; }

        //Navigation Properties , always define Id for Every Objects you define 

        public Product product { get; set; }

        //public int shopId { get; set; }
        //public Shop shop { get; set; }



    }
}
