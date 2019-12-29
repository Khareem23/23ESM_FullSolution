using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KaylaaShop.Data
{
    public class SqlLiteDAL
    {
        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=DB_Shop.db");
        }

        public int GenericOperation<T>(T tableObj, string op)
        {
            string rowId = "";
            List<ColumnValue> columnValueList = new List<ColumnValue>();
            ColumnValue columnValue = new ColumnValue();

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

            int res = 0;

            string tablename = t.Name;
            string columns = string.Join(",", columnValueList.Where(x => x.Column.ToLower() != "id").Select(c => c.Column).ToArray());
            string values = string.Join(",", columnValueList.Where(x => x.Column.ToLower() != "id").Select(c => "'" + c.Value + "'").ToArray());

            SQLiteCommand sqlite_cmd;

            if (op.ToLower() == "added")
            {
                using (var cnn = SimpleDbConnection())
                {
                    cnn.Open();
                    sqlite_cmd = cnn.CreateCommand();
                    sqlite_cmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tablename, columns, values);
                    sqlite_cmd.ExecuteNonQuery();
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
                    sqlite_cmd = cnn.CreateCommand();
                    sqlite_cmd.CommandText = updateQuery;
                    sqlite_cmd.ExecuteNonQuery();
                }
            }
        
            return res;
        }

        public static int ExecuteNonQuery(string dbConnString,string query,SqliteParameter[] parameters)
        {
            int r = 0;
            using (var connection = new SqliteConnection("" +
        new SqliteConnectionStringBuilder
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
