using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using SqlKata.Execution;
using Voyager.Core.Connections;
using Voyager.Core.Models;
using Voyager.Core.Utility;
using Microsoft.CSharp;
using System.Collections;
using System.Reflection;
using SqlKata;
using SqlKata.Extensions;

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
            
            var objectType = ObjectInstance.GetClassType<IVoyagerModel>("User");

            var instantiatedObject = Activator.CreateInstance(objectType) as User;
            // get a property value
            var tableName = instantiatedObject.TableName;

            IVoyagerModel model = (IVoyagerModel) ObjectInstance.GetObjectInstanceOfDifferentAssembly(objectType.AssemblyQualifiedName);

            var typer = ObjectInstance.GetClassType<IVoyagerModel>("User");

            Dictionary<string, IVoyagerModel> classes = new Dictionary<string, IVoyagerModel>();
            classes.Add("User", new User());


            var res = model.func("testfunction");
            var addition = model.func("add", new object[] { 4, 6 });


            //var test = model.GetType().GetProperty("tableName").GetValue(model, null);


            // Adding Query Scope Start

            var query = _db.Query("AspNetUsers");

            if(model.checkFunc("LocalScope"))
            {
                query = model.func("LocalScope", new object[] { query }) as Query;
            }

            var result = query.Get();

            // Adding Query Scope End


            // Query
            var users = _db.Query("AspNetUsers").Get();

            return Ok(users);
            return View();
        }

        public List<string> GetAllEntities()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(IVoyagerModel).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .Select(x => x.Name).ToList();
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
