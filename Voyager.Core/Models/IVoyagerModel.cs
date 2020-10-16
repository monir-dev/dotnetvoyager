using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voyager.Core.Models
{
    public interface IVoyagerModel
    {
        object this[string propertyName] { get; }

        object func(string functionName, object[] args = null);
    }
}
