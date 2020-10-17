using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Voyager.Core.Models
{
    public class Role : VoyagerModel
    {
        public string TableName => "Roles";

        


        public Query LocalScope(Query query)
        {
            //query = query.Where($"{TableName}.Email", "!=", "admin@gmail.com");
            return query;
        }
    }
}
