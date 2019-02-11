using Socha3.Common.Reflection;
using System;

namespace Socha3.Common.AppEnvironment
{
    public class AssemblyDetails
    {
        public string GitCommit { get; set; }
        public DateTime BuiltOn { get; set; }
        public string Configuration { get; set; }

        /// <summary>
        /// If type is null, AppEnv.ExecutingAssembly will be used.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AssemblyDetails GetDetails(Type type = null)
        {
            var infoObj = AppEnv.GetAssemblyInformationalVersion(type);
            var details = JsonUtil.Deserialize<AssemblyDetails>(infoObj.InformationalVersion);

            return details;
        }
    }
}