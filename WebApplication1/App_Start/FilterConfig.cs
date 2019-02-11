using System.Web;
using System.Web.Mvc;

namespace  Socha3.MemeBox2000
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}