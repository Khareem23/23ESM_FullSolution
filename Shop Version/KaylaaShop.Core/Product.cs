using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaylaaShop.Core
{
    public class Product : Sync
    {
        public int Id { get; set; }
        public string  Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Decimal costPrice { get; set; }


        public string productBarcodeUrl { get; set; }
        public string  productImageUrl { get; set; }
        public string  prodCode { get; set; }
        public int quantityAvailable { get; set; }
        public bool status { get; set; }   // Availabity Status 
        public DateTime lastStockDate { get; set; }

        public string prodSize { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public Decimal discountSellingPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Decimal NormalSellingPrice { get; set; }

        public double? salesDiscount { get; set; }

        public string Category { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Country { get; set; }

        public bool isPrinted { get; set; }
        public int shopId { get; set; }
        public Shop shop { get; set; }



     
      


        public Product()
        {
            lastStockDate = DateTime.Now;
            status = true;
        }


    }




}
