using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voyager.Core.Connections
{
    public interface IElequentProvider
    {
        Query Query(string sql = null);
        QueryFactory Instance();
    }
}
