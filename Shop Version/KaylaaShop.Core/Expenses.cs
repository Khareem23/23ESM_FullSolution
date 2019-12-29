using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaylaaShop.Core
{
    public class Expenses: Sync
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime date { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Decimal amount { get; set; }

        public int shopId { get; set; }
        public Shop shop { get; set; }
    }




}
