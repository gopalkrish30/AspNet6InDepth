using System.Data;
using System.Data.SqlClient;

namespace Api
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionstring;

        public SqlConnectionFactory(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public IDbConnection Connect()
        {
            return new SqlConnection(_connectionstring);
        }
    }
}
