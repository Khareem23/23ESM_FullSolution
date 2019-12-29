using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncMan.Core
{
    public class Expenses: Sync
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }

      //  [Column(TypeName = "decimal(18, 2)")]
        public Decimal amount { get; set; }

        public int shopid { get; set; }
        //public Shop shop { get; set; }
    }




}
