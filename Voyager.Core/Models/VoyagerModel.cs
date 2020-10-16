using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voyager.Core.Models
{
    public class VoyagerModel: IVoyagerModel
    {
        public object this[string propertyName] => GetType().GetProperty(propertyName)?.GetValue(this, null);

        public object func(string functionName, object[] args = null) => GetType().GetMethod(functionName)?.Invoke(this, args ?? new object[] { });

        public bool checkFunc(string functionName) => GetType().GetMethod(functionName) != null;
    }
}
