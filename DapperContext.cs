using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebAPI
{
    public class DapperContext : IDapper
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionStrings:dbConnection").Value;
        }
        public async Task<T> ExecuteGetAllAsync<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (dbConnection.State == ConnectionState.Closed)
                    {
                        dbConnection.Open();
                        //Log.Information("DbConnection Established");
                    }
                    return (await dbConnection.ExecuteScalarAsync<T>(query, sp_params, commandType: commandType));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<int>> QueryAsync<T>(string spname, DynamicParameters param)
        {
            List<int> ids = new List<int>();
            using (var connection = new SqlConnection(_connectionString))
            {
                ids = connection.Query<int>(spname, param, commandType: CommandType.StoredProcedure).ToList();
            }
            return ids;
        }
    }
}
