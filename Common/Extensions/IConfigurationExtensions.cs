using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Socha3.Common.Extensions
{
    public static class IConfigurationExtensions
    {
        /// <summary>
        /// Boolean representation of the ShowErrors AppSetting
        /// </summary>
        public static bool ShowErrors(this IConfiguration configuration)
        {
            var setting = configuration.GetValue<string>("AppSettings");
            return Convert.ToBoolean(setting);
        }

        /// <summary>
        /// The AppName AppSetting
        /// </summary>
        public static string AppName(this IConfiguration configuration)
        {
            var setting = configuration.GetValue<string>(nameof(AppName));
            return setting;
        }

        /// <summary>
        /// The ConnectionStringName AppSetting
        /// </summary>
        public static string ConnectionStringName(this IConfiguration configuration)
        {
            var setting = configuration.GetValue<string>(nameof(ConnectionStringName));
            return setting;
        }
    }
}