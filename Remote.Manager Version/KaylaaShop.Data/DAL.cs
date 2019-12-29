using Dapper;
using KaylaaShop.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KaylaaShop.Data
{
    public class DAL
    {
        public static SqlConnection SimpleDbConnection()
        {
            //var connectionString = @"Data Source=SQL5042.site4now.net;Initial Catalog=DB_9D9472_kaylaaDb;User Id=DB_9D9472_kaylaaDb_admin;Password=@Kaylaa_2019#";
            //var connectionString = @"Data Source=sql5047.site4now.net;Initial Catalog=DB_9D9472_23esmdb;User Id=DB_9D9472_23esmdb_admin;Password=@Demo_2019#" ;
            var connectionString = @"Data Source=.;Initial Catalog=23esmdb;Integrated security=true";
            return new SqlConnection(connectionString);
        }

       
        public static List<SyncManager> GetUnsyncedData()
        {
           
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                List<SyncManager> result = cnn.Query<SyncManager>(
                    @"SELECT Id, SyncManagerId, SyncTrackId, ShopId, SourceDataStore, SourceDataStoreType, DestinationDataStore, DestinationDataStoreType, Entity,
                           State, Action, SyncExecutionStatus, DateLogged, DateApplied
            FROM SyncManager
            WHERE SyncExecutionStatus=0", null).ToList();
                return result;
            }
        }

        public static void MarkAsSynced(string id)
        {
            SqlCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = "UPDATE SyncManager SET SyncExecutionStatus = 1 WHERE SyncManagerId = '" + id + "'";
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }


        #region UPDATESYNCDATA_ON_OBJECS IN DB
        public static void UpdateCustomerSync(Customer cust)
        {
            SqlCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = "UPDATE Customers SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=@trackid,UnSyncedEvents=1 WHERE id=@id";
                sqlite_cmd.Parameters.Add(new SqlParameter("@trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@id", cust.Id));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public static void UpdateExpenseSync(Expenses cust)
        {
            SqlCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = "UPDATE Expenses SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=@trackid,UnSyncedEvents=1 WHERE id=@id";
                sqlite_cmd.Parameters.Add(new SqlParameter("@trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@id", cust.Id));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
        //ShoppingCartitems
        public static void UpdateShoppingCartitemsSync(ShoppingCartItem cust)
        {
            SqlCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"UPDATE ShoppingCartitems SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=@trackid,UnSyncedEvents=1 " +
                    "WHERE shoppingCartItemId=@id";
                sqlite_cmd.Parameters.Add(new SqlParameter("@trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@id", cust.shoppingCartItemId));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public static void UpdateShoppingCartSync(ShoppingCart cust)
        {
            SqlCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"UPDATE ShoppingCarts SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=@trackid,UnSyncedEvents=1 " +
                    "WHERE ShoppingCartId=@id";
                sqlite_cmd.Parameters.Add(new SqlParameter("@trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@id", cust.ShoppingCartId));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public static void UpdateProductSync(Product cust)
        {
            SqlCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = "UPDATE Product SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=@trackid,UnSyncedEvents=1 WHERE id=@id";
                sqlite_cmd.Parameters.Add(new SqlParameter("@trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@id", cust.Id));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public static void UpdateStaffSync(staff cust)
        {
            SqlCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = "UPDATE AspNetUsers SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=@trackid,UnSyncedEvents=1 WHERE id=@id";
                sqlite_cmd.Parameters.Add(new SqlParameter("@trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@id", cust.Id));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
        #endregion

        #region ADDSYNCDATATOTABLES
        public static int AddCustomer(Customer cust)
        {
            SqlCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO [dbo].[Customers]
           ([Name]
           ,[gender]
           ,[address]
           ,[phoneNumber]
           ,[email]
           ,[shopId]
           ,[DateSynced]
           ,[SyncId]
           ,[SyncStatus]
           ,[SyncTrackId]
           ,[UnSyncedEvents])
     VALUES
           (@Name
           ,@gender
           ,@address
           ,@phoneNumber
           ,@email
           ,@shopId
           ,@DateSynced
           ,@SyncId
           ,@SyncStatus
           ,@SyncTrackId
           ,@UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SqlParameter("@Name", cust.Name));
                sqlite_cmd.Parameters.Add(new SqlParameter("@gender", cust.gender));
                sqlite_cmd.Parameters.Add(new SqlParameter("@address", cust.address));
                sqlite_cmd.Parameters.Add(new SqlParameter("@phonenumber", cust.phoneNumber));
                sqlite_cmd.Parameters.Add(new SqlParameter("@email", cust.email));
                sqlite_cmd.Parameters.Add(new SqlParameter("@shopId", cust.shopId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }

        public static int AddExpense(Expenses cust)
        {
            SqlCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO [dbo].[Expenses]
           ([Name]
           ,[date]
           ,[amount]
           ,[shopId]
           ,[DateSynced]
           ,[SyncId]
           ,[SyncStatus]
           ,[SyncTrackId]
           ,[UnSyncedEvents])
     VALUES
           (@Name
           ,@date
           ,@amount
           ,@shopId
           ,@DateSynced
           ,@SyncId
           ,@SyncStatus
           ,@SyncTrackId
           ,@UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SqlParameter("@Name", cust.Name));
                sqlite_cmd.Parameters.Add(new SqlParameter("@date", cust.date));
                sqlite_cmd.Parameters.Add(new SqlParameter("@amount", cust.amount));
                sqlite_cmd.Parameters.Add(new SqlParameter("@shopId", cust.shopId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }

        public static int AddShoppingCartitems(ShoppingCartItem cust)
        {
            SqlCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO [dbo].[ShoppingCartitems]
           ([quantity]
           ,[ProductId]
           ,[ShoppingCartId]
           ,[profit]
           ,[AmountSold]
           ,[DateSynced]
           ,[SyncId]
           ,[SyncStatus]
           ,[SyncTrackId]
           ,[UnSyncedEvents])
     VALUES
           (@quantity
           ,@ProductId
           ,@ShoppingCartId
           ,@profit
           ,@AmountSold
           ,@DateSynced
           ,@SyncId
           ,@SyncStatus
           ,@SyncTrackId
           ,@UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SqlParameter("@quantity", cust.quantity));
                sqlite_cmd.Parameters.Add(new SqlParameter("@ProductId", cust.ProductId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@ShoppingCartId", cust.shoppingCartItemId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@profit", cust.profit));
                sqlite_cmd.Parameters.Add(new SqlParameter("@AmountSold", cust.AmountSold));

                sqlite_cmd.Parameters.Add(new SqlParameter("@DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }

        public static int AddShoppingCart(ShoppingCart cust)
        {
            SqlCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO [dbo].[ShoppingCarts]
           ([ShoppingCartId]
           ,[salesdate]
           ,[totalQuantity]
           ,[totalPrice]
           ,[StaffId]
           ,[CustomerId]
           ,[totalProfit]
           ,[shopId]
           ,[DateSynced]
           ,[SyncId]
           ,[SyncStatus]
           ,[SyncTrackId]
           ,[UnSyncedEvents])
     VALUES
           (@ShoppingCartId
           ,@salesdate
           ,@totalQuantity
           ,@totalPrice
           ,@StaffId
           ,@CustomerId
           ,@totalProfit
           ,@shopId
           ,@DateSynced
           ,@SyncId
           ,@SyncStatus
           ,@SyncTrackId
           ,@UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SqlParameter("@ShoppingCartId", cust.ShoppingCartId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@salesdate", cust.salesdate));
                sqlite_cmd.Parameters.Add(new SqlParameter("@totalQuantity", cust.totalQuantity));
                sqlite_cmd.Parameters.Add(new SqlParameter("@totalPrice", cust.totalPrice));
                sqlite_cmd.Parameters.Add(new SqlParameter("@StaffId", cust.StaffId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@CustomerId", cust.CustomerId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@totalProfit", cust.totalProfit));
                sqlite_cmd.Parameters.Add(new SqlParameter("@shopId", cust.shopId));

                sqlite_cmd.Parameters.Add(new SqlParameter("@DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }

        public static int AddProduct(Product cust)
        {
            SqlCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO [dbo].[Products]
           ([Name]
           ,[costPrice]
           ,[discountSellingPrice]
           ,[productImageUrl]
           ,[prodCode]
           ,[quantityAvailable]
           ,[status]
           ,[lastStockDate]
           ,[productCategoryId]
           ,[productColorId]
           ,[NormalSellingPrice]
           ,[prodSize]
           ,[IsPrinted]
           ,[shopId]
           ,[salesDiscount]
           ,[productBarcodeUrl]
           ,[DateSynced]
           ,[SyncId]
           ,[SyncStatus]
           ,[SyncTrackId]
           ,[UnSyncedEvents])
     VALUES
           (@Name
           ,@costPrice
           ,@discountSellingPrice
           ,@productImageUrl
           ,@prodCode
           ,@quantityAvailable
           ,@status
           ,@lastStockDate
           ,@productCategoryId
           ,@productBrandId
           ,@productColorId
           ,@productCountryId
           ,@NormalSellingPrice
           ,@prodSize
           ,@isPrinted
           ,@shopId
           ,@salesDiscount
           ,@productBarcodeUrl
           ,@DateSynced
           ,@SyncId
           ,@SyncStatus
           ,@SyncTrackId
           ,@UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SqlParameter("@Name", cust.Name));
                sqlite_cmd.Parameters.Add(new SqlParameter("@costPrice", cust.costPrice));
                sqlite_cmd.Parameters.Add(new SqlParameter("@discountSellingPrice", cust.discountSellingPrice));
                sqlite_cmd.Parameters.Add(new SqlParameter("@productImageUrl", cust.productImageUrl));
                sqlite_cmd.Parameters.Add(new SqlParameter("@quantityAvailable", cust.quantityAvailable));
                sqlite_cmd.Parameters.Add(new SqlParameter("@status", cust.status));
                sqlite_cmd.Parameters.Add(new SqlParameter("@lastStockDate", cust.lastStockDate));
                sqlite_cmd.Parameters.Add(new SqlParameter("@productCategoryId", cust.Category));
                sqlite_cmd.Parameters.Add(new SqlParameter("@productColorId", cust.Color));
                sqlite_cmd.Parameters.Add(new SqlParameter("@NormalSellingPrice", cust.NormalSellingPrice));
                sqlite_cmd.Parameters.Add(new SqlParameter("@prodSize", cust.prodSize));
                sqlite_cmd.Parameters.Add(new SqlParameter("@isPrinted", cust.isPrinted));
                sqlite_cmd.Parameters.Add(new SqlParameter("@shopId", cust.shopId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@salesDiscount", cust.salesDiscount));
                sqlite_cmd.Parameters.Add(new SqlParameter("@productBarcodeUrl", cust.productBarcodeUrl));

                sqlite_cmd.Parameters.Add(new SqlParameter("@DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }

        public static int AddUser(staff cust)
        {
            SqlCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[UserName]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[Discriminator]
           ,[fullName]
           ,[gender]
           ,[gaurantorName]
           ,[gaurantorPhoneNumber]
           ,[address]
           ,[shopId]
           ,[DateSynced]
           ,[SyncId]
           ,[SyncStatus]
           ,[SyncTrackId]
           ,[UnSyncedEvents])
     VALUES
           (@Id
           ,@UserName
           ,@NormalizedUserName
           ,@Email
           ,@NormalizedEmail
           ,@EmailConfirmed
           ,@PasswordHash
           ,@SecurityStamp
           ,@ConcurrencyStamp
           ,@PhoneNumber
           ,@PhoneNumberConfirmed
           ,@TwoFactorEnabled
           ,@LockoutEnd
           ,@LockoutEnabled
           ,@AccessFailedCount
           ,@Discriminator
           ,@fullName
           ,@gender
           ,@gaurantorName
           ,@gaurantorPhoneNumber
           ,@address
           ,@shopId
           ,@DateSynced
           ,@SyncId
           ,@SyncStatus
           ,@SyncTrackId
           ,@UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SqlParameter("@Id", cust.Id));
                sqlite_cmd.Parameters.Add(new SqlParameter("@UserName", cust.UserName));
                sqlite_cmd.Parameters.Add(new SqlParameter("@NormalizedUserName", cust.NormalizedUserName));
                sqlite_cmd.Parameters.Add(new SqlParameter("@NormalizedEmail", cust.NormalizedEmail));
                sqlite_cmd.Parameters.Add(new SqlParameter("@EmailConfirmed", cust.EmailConfirmed));
                sqlite_cmd.Parameters.Add(new SqlParameter("@PasswordHash", cust.PasswordHash));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SecurityStamp", cust.SecurityStamp));
                sqlite_cmd.Parameters.Add(new SqlParameter("@ConcurrencyStamp", cust.ConcurrencyStamp));
                sqlite_cmd.Parameters.Add(new SqlParameter("@PhoneNumber", cust.PhoneNumber));
                sqlite_cmd.Parameters.Add(new SqlParameter("@PhoneNumberConfirmed", cust.PhoneNumberConfirmed));
                sqlite_cmd.Parameters.Add(new SqlParameter("@LockoutEnd", cust.LockoutEnd));
                sqlite_cmd.Parameters.Add(new SqlParameter("@LockoutEnabled", cust.LockoutEnabled));
                sqlite_cmd.Parameters.Add(new SqlParameter("@AccessFailedCount", cust.AccessFailedCount));
                sqlite_cmd.Parameters.Add(new SqlParameter("@Discriminator", cust.ConcurrencyStamp));
                sqlite_cmd.Parameters.Add(new SqlParameter("@fullName", cust.gender));
                sqlite_cmd.Parameters.Add(new SqlParameter("@gender", cust.shopId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@gaurantorName", cust.gaurantorName));
                sqlite_cmd.Parameters.Add(new SqlParameter("@gaurantorPhoneNumber", cust.gaurantorPhoneNumber));
                sqlite_cmd.Parameters.Add(new SqlParameter("@address", cust.address));
                sqlite_cmd.Parameters.Add(new SqlParameter("@shopId", cust.shopId));

                sqlite_cmd.Parameters.Add(new SqlParameter("@DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SqlParameter("@SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SqlParameter("@UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        } 
        #endregion

        public static int AddSyncObjectToDB(object obj,string objName)
        {
            int r = 0;
            if (objName == "staff")
            {
                staff st = (staff)obj;
                r=AddUser(st);
            }
            if (objName == "product")
            {
                Product st = (Product)obj;
                r=AddProduct(st);
            }
            if (objName == "customer")
            {
                Customer st = (Customer)obj;
                r=AddCustomer(st);
            }
            if (objName == "expenses")
            {
                Expenses st = (Expenses)obj;
                r = AddExpense(st);
            }
            if (objName == "shoppingcart")
            {
                ShoppingCart st = (ShoppingCart)obj;
                r = AddShoppingCart(st);
            }
            if (objName == "shoppingcartitem")
            {
                ShoppingCartItem st = (ShoppingCartItem)obj;
                r = AddShoppingCartitems(st);
            }
            return r;
        }

        public static int GenericOperation<T>(T tableObj, string op, string acobj="")
        {
            int res = 0;
            try
            {
                string rowId = "";
                string syncTrack = "";
                List<ColumnValue> columnValueList = new List<ColumnValue>();
                ColumnValue columnValue = new ColumnValue();

                Type t = tableObj.GetType();

                columnValueList = new List<ColumnValue>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    PropertyInfo propr = t.GetProperty(prop.Name);
                    columnValue = new ColumnValue();
                    columnValue.Column = prop.Name;
                    columnValue.Value = propr.GetValue(tableObj, null)?.ToString();
                    if (prop.Name.ToLower() != "id")
                    {
                        columnValueList.Add(columnValue);
                    }
                    if (prop.Name.ToLower() == "synctrackid")
                    {
                        syncTrack = columnValue.Value;
                    }
                }
                //string tablename = t.Name;
                //string columns = string.Join(",", columnValueList.Select(c => c.Column).ToArray());
                //string values = string.Join(",", columnValueList.Select(c => c.Value).ToArray());
                string tablename = t.Name;
                string columns = string.Join(",", columnValueList.Where(x => x.Column != "Id").Select(c => c.Column).ToArray());
                string values = string.Join(",", columnValueList.Where(x => x.Column != "Id").Select(c => "'" + c.Value + "'").ToArray());
                
                SqlCommand sqlite_cmd;


                tablename = tablename.EndsWith('s')  ? tablename : tablename + "s";
                if(tablename.Contains("SyncManager"))
                {
                    tablename = tablename.Substring(0, tablename.Length - 1);
                }
                if (op.ToLower() == "added")
                {
                    using (var cnn = SimpleDbConnection())
                    {
                        cnn.Open();
                        sqlite_cmd = cnn.CreateCommand();
                        sqlite_cmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tablename, columns, values).ToLower().Replace("shop,","").Replace("'',","");
                        res = sqlite_cmd.ExecuteNonQuery();
                        //get id of the accompanying object
                        if (!string.IsNullOrEmpty(acobj))
                        {
                            string sel = "select top(1) id from " + acobj + " order by id desc";
                            sqlite_cmd = cnn.CreateCommand();
                            sqlite_cmd.CommandText = sel;
                            res = (int)sqlite_cmd.ExecuteScalar();
                        }
                        cnn.Close();
                    }
                }
                else if (op.ToLower() == "modified")
                {
                    string updateQuery = "UPDATE " + tablename + " SET ";
                    foreach (var item in columnValueList)
                    {
                        updateQuery += item.Column + "='" + item.Value + "',";
                    }

                    updateQuery = updateQuery.TrimEnd(',');

                    updateQuery += " WHERE SyncTrackId = '" + syncTrack + "'";

                    using (var cnn = SimpleDbConnection())
                    {
                        cnn.Open();
                        sqlite_cmd = cnn.CreateCommand();
                        sqlite_cmd.CommandText = updateQuery;
                        res = sqlite_cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int ExecuteNonQuery(string dbConnString,string query,SqlParameter[] parameters)
        {
            int r = 0;
            using (var connection = new SqlConnection("" +
        new SqlConnectionStringBuilder
        {
            DataSource = dbConnString
        }))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var insertCommand = connection.CreateCommand();
                    insertCommand.Transaction = transaction;
                    insertCommand.CommandText = query;// "INSERT INTO message ( text ) VALUES ( $text )";
                    foreach (var param in parameters)
                    {
                        insertCommand.Parameters.Add(param);
                    }
                    r=insertCommand.ExecuteNonQuery();
                    transaction.Commit();
                    connection.Close();
                }
            }
            return r;
        }
    }

    public class ColumnValue
    {
        public string Column { get; set; }
        public string Value { get; set; }
    }
}
