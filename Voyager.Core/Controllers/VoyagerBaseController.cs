using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Voyager.Core.Connections;

using SqlKata;
using SqlKata.Execution;
using Voyager.Core.Connections;
using System.Data.SqlClient; // Sql Server Connection Namespace
using SqlKata.Compilers;
using Microsoft.Extensions.Configuration;
using Voyager.Core.Utility.Voyager.Database.Schema;
using Voyager.Core.Utility;
using Voyager.Core.Models;

namespace Voyager.Core.Controllers
{
    public class VoyagerBaseController : Controller
    {
        private readonly IElequentProvider db;

        public VoyagerBaseController(IElequentProvider db)
        {
            this.db = db;
        }

        //***************************************
        //               ____
        //              |  _ \
        //              | |_) |
        //              |  _ <
        //              | |_) |
        //              |____/
        //
        //      Browse our Data Type (B)READ
        //
        //****************************************
        public IActionResult Index()
        {
            IEnumerable<dynamic> dataTypeContent = null;

            // GET THE SLUG, ex. 'posts', 'pages', etc.
            //var slug = GetSlug($request);
            var slug = "roles";

            var tableName = "roles";
            var keyColumn = "id";

            // GET THE DataType based on the slug
            var dataType = db.Query("data_types").Where("slug", "=", slug).First();


            // Check permission
            //$this->authorize('browse', app($dataType->model_name));

            var getter = Convert.ToBoolean(dataType.server_side) ? "paginate" : "get";

            var search = new Dictionary<string, string>()
            {
                {"value", Request.Query["s"].ToString()},
                {"key", Request.Query["key"].ToString()},
                {"filter", Request.Query["filter"].ToString()},
            };


            var searchNames = new Dictionary<string, string>();
            if (Convert.ToBoolean(dataType.server_side))
            {
                var searchable = SchemaManager.DescribeTable(db, tableName);
                var dataRow = db.Query("data_rows").Where("data_type_id", "=", dataType.id);
                foreach (var value in searchable) {
                    var field = db.Query("data_rows").Where("field", value).First();
                    var displayName = value.Replace('_', ' ');//ucwords(str_replace('_', ' ', $value));
                    //if (field !== null) {
                    //    displayName = field->getTranslatedAttribute('display_name');
                    //}
                    searchNames.Add(value, displayName);
                }
            }

            var orderBy = !string.IsNullOrEmpty(Request.Query["order_by"]) ? Request.Query["order_by"].ToString() : dataType.order_column;
            var sortOrder = !string.IsNullOrEmpty(Request.Query["sort_order"]) ? Request.Query["sort_order"].ToString() : dataType.order_direction;
            var usesSoftDeletes = false;
            var showSoftDeleted = false;

            // Next Get or Paginate the actual content from the MODEL that corresponds to the slug DataType
            if (Convert.ToString(dataType.model_name).Length != 0)
            {
                IVoyagerModel model = ObjectInstance.GetGenericInstanceOfDifferentAssembly<IVoyagerModel>(dataType.model_name);
                var table = model["TableName"].ToString();

                var query = db.Query(tableName);

                //if (model.checkFunc("LocalScope"))
                //{
                //    query = model.func("LocalScope", new object[] { query }) as Query;
                //}

                //                if ($dataType->scope && $dataType->scope != '' && method_exists($model, 'scope'.ucfirst($dataType->scope))) {
                //                $query = $model->{$dataType->scope} ();
                //                } else
                //                {
                                query = query.Select($"{table}.*");
                //                }

                //                // Use withTrashed() if model uses SoftDeletes and if toggle is selected
                //                if ($model && in_array(SoftDeletes::class, class_uses_recursive($model)) && Auth::user()->can('delete', app($dataType->model_name))) {
                //                $usesSoftDeletes = true;

                //                if ($request->get('showSoftDeleted')) {
                //                    $showSoftDeleted = true;
                //                    $query = $query->withTrashed();
                //    }
                //}

                //            // If a column has a relationship associated with it, we do not want to show that field
                //            $this->removeRelationshipField($dataType, 'browse');
                string search_filter = string.Empty;
                string search_value = string.Empty;

                if (!string.IsNullOrEmpty(search["value"]) && !string.IsNullOrEmpty(search["key"]) && !string.IsNullOrEmpty(search["filter"]))
                {
                    search_filter = (search["filter"] == "equals") ? "=" : "LIKE";
                    search_value = (search["filter"] == "equals") ? search["value"] : $"%{search["value"]}%";
                    query = query.Where(search["key"], search_filter, search_value);
                }

                if (!string.IsNullOrEmpty(orderBy)) {
                //if (string.IsNullOrEmpty(orderBy) && in_array($orderBy, $dataType->fields())) {
                    var querySortOrder = (!string.IsNullOrEmpty(sortOrder)) ? sortOrder : "desc";
                    dataTypeContent = query.OrderBy(orderBy, querySortOrder);
                                //$dataTypeContent = call_user_func([
                                //    $query->orderBy($orderBy, $querySortOrder),
                                //    $getter,
                                //]);
                }
                //elseif($model->timestamps) {
                //                $dataTypeContent = call_user_func([$query->latest($model::CREATED_AT), $getter]);
                //} 
                else
                {
                    dataTypeContent = (IEnumerable<dynamic>) query.OrderByDesc(keyColumn).Get();
                    return Ok(dataTypeContent);
                }

                //            // Replace relationships' keys for labels and create READ links if a slug is provided.
                //            $dataTypeContent = $this->resolveRelations($dataTypeContent, $dataType);
                //        } else
                //{
                //            // If Model doesn't exist, get data from table name
                //            $dataTypeContent = call_user_func([DB::table($dataType->name), $getter]);
                //            $model = false;
            }

            
            return View();
        }

        
    }
}
