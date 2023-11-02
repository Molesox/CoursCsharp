using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Todapp.Data.Context
{
    public class SqlContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SqlContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public SqlContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection() => new SqlConnection(_connectionString);
    }

}