using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlKata;
using SqlKata.Execution;
using Voyager.Core.Connections;
using Voyager.Core.Models;
using Voyager.Core.Utility;

namespace Voyager.Core.Controllers
{
    public class RolesController : Controller
    {
        private readonly IElequentProvider db;

        public RolesController(IElequentProvider db)
        {
            this.db = db;
        }

        // GET: RolesController
        public ActionResult Index()
        {
            IVoyagerModel model = ObjectInstance.GetGenericInstanceOfDifferentAssembly<IVoyagerModel>("Role");
            var tableName = model["TableName"].ToString();

            var query = db.Query(tableName);

            if (model.checkFunc("LocalScope"))
            {
                query = model.func("LocalScope", new object[] { query }) as Query;
            }

            var result = query.Get();


            // Bread Browse column list
            var identityColumn = "Id";
            var browseColumnList = new string[] { "Name", "DisplayName" };
            browseColumnList = browseColumnList.Concat(new[] { identityColumn }).ToArray();


            var listColumns = new List<string>();
            var columns = db.Instance().Select($"SELECT name FROM syscolumns WHERE id=OBJECT_ID('{tableName}')").ToArray();
            foreach(var column in columns)
            {
                listColumns.Add(column.name);
            }

            return View();
        }

        // GET: RolesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RolesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RolesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
