using Dapper;
using SyncMan.Core;
using SyncMan.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SyncMan
{
    public class DataAccess
    {
        public static string DbFile
        {
            //path to the sqllite  db on the local customers system
            get {
                return @"C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Shop Version\KaylaaShop\DB_Shop.db"; 
            }
          
        }

        public SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public List<SyncManager> GetUnsyncedData()
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                List<SyncManager> result = cnn.Query<SyncManager>(
                    @"SELECT Id, SyncManagerId, SyncTrackId, ShopId, SourceDataStore, SourceDataStoreType, DestinationDataStore, DestinationDataStoreType, Entity,
                           State, Action, SyncExecutionStatus, DateLogged, DateApplied
            FROM SyncManager
            WHERE SyncExecutionStatus=0 and SourceDataStore='Local'", null).ToList();
                cnn.Close();
                return result;
            }
        }


        public void MarkAsSynced(string syncTrackId)
        {
            SQLiteCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = "UPDATE SyncManager SET SyncExecutionStatus = 1 WHERE SyncManagerId = '" + syncTrackId + "'";
                int c=sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

       

        public void UpdateCustomer(Customer cust)
        {
            SQLiteCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = "UPDATE Customers SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=$trackid,UnSyncedEvents=1 WHERE id=$id";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$id", cust.Id));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public void UpdateExpense(Expenses cust)
        {
            SQLiteCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"UPDATE Expenses SET DateSynced=getdate(),SyncStatus=0,
SyncTrackId=$trackid,UnSyncedEvents=1 WHERE id=$id";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$id", cust.id));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
        //ShoppingCartitems
        public void UpdateShoppingCartitems(ShoppingCartItem cust)
        {
            SQLiteCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"UPDATE ShoppingCartitems SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=$trackid,UnSyncedEvents=1 " +
                    "WHERE id=$id";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$id", cust.Id));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public void UpdateShoppingCart(ShoppingCart cust)
        {
            SQLiteCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"UPDATE ShoppingCarts SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=$trackid,UnSyncedEvents=1 " +
                    "WHERE ShoppingCartId=$id";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$id", cust.Id));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public void UpdateProduct(Product cust)
        {
            SQLiteCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"UPDATE Product SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=$trackid,UnSyncedEvents=1 " +
                    "WHERE id=$id";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$id", cust.Id));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public void UpdateStaff(staff cust)
        {
            SQLiteCommand sqlite_cmd;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"UPDATE staff SET DateSynced=getdate(),SyncStatus=0,SyncTrackId=$trackid,UnSyncedEvents=1 " +
                    "WHERE id=$id";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$trackid", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$id", cust.Id));
                sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }


        #region ADDSYNCDATATOTABLES
        public int AddCustomer(Customer cust)
        {
            SQLiteCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO Customers
           (Name
           ,gender
           ,address
           ,phoneNumber
           ,email
           ,shopId
           ,DateSynced
           ,SyncId
           ,SyncStatus
           ,SyncTrackId
           ,UnSyncedEvents)
     VALUES
           ($Name
           ,$gender
           ,$address
           ,$phoneNumber
           ,$email
           ,$shopId
           ,$DateSynced
           ,$SyncId
           ,$SyncStatus
           ,$SyncTrackId
           ,$UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$Name", cust.Name));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$gender", cust.gender));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$address", cust.address));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$phonenumber", cust.phoneNumber));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$email", cust.email));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$shopId", cust.shopId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }

        public int AddExpense(Expenses cust)
        {
            SQLiteCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO Expenses
           (Name
           ,date
           ,amount
           ,shopId
           ,DateSynced
           ,SyncId
           ,SyncStatus
           ,SyncTrackId
           ,UnSyncedEvents)
     VALUES
           ($Name
           ,$date
           ,$amount
           ,$shopId
           ,$DateSynced
           ,$SyncId
           ,$SyncStatus
           ,$SyncTrackId
           ,$UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$Name", cust.name));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$date", cust.date));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$amount", cust.amount));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$shopId", cust.shopid));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }

        public int AddShoppingCartitems(ShoppingCartItem cust)
        {
            SQLiteCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO ShoppingCartitems
           (quantity
           ,ProductId
           ,ShoppingCartId
           ,profit
           ,AmountSold
           ,DateSynced
           ,SyncId
           ,SyncStatus
           ,SyncTrackId
           ,UnSyncedEvents)
     VALUES
           ($quantity
           ,$ProductId
           ,$ShoppingCartId
           ,$profit
           ,$AmountSold
           ,$DateSynced
           ,$SyncId
           ,$SyncStatus
           ,$SyncTrackId
           ,$UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$quantity", cust.quantity));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$ProductId", cust.ProductId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$ShoppingCartId", cust.ShoppingCartId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$profit", cust.profit));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$AmountSold", cust.AmountSold));

                sqlite_cmd.Parameters.Add(new SQLiteParameter("$DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }

        public int AddShoppingCart(ShoppingCart cust)
        {
            SQLiteCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO ShoppingCarts
           (Id
           ,salesdate
           ,totalQuantity
           ,totalPrice
           ,StaffId
           ,CustomerId
           ,totalProfit
           ,shopId
           ,DateSynced
           ,SyncId
           ,SyncStatus
           ,SyncTrackId
           ,UnSyncedEvents)
     VALUES
           ($ShoppingCartId
           ,$salesdate
           ,$totalQuantity
           ,$totalPrice
           ,$StaffId
           ,$CustomerId
           ,$totalProfit
           ,$shopId
           ,$DateSynced
           ,$SyncId
           ,$SyncStatus
           ,$SyncTrackId
           ,$UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$ShoppingCartId", cust.Id));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$salesdate", cust.salesdate));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$totalQuantity", cust.totalQuantity));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$totalPrice", cust.totalPrice));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$StaffId", cust.StaffId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$CustomerId", cust.CustomerId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$totalProfit", cust.totalProfit));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$shopId", cust.shopId));

                sqlite_cmd.Parameters.Add(new SQLiteParameter("$DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }

        public int AddProduct(Product cust)
        {
            SQLiteCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO Products
           (Name
           ,costPrice
           ,discountSellingPrice
           ,productImageUrl
           ,prodCode
           ,quantityAvailable
           ,status
           ,lastStockDate
           ,Category
           ,Color
           ,NormalSellingPrice
           ,prodSize
           ,isPrinted
           ,shopId
           ,salesDiscount
           ,productBarcodeUrl
           ,DateSynced
           ,SyncId
           ,SyncStatus
           ,SyncTrackId
           ,UnSyncedEvents)
     VALUES
           ($Name
           ,$costPrice
           ,$discountSellingPrice
           ,$productImageUrl
           ,$prodCode
           ,$quantityAvailable
           ,$status
           ,$lastStockDate
           ,$Category
           ,$Color
           ,$NormalSellingPrice
           ,$prodSize
           ,isPrinted
           ,$shopId
           ,$salesDiscount
           ,$productBarcodeUrl
           ,$DateSynced
           ,$SyncId
           ,$SyncStatus
           ,$SyncTrackId
           ,$UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$Name", cust.Name));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$costPrice", cust.costPrice));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$discountSellingPrice", cust.discountSellingPrice));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$productImageUrl", cust.productImageUrl));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$quantityAvailable", cust.quantityAvailable));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$status", cust.status));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$lastStockDate", cust.lastStockDate));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$Category", cust.Category));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$productColorId", cust.Color));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$NormalSellingPrice", cust.NormalSellingPrice));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$prodSize", cust.prodSize));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$isPrinted", cust.isPrinted));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$shopId", cust.shopId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$salesDiscount", cust.salesDiscount));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$productBarcodeUrl", cust.productBarcodeUrl));

                sqlite_cmd.Parameters.Add(new SQLiteParameter("$DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }

        public int AddUser(staff cust)
        {
            SQLiteCommand sqlite_cmd;
            int c = 0;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                sqlite_cmd = cnn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO AspNetUsers
           (Id
           ,UserName
           ,NormalizedUserName
           ,Email
           ,NormalizedEmail
           ,EmailConfirmed
           ,PasswordHash
           ,SecurityStamp
           ,ConcurrencyStamp
           ,PhoneNumber
           ,PhoneNumberConfirmed
           ,TwoFactorEnabled
           ,LockoutEnd
           ,LockoutEnabled
           ,AccessFailedCount
           ,Discriminator
           ,fullName
           ,gender
           ,gaurantorName
           ,gaurantorPhoneNumber
           ,address
           ,shopId
           ,DateSynced
           ,SyncId
           ,SyncStatus
           ,SyncTrackId
           ,UnSyncedEvents)
     VALUES
           ($Id
           ,$UserName
           ,$NormalizedUserName
           ,$Email
           ,$NormalizedEmail
           ,$EmailConfirmed
           ,$PasswordHash
           ,$SecurityStamp
           ,$ConcurrencyStamp
           ,$PhoneNumber
           ,$PhoneNumberConfirmed
           ,$TwoFactorEnabled
           ,$LockoutEnd
           ,$LockoutEnabled
           ,$AccessFailedCount
           ,$Discriminator
           ,$fullName
           ,$gender
           ,$gaurantorName
           ,$gaurantorPhoneNumber
           ,$address
           ,$shopId
           ,$DateSynced
           ,$SyncId
           ,$SyncStatus
           ,$SyncTrackId
           ,$UnSyncedEvents)";
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$Id", cust.Id));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$UserName", cust.UserName));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$NormalizedUserName", cust.NormalizedUserName));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$NormalizedEmail", cust.NormalizedEmail));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$EmailConfirmed", cust.EmailConfirmed));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$PasswordHash", cust.PasswordHash));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SecurityStamp", cust.SecurityStamp));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$ConcurrencyStamp", cust.ConcurrencyStamp));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$PhoneNumber", cust.PhoneNumber));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$PhoneNumberConfirmed", cust.PhoneNumberConfirmed));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$LockoutEnd", cust.LockoutEnd));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$LockoutEnabled", cust.LockoutEnabled));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$AccessFailedCount", cust.AccessFailedCount));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$Discriminator", cust.ConcurrencyStamp));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$fullName", cust.gender));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$gender", cust.shopId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$gaurantorName", cust.gaurantorName));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$gaurantorPhoneNumber", cust.gaurantorPhoneNumber));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$address", cust.address));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$shopId", cust.shopId));

                sqlite_cmd.Parameters.Add(new SQLiteParameter("$DateSynced", cust.DateSynced));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncId", cust.SyncId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncStatus", cust.SyncStatus));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$SyncTrackId", cust.SyncTrackId));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("$UnSyncedEvents", cust.UnSyncedEvents));
                c = sqlite_cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return c;
        }
        #endregion


        public int AddSyncObjectToDB(object obj, string objName)
        {
            int r = 0;
            if (objName == "staff")
            {
                staff st = (staff)obj;
                r = AddUser(st);
            }
            if (objName == "product")
            {
                Product st = (Product)obj;
                r = AddProduct(st);
            }
            if (objName == "customer")
            {
                Customer st = (Customer)obj;
                r = AddCustomer(st);
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

        public int GenericOperation<T>(T tableObj, string op)
        {
            string rowId = "";
            List<ColumnValue> columnValueList = new List<ColumnValue>();
            ColumnValue columnValue = new ColumnValue();
            int c = 0;
            Type t = tableObj.GetType();

            columnValueList = new List<ColumnValue>();
            foreach (var prop in typeof(T).GetProperties())
            {
                PropertyInfo propr = t.GetProperty(prop.Name);

                columnValue = new ColumnValue();
                columnValue.Column = prop.Name;
                columnValue.Value = propr.GetValue(tableObj, null).ToString();
                columnValueList.Add(columnValue);

                if (prop.Name.ToLower() == "id")
                {
                    rowId = columnValue.Value;
                }
            }

            string tablename = t.Name;
            //string columns = string.Join(",", columnValueList.Select(c => c.Column).ToArray());
            //string values = string.Join(",", columnValueList.Select(c => c.Value).ToArray());
            string columns = string.Join(",", columnValueList.Where(x => x.Column != "Id").Select(c => c.Column).ToArray());
            string values = string.Join(",", columnValueList.Where(x => x.Column != "Id").Select(c => "'" + c.Value + "'").ToArray());

            SQLiteCommand sqlite_cmd;
            try
            {
                if (op.ToLower() == "added")
                {
                    using (var cnn = SimpleDbConnection())
                    {
                        cnn.Open();
                        sqlite_cmd = cnn.CreateCommand();
                        sqlite_cmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tablename, columns, values);
                        c = sqlite_cmd.ExecuteNonQuery();
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

                    updateQuery += " WHERE Id = '" + rowId + "'";

                    using (var cnn = SimpleDbConnection())
                    {
                        cnn.Open();
                        sqlite_cmd = cnn.CreateCommand();
                        sqlite_cmd.CommandText = updateQuery;
                        c = sqlite_cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                c = -1;
            }
            return c;
        }

        


    }
}
