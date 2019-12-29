using KaylaaShop.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//using Microsoft.Extensions.Configuration.FileExtensions;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KaylaaShop.Data
{
    public class KaylaaDataContext :IdentityDbContext<IdentityUser>
    {

        public KaylaaDataContext(DbContextOptions<KaylaaDataContext> options) : base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //   => options.UseSqlite("Data Source=DB_MakeUpResidence.db");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var connectionString = @"Data Source=DB_Shop.db";
                //optionsBuilder.UseSqlite(connectionString);

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
            Task.Run(() => {
                foreach (var entityEntry in ChangeTracker.Entries())
                {
                    string state = entityEntry.State.ToString();
                    string objName = entityEntry.Entity.GetType().ToString();
                    var entity = entityEntry.Entity as ChangeTracking;
                    //string id = entity.Id;
                    if (entityEntry.State == EntityState.Modified
                        || entityEntry.State == EntityState.Added
                        || entityEntry.State == EntityState.Deleted)
                    {
                        
                        // use entry.OriginalValues
                        if (objName.Contains("ProductBrand"))
                        {
                            var productBrand = CreateWithValues<ProductBrand>(entityEntry, entityEntry.OriginalValues);
                           // productBrand=(ProductBrand)SetSyncData(productBrand);
                        }

                        if (objName.Contains("ShoppingCart"))
                        {
                            var shoppingCart = CreateWithValues<ShoppingCart>(entityEntry, entityEntry.OriginalValues);
                            //add sync details
                            shoppingCart = (ShoppingCart)SetSyncData(shoppingCart, entityEntry.State.ToString());
                            //log into SyncManager
                          
                                int v = LogSyncObject(shoppingCart, shoppingCart.shopId, entityEntry.State.ToString());
                           
                        }

                        if (objName.Contains("ShoppingCartItem"))
                        {
                            var shoppingCartItem = CreateWithValues<ShoppingCartItem>(entityEntry, entityEntry.OriginalValues);
                            //add sync details
                            shoppingCartItem = (ShoppingCartItem)SetSyncData(shoppingCartItem, entityEntry.State.ToString());
                            //log into SyncManager
                          
                                var item=this.ShoppingCarts.Where(i => i.Id == shoppingCartItem.ShoppingCartId).FirstOrDefault();

                                int v = LogSyncObject(shoppingCartItem, item.shopId, entityEntry.State.ToString());
                          
                        }

                        if (objName.Contains("Expenses"))
                        {
                            var expenses = CreateWithValues<Expenses>(entityEntry, entityEntry.OriginalValues);

                            //add sync details
                            expenses = (Expenses)SetSyncData(expenses, entityEntry.State.ToString());
                            //log into SyncManager
                         
                                int v = LogSyncObject(expenses, expenses.shopId, entityEntry.State.ToString());
                           
                        }

                        if (objName.Contains("Customer"))
                        {
                            var customer = CreateWithValues<Customer>(entityEntry, entityEntry.OriginalValues);

                            //add sync details
                            customer = (Customer)SetSyncData(customer, entityEntry.State.ToString());
                            //log into SyncManager
                          
                                int v = LogSyncObject(customer, customer.shopId, entityEntry.State.ToString());
                          
                        }

                        if (objName=="Product")
                        {
                            var customer = CreateWithValues<Product>(entityEntry, entityEntry.OriginalValues);

                            //add sync details
                            customer = (Product)SetSyncData(customer, entityEntry.State.ToString());
                            //log into SyncManager

                            int v = LogSyncObject(customer, customer.shopId, entityEntry.State.ToString());

                        }

                        if (objName == "staff")
                        {
                            var customer = CreateWithValues<staff>(entityEntry, entityEntry.OriginalValues);

                            //add sync details
                            customer = (staff)SetSyncData(customer, entityEntry.State.ToString());
                            //log into SyncManager

                            int v = LogSyncObject(customer, (int)customer.shopId, entityEntry.State.ToString());

                        }
                    }

                }
            });

        }
        
        private object SetSyncData(object entity,string action)
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
               
            }

            if (objName.Contains("ShoppingCart"))
            {
                ShoppingCart cart = (ShoppingCart)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;

            }

            if (objName.Contains("Expenses"))
            {
                Expenses cart = (Expenses)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;
            }

            if (objName.Contains("Customer"))
            {
                Customer cart = (Customer)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;
            }
            if (objName=="Product")
            {
                Product cart = (Product)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;
            }
            if (objName.Contains("staff"))
            {
                staff cart = (staff)entity;
                cart.DateSynced = DateTime.Now;
                cart.SyncStatus = 0;
                cart.SyncTrackId = Guid.NewGuid();
                cart.UnSyncedEvents = 1;
                entity = cart;
            }
            return entity;
        }
        
        private int LogSyncObject(object entity, int shopId, string action)
        {
            SqlLiteDAL dal = new SqlLiteDAL();
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

            if (objName.Contains("staff"))
            {
                staff cart = (staff)entity;
                entity = cart;
                syncId = cart.SyncTrackId;

            }
            if (objName=="Product")
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
                DestinationDataStore = "Remote",
                DestinationDataStoreType = "SQLServer",
                SourceDataStore = "Local",
                SourceDataStoreType = "SQLLite",
                State = Newtonsoft.Json.JsonConvert.SerializeObject(entity),
                SyncTrackId = syncId,
                SyncExecutionStatus = 0,
                SyncManagerId = Guid.NewGuid()
            };

            int c = dal.GenericOperation<SyncManager>(man, "added");
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

        public DbSet<SyncManager> SyncManager { get; set; }




    }

   
}
