
using Socha3.Common.Reflection;

namespace Socha3.Common.AppEnvironment
{
    public class Configurations : PublicConstantsClassBase<Configurations, string>
    {
        public static string Debug { get; } = "Debug";
        public static string Local { get; } = "Local";
        public static string Dev { get; } = "Dev";
        public static string Release { get; } = "Release";
        public static string localhost { get; } = "localhost";
    }
}