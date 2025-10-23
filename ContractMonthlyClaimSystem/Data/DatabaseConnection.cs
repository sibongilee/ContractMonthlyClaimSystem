using System;
using System.Data.SqlClient;
namespace ContractMonthlyClaimSystem.Data
{
    public class DatabaseConnection
    {
        // connection string to connect to the SQL Server database
        private readonly string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContractMonthlyClaimDB;Integrated Security=True;";
        // method to get a sql connection instance
        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
  
