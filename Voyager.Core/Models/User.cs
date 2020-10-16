using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voyager.Core.Models
{
    public class User : IVoyagerModel
    {
        public string tableName = "AspNetUsers";
        public object this[string propertyName] => GetType().GetProperty(propertyName)?.GetValue(this, null);
    }
}
