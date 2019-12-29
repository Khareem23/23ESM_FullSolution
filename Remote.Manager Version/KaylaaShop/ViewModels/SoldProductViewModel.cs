using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaylaaShop.ViewModels
{
    public class SoldProductViewModel
    {
        public string ProductImageUrl { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public Decimal CostPrice { get; set; }
        public Decimal AmountSold { get; set; }
        public Decimal TotalSellingPrice { get; set; }
        public decimal VAT { get; set; }
        public int Quantity { get; set; }
        public string DateSold { get; set; }
    }
}
