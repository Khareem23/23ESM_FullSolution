using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SyncMan.Core;

namespace SyncMan
{
    public class Program
    {        
        static DataAccess dataAccess = new DataAccess();

        public static HubConnection connection;



        public static void Main(string[] args)
        {
            InitializeSignalR();
            CreateHostBuilder(args).Build().Run();
        }

        public static void InitializeSignalR()
        {
            connection = new HubConnectionBuilder()
               .WithUrl("http://localhost:5000/syncHub")
               .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<string, List<string>, string>("ReceiveSync", (sid, synsedsuccessids, datatosync) =>
            {
                if (synsedsuccessids == null)
                {
                    string msg = datatosync;
                    return;
                }
                foreach (var item in synsedsuccessids)
                {
                    dataAccess.MarkAsSynced(item);
                }
               
            });

            connection.On<string, List<string>, string>("ReceiveSync2", (sid, synsedsuccessids, datatosync) =>
            {
                if (string.IsNullOrEmpty(datatosync))
                {
                    string msg = "Nothing to sync";
                    return;
                }
                var sm = JsonConvert.DeserializeObject<List<SyncManager>>(datatosync);
                //foreach (var item in synsedsuccessids)
                //{
                //    dataAccess.MarkAsSynced(item);
                //}
                if (sm?.Count > 0)
                {
                    Worker.Download(sm);
                }
            });

            try
            {
                connection.StartAsync().Wait();
                //connection started
                connection.InvokeAsync("Login", Worker.shopId.ToString(), "").Wait();
            }
            catch (Exception ex)
            {

            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}