using System;
using System.Data.SqlClient;
namespace ContractMonthlyClaimSystem.Data
{
    public class DatabaseConnection
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContractMonthlyClaimDB;Integrated Security=True;";
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
