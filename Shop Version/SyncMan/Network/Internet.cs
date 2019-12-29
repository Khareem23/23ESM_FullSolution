using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SyncMan
{
    public class Internet
    {
        public static bool IsConnected()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
