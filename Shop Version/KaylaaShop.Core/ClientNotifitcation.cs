using System;
using System.Collections.Generic;
using System.Text;

namespace KaylaaShop.Core
{
    public class ClientNotifitcation<T>  where T : class
    {
        public int id { get; set; }
        public EventType eventtriggered { get; set; }
        public string entityType { get; set; }
        public int? Qty { get; set; }
        public T  entityObject { get; set; }
        public DateTime createTime { get; set; }
    }
}
