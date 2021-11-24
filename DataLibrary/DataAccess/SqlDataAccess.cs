using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "FDEC")
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();
            return configuration.GetSection("ConnectionStrings").GetSection(connectionName).Value;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static int InsertData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static T FindData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.QueryFirstOrDefault<T>(sql, data);
            }
        }
        public static DataTable GetDataBySQL(string sql, params SqlParameter[] Parameters)
        {
            using (SqlConnection cnn = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, cnn);
                command.Parameters.AddRange(Parameters);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                command.Connection.Close();
                return dt;
            }
        }
        public static int ExecuteSQL(string sql, params SqlParameter[] Parameters)
        {
            using (SqlConnection cnn = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, cnn);
                command.Parameters.AddRange(Parameters);
                command.Connection.Open();
                int count = command.ExecuteNonQuery();
                command.Connection.Close();
                return count;
            }

        }
    }
}