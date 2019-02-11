using Microsoft.AspNetCore.Http;
using Socha3.Common.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Socha3.Common.AppEnvironment
{
    /// <summary>
    /// Properties about the app's running environment.
    /// </summary>
    public static class AppEnv
    {
        private static bool executingAssemblySet = false;
        /// <summary>
        /// Set the Executing Assembly only once on app start for app environment information.
        /// </summary>
        /// <param name="typeFromExecutingAssembly"></param>
        public static void SetExecutingAssembly(Type typeFromExecutingAssembly)
        {
            if (executingAssemblySet)
                throw new InvalidOperationException($"Executing assembly was already set.");

            ExecutingAssembly = Assembly.GetAssembly(typeFromExecutingAssembly);
            executingAssemblySet = true;
        }
        public static Assembly ExecutingAssembly { get; private set; }

        /// <summary>
        /// The Windows Principal Identity the app's process is running under.
        /// </summary>
        public static string Identity
        {
            get
            {
                var appIdentity = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                return appIdentity;
            }
        }

        /// <summary>
        /// Attempts deserialization of the json string in the AssemblyInformationalVersion to an "AssemblyInfo" object. If type is not set, the AppEnv.ExecutingAssembly will be used.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AssemblyDetails GetAssemblyDetails(Type type = null)
        {
            var info = AssemblyDetails.GetDetails(type);
            return info;
        }

        /// <summary>
        /// Gets the version info from the assembly in which the given type is defined.  If type is not set, the AppEnv.ExecutingAssembly will be used.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Version GetAssemblyVersion(Type type = null)
        {
            Assembly assembly;
            if (type == null)
                assembly = ExecutingAssembly;
            else
                assembly = Assembly.GetAssembly(type);
            var version = assembly.GetName().Version;

            return version;
        }

        /// <summary>
        /// Gets the assembly for the given type and returns its AssemblyVersion custom attribute.  If type is null, AppEnv.ExecutingAssembly will be used.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AssemblyInformationalVersionAttribute GetAssemblyInformationalVersion(Type type = null)
        {
            Assembly assembly;
            if (type == null)
                assembly = ExecutingAssembly;
            else
                assembly = Assembly.GetAssembly(type);

            var info = (AssemblyInformationalVersionAttribute)assembly.GetCustomAttributes(false).ToList().FirstOrDefault(attr => attr.GetType() == typeof(AssemblyInformationalVersionAttribute));
            return info;
        }
    }
}