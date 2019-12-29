using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SyncMan.Core;
using SyncMan.Network;

namespace SyncMan
{
    public class Worker : BackgroundService
    {
        SignalR signalR = new SignalR();
        private readonly ILogger<Worker> _logger;
        DataAccess dataAccess = new DataAccess();
        public static int shopId = 1;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        //start websocket listener

        //check events awaiting synchronization

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                if (Internet.IsConnected())
                {
                    Upload();
                }

                await Task.Delay(5000, stoppingToken);
            }
        }

        void Upload()
        {
            List<SyncManager> unsynced = dataAccess.GetUnsyncedData();
            try
            {
                signalR.Push(shopId.ToString(), unsynced);
            }
            catch (Exception ex)
            {
                Program.InitializeSignalR();
            }
        }

        public static void Download(List<SyncManager> list)
        {
            DataAccess dataAccess = new DataAccess();
            foreach(var item in list)
            {
                int c = dataAccess.GenericOperation<SyncManager>(item, "added");
                CommitToDB(item, item.Entity.ToLower(), item.Action);
              
            }
        }


        // This Method is called by Download Method 
        public static void CommitToDB(SyncManager item, string entity, string action)
        {
            DataAccess dataAccess = new DataAccess();
            //dataAccess.GenericOperation(item.State)
            int c = 0;
            try
            {
                switch (item.Entity)
                {
                    case "customer":
                        Customer customer = (Customer)JsonConvert.DeserializeObject<Customer>(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c = dataAccess.AddSyncObjectToDB(customer, entity.ToLower());
                            break;
                        }
                        c = dataAccess.GenericOperation<Customer>(customer, action);
                        break;
                    case "expenses":
                        try
                        {
                            Expenses expenses = (Expenses)JsonConvert.DeserializeObject<Expenses>(item.State);
                            if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                            {
                                c = dataAccess.AddSyncObjectToDB(expenses, entity.ToLower());
                                break;
                            }
                            c = dataAccess.GenericOperation<Expenses>(expenses, action);
                            break;
                        }
                        catch(Exception ex)
                        {
                            break;
                        }
                    case "shoppingcart":
                        ShoppingCart shoppingCart = (ShoppingCart)JsonConvert.DeserializeObject(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c = dataAccess.AddSyncObjectToDB(shoppingCart, entity.ToLower());
                            break;
                        }
                        c = dataAccess.GenericOperation<ShoppingCart>(shoppingCart, action);
                        break;
                    case "shoppingcartitem":
                        ShoppingCartItem shoppingCartItem = (ShoppingCartItem)JsonConvert.DeserializeObject(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c = dataAccess.AddSyncObjectToDB(shoppingCartItem, entity.ToLower());
                            break;
                        }
                        c = dataAccess.GenericOperation<ShoppingCartItem>(shoppingCartItem, action);
                        break;
                    case "product":
                        Product product = (Product)JsonConvert.DeserializeObject(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c = dataAccess.AddSyncObjectToDB(product, entity.ToLower());
                            break;
                        }
                        c = dataAccess.GenericOperation<Product>(product, action);
                        break;
                    case "staff":
                        staff staff = (staff)JsonConvert.DeserializeObject(item.State);
                        if (action.ToLower() == "added" && !entity.ToLower().Contains("syncmanager"))
                        {
                            c = dataAccess.AddSyncObjectToDB(staff, entity.ToLower());
                            break;
                        }
                        c = dataAccess.GenericOperation<staff>(staff, action);
                        break;
                }
                if (c > 0)
                {
                    dataAccess.MarkAsSynced(item.SyncManagerId.ToString());
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}
