using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncMan.Network
{
    public class SignalR
    {
        public async void Push(string shopid, List<SyncMan.Core.SyncManager> list)
        {
            try
            {
                string data = JsonConvert.SerializeObject(list);
                await Program.connection.InvokeAsync("DoSync", shopid, data);
                await Program.connection.InvokeAsync("DoRemoteSync", shopid, data);
            }
            catch(Exception ex)
            {
                Program.InitializeSignalR();
            }
        }
    }
}
