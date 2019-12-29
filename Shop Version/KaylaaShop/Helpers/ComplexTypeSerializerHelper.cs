using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaylaaShop.Helpers
{
    public static class ComplexTypeSerializerHelper 
    {
        public static string SerializeObject<T>(List<T> tempData)
        {
            return JsonConvert.SerializeObject(tempData);
        }
        public static List<T> DeserializeObject<T>(string tempData)
        {
            if (tempData.Length == 0)
            {
                return new List<T>();
            }
            return JsonConvert.DeserializeObject<List<T>>(tempData);
        }
    }
}
