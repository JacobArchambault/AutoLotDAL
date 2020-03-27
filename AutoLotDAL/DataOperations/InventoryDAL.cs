using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AutoLotDAL.DataOperations
{
    public class InventoryDAL
    {
        private readonly string _connectionString;
        public InventoryDAL() : this(@"Data Source = (localdb)\mssqllocaldb;IntegratedSecurity=true;Initial Catalog=AutoLot")
        { }
        public InventoryDAL(string connectionString) => _connectionString = connectionString;
    }
}
