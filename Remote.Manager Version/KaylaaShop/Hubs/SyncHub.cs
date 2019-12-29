using KaylaaShop.Core;
using KaylaaShop.Data;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaylaaShop.Hubs
{
    public class SyncHub : Hub
    {
        // client call this method
        //public async Task SendMesssage(string connectionid, ClientNotification clientnotifications)
        //{
        //    // send data to Manager ( Online ) Part
        //    await Clients.Client(connectionid).SendAsync("ReceiveMessage", clientnotifications);
        //}
        static Dictionary<string, string> connections = new Dictionary<string, string>();
        public async Task SendMesssage(string connectionid,string name,string message)
        {
            // send data to Manager ( Online ) Part
            await Clients.Client(connectionid).SendAsync("ReceiveMessage", name,message);
        }

        public async Task Login(string shopid, string email)
        {
            string connectionid = Context.ConnectionId;
            if (connections.Where(c => c.Value == shopid).Any())
            {

            }
            else
            {
                connections.Add(connectionid, shopid);
                await Clients.Client(connectionid).SendAsync("LoggedIn", connectionid);
            }
        }

        public async Task DoSync(string shopId, string data)
        {
            try
            {
                if (connections.Where(c => c.Value == shopId).Any())
                {
                    var conn = connections.Where(c => c.Value == shopId).FirstOrDefault();
                    //get data from clients
                    var d = JsonConvert.DeserializeObject<List<SyncManager>>(data);
                    List<string> persistedentries = new List<string>();
                    //persist on the server
                    foreach (var item in d)
                    {
                        item.DateApplied = DateTime.Now;
                        int c = DAL.GenericOperation<SyncManager>(item, item.Action);
                        if (c > 0)
                        {
                            persistedentries.Add(item.SyncManagerId.ToString());
                            //process specific entry
                            string dataspec = item.State;
                            CommitToDB(item,item.Entity,item.Action);
                        }
                        else
                        {
                            int v=CommitToDB(item, item.Entity, item.Action);
                            if(v>0)
                            {
                                persistedentries.Add(item.SyncManagerId.ToString());
                            }
                        }
                    }
                    
                    //get items to sync to local for this shop
                    int shid = 0;
                    shid = int.Parse(shopId);
                    var unsynced = DAL.GetUnsyncedData().Where(c => c.ShopId == shid).ToList();
                    string datatosendtolocal = JsonConvert.SerializeObject(unsynced);
                    //respond with caller specific data for local sync on the client side
                    await Clients.Client(conn.Key).SendAsync("ReceiveSync", shopId, persistedentries, "");
                    //foreach(var item in unsynced)
                    //{
                    //    DAL.MarkAsSynced(item.SyncManagerId.ToString());
                    //}
                }
                else
                {
                    await Clients.Client(Context.ConnectionId).SendAsync("ReceiveSync", shopId, null, "INVALID REQUEST, LOGIN FIRST");
                }
            }
            catch(Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveSync", shopId, null, "SYNC FAILED | "+ex.ToString());
            }
        }

        public async Task DoRemoteSync(string shopId, string data)
        {
            try
            {
                if (connections.Where(c => c.Value == shopId).Any())
                {
                    var conn = connections.Where(c => c.Value == shopId).FirstOrDefault();
                    //get data from clients
                    
                    int shid = int.Parse(shopId);
                    var unsynced = DAL.GetUnsyncedData().Where(c => c.ShopId == shid).ToList();
                    string datatosendtolocal = JsonConvert.SerializeObject(unsynced);
                    //respond with caller specific data for local sync on the client side
                    await Clients.Client(conn.Key).SendAsync("ReceiveSync2", shopId, null, datatosendtolocal);
                    foreach (var item in unsynced)
                    {
                        DAL.MarkAsSynced(item.SyncManagerId.ToString());
                    }
                }
                else
                {
                    await Clients.Client(Context.ConnectionId).SendAsync("ReceiveSync", shopId, null, "INVALID REQUEST, LOGIN FIRST");
                }
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveSync", shopId, null, "SYNC FAILED | " + ex.ToString());
            }
        }

        public static int CommitToDB(SyncManager item,string entity,string action)
        {
                //dataAccess.GenericOperation(item.State)
                int c = 0;
            try
            {
                switch (item.Entity)
                {
                    case "Customer" :
                        Customer customer = (Customer)JsonConvert.DeserializeObject<Customer>(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c = DAL.AddSyncObjectToDB(customer, entity.ToLower());
                            break;
                        }                        
                        c = DAL.GenericOperation<Customer>(customer, action);
                        break;
                    case "Expenses":
                        Expenses expenses = (Expenses)JsonConvert.DeserializeObject(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c = DAL.AddSyncObjectToDB(expenses, entity.ToLower());
                            break;
                        }
                        c = DAL.GenericOperation<Expenses>(expenses, action);
                        break;
                    case "ShoppingCart":
                        ShoppingCart shoppingCart = (ShoppingCart)JsonConvert.DeserializeObject(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c = DAL.AddSyncObjectToDB(shoppingCart, entity.ToLower());
                            break;
                        }
                        c = DAL.GenericOperation<ShoppingCart>(shoppingCart, action);
                        break;
                    case "ShoppingCartItem":
                        ShoppingCartItem shoppingCartItem = (ShoppingCartItem)JsonConvert.DeserializeObject(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c = DAL.AddSyncObjectToDB(shoppingCartItem, entity.ToLower());
                            break;
                        }
                        c = DAL.GenericOperation<ShoppingCartItem>(shoppingCartItem, action);
                        break;
                    case "Product":
                        Product product = (Product)JsonConvert.DeserializeObject(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c = DAL.AddSyncObjectToDB(product, entity.ToLower());
                            break;
                        }
                        c = DAL.GenericOperation<Product>(product, action);
                        break;
                    case "staff":
                        staff staff = (staff)JsonConvert.DeserializeObject(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c=DAL.AddSyncObjectToDB(staff, entity.ToLower());
                            break;
                        }
                        c = DAL.GenericOperation<staff>(staff, action);
                        break;
                }
                if (c > 0)
                {
                    DAL.MarkAsSynced(item.SyncManagerId.ToString());
                }
            }
            catch(Exception ex)
            {

            }
            return c;
        }

        public static void Download(List<SyncManager> list)
        {
           
            foreach (var item in list)
            {
                CommitToDB(item, item.Entity, item.Action);
                //dataAccess.GenericOperation(item.State)
                //int c = 0;
                //switch (item.Entity)
                //{
                //    case "Customer":
                //        Customer customer = (Customer)JsonConvert.DeserializeObject<Customer>(item.State);
                //        c = DAL.GenericOperation<Customer>(customer, item.Action);
                //        break;
                //    case "Expenses":
                //        Expenses expenses = (Expenses)JsonConvert.DeserializeObject(item.State);
                //        c = DAL.GenericOperation<Expenses>(expenses, item.Action);
                //        break;
                //    case "ShoppingCart":
                //        ShoppingCart shoppingCart = (ShoppingCart)JsonConvert.DeserializeObject(item.State);
                //        c = DAL.GenericOperation<ShoppingCart>(shoppingCart, item.Action);
                //        break;
                //    case "ShoppingCartItem":
                //        ShoppingCartItem shoppingCartItem = (ShoppingCartItem)JsonConvert.DeserializeObject(item.State);
                //        c = DAL.GenericOperation<ShoppingCartItem>(shoppingCartItem, item.Action);
                //        break;
                //}
                //if(c>0)
                //{
                //    DAL.MarkAsSynced(item.SyncManagerId.ToString());
                //}
            }
        }

        public override Task OnConnectedAsync()
        {
            string conId=Context.ConnectionId;
            if(connections.Where(c => c.Key==conId).Any())
            {

            }
            return Task.FromResult(0);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string conId = Context.ConnectionId;
            connections.Remove(conId);
            try
            {
                string reason = exception.ToString();
            }
            catch { }
            return base.OnDisconnectedAsync(exception);
        }



    }
}
