using Socha3.Common.AppEnvironment;
using Socha3.Common.Reflection;
using Socha3.Common.Spaminator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace  Socha3.MemeBox2000
{
    public class MemeBox2000App : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AppEnv.SetExecutingAssembly(typeof(MemeBox2000App));
            Application["AssemblyDetails"] = AppEnv.GetAssemblyDetails();
        }

        protected void Application_Error()
        {
            if (!AppEnv.AppSettings.ShowErrors)
            {
                var exception = Server.GetLastError();
                Session["LastError"] = exception;
                EmailUtil.Gmail("mike@socha3.com", "mike@socha3.com", "", "Error at socha3.com", JsonUtil.Serialize(exception));
                Response.Redirect("~/Home/Error");
            }
        }
    }
}