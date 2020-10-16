using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlKata.Execution;
using Voyager.Core.Connections;
using Voyager.Core.Models;
using Voyager.Core.Utility;

namespace Voyager.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IElequentProvider _db;

        public HomeController(ILogger<HomeController> logger, IElequentProvider _db)
        {
            _logger = logger;
            this._db = _db;
        }

        public IActionResult Index()
        {

            // Get Class Instance
            //https://jeremylindsayni.wordpress.com/2019/02/11/instantiating-a-c-object-from-a-string-using-activator-createinstance-in-net/
            const string objectToInstantiate = "Voyager.Core.Models.User, Voyager.Core";

            var objectType = Type.GetType(objectToInstantiate);

            var instantiatedObject = Activator.CreateInstance(objectType) as User;
            // get a property value
            var tableName = instantiatedObject.tableName;


            //var model = ObjectInstance.GetGenericInstanceOfDifferentAssembly<IVoyagerModel>("Voyager.Core.Models.User, Voyager.Core");
            var model = ObjectInstance.GetVoyagerModelInstanceOfDifferentAssembly("Voyager.Core.Models.User, Voyager.Core");

            // Query
            var users = _db.Query("AspNetUsers").Get();

            return Ok(users);
            return View();
        }
        public IVoyagerModel GetInstance(string FullyQualifiedNameOfClass)
        {
            Type type = Type.GetType(FullyQualifiedNameOfClass);
            if (type != null)
                return (IVoyagerModel)Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(FullyQualifiedNameOfClass);
                if (type != null)
                    return (IVoyagerModel)Activator.CreateInstance(type);
            }
            return null;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
