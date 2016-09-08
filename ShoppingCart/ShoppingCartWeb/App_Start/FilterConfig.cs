using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Serialization;

namespace ShoppingCartWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
