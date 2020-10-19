using SqlKata;
using SqlKata.Execution;
using Voyager.Core.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voyager.Core.Utility.Voyager.Database.Schema
{
    public class SchemaManager
    {
        public static List<string> DescribeTable(IElequentProvider db, string tableName)
        {
            List<string> listColumns = new List<string>();
            var columns = db.Instance().Select($"SELECT name FROM syscolumns WHERE id=OBJECT_ID('{tableName}')").ToArray();
            foreach (var column in columns)
            {
                listColumns.Add(column.name);
            }

            return listColumns;
        }
    }
}
