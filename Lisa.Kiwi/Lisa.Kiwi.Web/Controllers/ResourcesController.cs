using System;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using System.Collections.Generic;
using System.Dynamic;
using System.ComponentModel;

namespace Lisa.Kiwi.Web.Controllers
{
    public class ResourcesController : Controller
    {
        public JsonResult Dutch()
        {
            var resourcesTypes = GetAllResourceTypes();
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var resource in resourcesTypes)
            {
                IDictionary<string, object> translations = new ExpandoObject();

                foreach (var translation in resource.GetProperties())
                {
                    //if(translation. is string){
                        translations.Add(translation.Name, translation.Name);
                   // }
                }

                result.Add(resource.Name, translations);
            }

            return Json(result, JsonRequestBehavior.AllowGet);     
        }

        private Type[] GetAllResourceTypes()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => string.Equals(t.Namespace, "Resources", StringComparison.Ordinal)).ToArray();
        }
    }
}