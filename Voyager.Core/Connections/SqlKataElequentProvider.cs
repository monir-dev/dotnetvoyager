using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SqlKata;
using SqlKata.Execution;
using System.Data.SqlClient; // Sql Server Connection Namespace
using SqlKata.Compilers;
using Microsoft.Extensions.Configuration;

namespace Voyager.Core.Connections
{
    public class SqlKataElequentProvider: IElequentProvider
    {
        private readonly IConfiguration _configuration;
        protected QueryFactory db;

        public SqlKataElequentProvider(IConfiguration configuration)
        {
            _configuration = configuration;

            //https://www.connectionstrings.com/store-and-read-connection-string-in-appsettings-json/
            string connectionString = _configuration.GetConnectionString("SqlKata");
            var connection = new SqlConnection(connectionString);
            var compiler = new SqlServerCompiler();

            db = new QueryFactory(connection, compiler);
        }


        public Query Query(string sql = null)
        {
            return string.IsNullOrEmpty(sql) ? db.Query() : db.Query(sql);
        }

        public QueryFactory Instance()
        {
            if (db == null) Connect();

            return db;
        }

        private void Connect()
        {
            //https://www.connectionstrings.com/store-and-read-connection-string-in-appsettings-json/
            var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var compiler = new SqlServerCompiler();

            db = new QueryFactory(connection, compiler);
        }
    }
}
