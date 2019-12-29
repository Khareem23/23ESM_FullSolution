using KaylaaShop.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KaylaaShop.Data
{
    public class KaylaaDataContext : IdentityDbContext<IdentityUser>
    {


        public KaylaaDataContext(DbContextOptions<KaylaaDataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var connectionString = @"Data Source=SQL5042.site4now.net;Initial Catalog=DB_9D9472_kaylaaDb;User Id=DB_9D9472_kaylaaDb_admin;Password=@Kaylaa_2019#";
                var connectionString = @"Data Source=.;Initial Catalog=23esmdb;Integrated Security=True";
                optionsBuilder.UseSqlServer(connectionString);

            }
        }

        public override int SaveChanges()
        {
            ProcessEntry();
            //SetModifiedInformation();
            int x = base.SaveChanges();
          
            //check object, if insert,update or delete

            return x;

        }
        
        public void ProcessEntry()
        {
            string holdstate = "";
            Task.Run(() => {
                foreach (var entityEntry in ChangeTracker.Entries())
                {
                    string state = entityEntry.State.ToString();
                    holdstate = state;
                    string objName = entityEntry.Entity.GetType().ToString();
                    var entity = entityEntry.Entity as ChangeTracking;
                    //string id = entity.Id;
                    if (entityEntry.State == EntityState.Modified
                        || entityEntry.State == EntityState.Added
                        || entityEntry.State == EntityState.Deleted)
                    {
                        string data = "";
                        // use entry.OriginalValues
                        if (objName.Contains("ProductBrand"))
                        {
                            var productBrand = CreateWithValues<ProductBrand>(entityEntry, entityEntry.OriginalValues);
                            // productBrand=(ProductBrand)SetSyncData(productBrand);
                        }

                        if (objName.ToLower()=="product")
                        {
                            var prod = CreateWithValues<Product>(entityEntry, entityEntry.OriginalValues);
                            //add sync details
                            prod = (Product)SetSyncData(prod, entityEntry.State.ToString());
                            //log into SyncManager
                            int v = LogSyncObject(prod, prod.shopId, holdstate);
                            prod = (Product)SetSyncDataPost(prod, entityEntry.State.ToString());
                        }

                        if (objName.ToLower() == "staff")
                        {
                            var prod = CreateWithValues<staff>(entityEntry, entityEntry.OriginalValues);
                            //add sync details
                            prod = (staff)SetSyncData(prod, entityEntry.State.ToString());
                            //log into SyncManager
                            int v = LogSyncObject(prod, (int)prod.shopId, holdstate);
                            prod = (staff)SetSyncDataPost(prod, entityEntry.State.ToString());
                        }

                        if (objName.Contains("ShoppingCart"))
                        {
                            var shoppingCart = CreateWithValues<ShoppingCart>(entityEntry, entityEntry.OriginalValues);
                            //add sync details
                            shoppingCart = (ShoppingCart)SetSyncData(shoppingCart, entityEntry.State.ToString());
                            //log into SyncManager
                            int v = LogSyncObject(shoppingCart, shoppingCart.shopId, holdstate);
                            shoppingCart = (ShoppingCart)SetSyncDataPost(shoppingCart, entityEntry.State.ToString());

                        }

                        if (objName.Contains("ShoppingCartItem"))
                        {
                            var shoppingCartItem = CreateWithValues<ShoppingCartItem>(entityEntry, entityEntry.OriginalValues);
                            //add sync details
                            shoppingCartItem = (ShoppingCartItem)SetSyncData(shoppingCartItem, entityEntry.State.ToString());
                            //log into SyncManager

                            var item = this.ShoppingCarts.Where(i => i.ShoppingCartId == shoppingCartItem.ShoppingCartId).FirstOrDefault();

                            int v = LogSyncObject(shoppingCartItem, item.shopId, holdstate);
                            shoppingCartItem = (ShoppingCartItem)SetSyncDataPost(shoppingCartItem, entityEntry.State.ToString());

                        }

                        if (objName.Contains("Expenses"))
                        {
                            var expenses = CreateWithValues<Expenses>(entityEntry, entityEntry.OriginalValues);

                            //add sync details
                            expenses = (Expenses)SetSyncData(expenses, entityEntry.State.ToString());
                            //log into SyncManager

                            int v = LogSyncObject(expenses, expenses.shopId, holdstate);
                            expenses.Id = v;
                            expenses = (Expenses)SetSyncDataPost(expenses, entityEntry.State.ToString());

                        }

                        if (objName.Contains("Customer"))
                        {
                            var customer = CreateWithValues<Customer>(entityEntry, entityEntry.OriginalValues);

                            //add sync details
                            customer = (Customer)SetSyncData(customer, entityEntry.State.ToString());
                            //log into SyncManager

                            int v = LogSyncObject(customer, customer.shopId, entityEntry.State.ToString());
                            customer = (Customer)SetSyncDataPost(customer, entityEntry.State.ToString());

                        }
                    }

                }
            });

        }

        private object SetSyncData(object entity, string action)
        {
            var objName = entity.GetType().Name;

            if (objName.Contains("ShoppingCartItem"))
            {
                ShoppingCartItem cart = (ShoppingCartItem)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;
                //DAL.UpdateShoppingCartitemsSync(cart);

            }

            if (objName.Contains("ShoppingCart"))
            {
                ShoppingCart cart = (ShoppingCart)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;
                //DAL.UpdateShoppingCartSync(cart);

            }

            if (objName.Contains("Expenses"))
            {
                Expenses cart = (Expenses)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;
                //DAL.UpdateExpenseSync(cart);
            }

            if (objName.Contains("Customer"))
            {
                Customer cart = (Customer)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;
                //DAL.UpdateCustomerSync(cart);
            }
            if (objName == "Product")
            {
                Product cart = (Product)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;
                //DAL.UpdateProductSync(cart);
            }
            if (objName == "staff")
            {
                staff cart = (staff)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;
                //DAL.UpdateStaffSync(cart);
            }
            return entity;
        }

        private object SetSyncDataPost(object entity, string action)
        {
            var objName = entity.GetType().Name;

            if (objName.Contains("ShoppingCartItem"))
            {
                ShoppingCartItem cart = (ShoppingCartItem)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = cart.SyncTrackId ;
                cart.UnSyncedEvents = 1;
                entity = cart;
                DAL.UpdateShoppingCartitemsSync(cart);

            }

            if (objName.Contains("ShoppingCart"))
            {
                ShoppingCart cart = (ShoppingCart)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                //cart.SyncTrackId = Guid.NewGuid();
                cart.SyncTrackId = cart.SyncTrackId;
                cart.UnSyncedEvents = 1;
                entity = cart;
                DAL.UpdateShoppingCartSync(cart);

            }

            if (objName.Contains("Expenses"))
            {
                Expenses cart = (Expenses)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                //cart.SyncTrackId = Guid.NewGuid();
                cart.SyncTrackId = cart.SyncTrackId;
                cart.UnSyncedEvents = 1;
                entity = cart;
                DAL.UpdateExpenseSync(cart);
            }

            if (objName.Contains("Customer"))
            {
                Customer cart = (Customer)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                //cart.SyncTrackId = Guid.NewGuid();
                cart.SyncTrackId = cart.SyncTrackId;
                cart.UnSyncedEvents = 1;
                entity = cart;
                DAL.UpdateCustomerSync(cart);
            }
            if (objName == "Product")
            {
                Product cart = (Product)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                //cart.SyncTrackId = Guid.NewGuid();
                cart.SyncTrackId = cart.SyncTrackId;
                cart.UnSyncedEvents = 1;
                entity = cart;
                DAL.UpdateProductSync(cart);
            }
            if (objName == "staff")
            {
                staff cart = (staff)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                //cart.SyncTrackId = Guid.NewGuid();
                cart.SyncTrackId = cart.SyncTrackId;
                cart.UnSyncedEvents = 1;
                entity = cart;
                DAL.UpdateStaffSync(cart);
            }
            return entity;
        }

        private int LogSyncObject(object entity, int shopId, string action)
        {
            var objName = entity.GetType().Name;
            Guid syncId = Guid.Empty;
            if (objName.Contains("ShoppingCartItem"))
            {
                ShoppingCartItem cart = (ShoppingCartItem)entity;
                entity = cart;
                syncId = cart.SyncTrackId;

            }

            if (objName.Contains("ShoppingCart"))
            {
                ShoppingCart cart = (ShoppingCart)entity;
                entity = cart;
                syncId = cart.SyncTrackId;

            }

            if (objName.Contains("Expenses"))
            {
                Expenses cart = (Expenses)entity;
                entity = cart;
                syncId = cart.SyncTrackId;
            }

            if (objName.Contains("Customer"))
            {
                Customer cart = (Customer)entity;
                entity = cart;
                syncId = cart.SyncTrackId;

            }
            if (objName=="staff")
            {
                staff cart = (staff)entity;
                entity = cart;
                syncId = cart.SyncTrackId;

            }

            if (objName.ToLower() == "product")
            {
                Product cart = (Product)entity;
                entity = cart;
                syncId = cart.SyncTrackId;

            }

            SyncManager man = new Core.SyncManager
            {
                Action = action,
                DateLogged = DateTime.Now,
                Entity = objName,
                ShopId = shopId,
                SourceDataStore = "Remote",
                SourceDataStoreType = "SQLServer",
                DestinationDataStore = "Local",
                DestinationDataStoreType = "SQLLite",
                State = Newtonsoft.Json.JsonConvert.SerializeObject(entity),
                SyncTrackId = syncId,
                SyncExecutionStatus = 0,
                SyncManagerId = Guid.NewGuid()
            };

            int c = DAL.GenericOperation<SyncManager>(man, action,objName);
            return c;
        }

        private T CreateWithValues<T>(EntityEntry entry, PropertyValues values) where T : new()
        {
            T entity = new T();
            Type type = typeof(T);
            foreach (var name in values.Properties)
            {
                var property = type.GetProperty(name.Name);
                //property.SetValue(entity, values.GetValue<object>(name.Name));
                property.SetValue(entity, entry.Property(name.Name).CurrentValue);
            }
            return entity;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //ProcessEntry();
            return await base.SaveChangesAsync(cancellationToken);
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductCountry> ProductCountries { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartitems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<staff> Staffs { get; set; }
        public DbSet<Shop> shops { get; set; }
        //public DbSet<ServerNotifitcation> ServerNotifications {get ; set ;}
        //public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<SyncManager> SyncManager { get; set; }






    }
}
