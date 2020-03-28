﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoLotDAL.BulkImport
{
    class ProcessBulkImport
    {
        private static SqlConnection _sqlConnection = null;
        private static readonly string _connectionString = @"Data Source = (localdb)\mssqllocaldb;Integrated Security=true;Initial Catalog=Autolot";

        private static void OpenConnection()
        {
            _sqlConnection = new SqlConnection { ConnectionString = _connectionString };
            _sqlConnection.Open();
        }
        private static void CloseConnection()
        {
            if (_sqlConnection?.State != ConnectionState.Closed)
            {
                _sqlConnection?.Close();
            }
        }
        private static void ExecuteBulkImport<T>(IEnumerable<T> records, string tableName)
        {
            OpenConnection();
            using (SqlConnection conn = _sqlConnection)
            {
                SqlBulkCopy bc = new SqlBulkCopy(conn)
                {
                    DestinationTableName = tableName
                };
                var dataReader = new MyDataReader<T>(records.ToList());
                try
                {
                    bc.WriteToServer(dataReader);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

    }
}
