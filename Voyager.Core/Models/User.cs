using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Voyager.Core.Models
{
    public class User : VoyagerModel
    {
        public string TableName => "AspNetUsers";
        
        public string testfunction()
        {
            return "Monir";
        }

        public int add(int x, int y) => x + y;
    }
}
