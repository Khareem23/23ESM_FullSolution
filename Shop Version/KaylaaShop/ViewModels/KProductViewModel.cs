using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaylaaShop.ViewModels
{
    public class KProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal costPrice { get; set; }
        public string productImageUrl { get; set; }
        public string prodCode { get; set; }
        public int quantityAvailable { get; set; }
        public bool status { get; set; }   // Availabity Status 
        public DateTime lastStockDate { get; set; }

        public string prodSize { get; set; }
        public Decimal discountSellingPrice { get; set; }
        public Decimal NormalSellingPrice { get; set; }

        public double? salesDiscount { get; set; } = 0.0;
        public int productCategoryId { get; set; }
        public int productCountryId { get; set; }
        public int productColorId { get; set; }
        public int productBrandId { get; set; }

        public int shopId { get; set; }

    }
}
