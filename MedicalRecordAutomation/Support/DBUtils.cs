using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AutomationWrapper.Utilities
{
    public class DBUtils
    {
        private static readonly string connectionString;

        // Static constructor to load configuration once
        static DBUtils()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // or Directory.GetCurrentDirectory()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            connectionString = config.GetConnectionString("omrdb");
        }

        public static string GetFirstCellValue(string query)
        {
            using var connection = new SqlConnection(connectionString); // connection the connection to mssql 

            using var command = new SqlCommand(query, connection); // build the query 
            connection.Open();
            return Convert.ToString(command.ExecuteScalar());
        }

        public static int UpdateDeleteInsertQuery(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString); // connection details
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int noOfRowsAffected = command.ExecuteNonQuery();
            connection.Close();

            return noOfRowsAffected;
        }

        public static DataTable SelectQuery(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString); // connection details
            SqlCommand command = new SqlCommand(query, connection);

            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            return dt;
        }

    }
}
