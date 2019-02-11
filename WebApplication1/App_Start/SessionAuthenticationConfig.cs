using System;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof( Socha3.MemeBox2000.App_Start.SessionAuthenticationConfig), "PreAppStart")]

namespace  Socha3.MemeBox2000.App_Start
{
    public static class SessionAuthenticationConfig
    {
        public static void PreAppStart()
        {
            DynamicModuleUtility.RegisterModule(typeof(System.IdentityModel.Services.SessionAuthenticationModule));
        }
    }
}