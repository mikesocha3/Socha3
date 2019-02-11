using System;

namespace Socha3.Common.Reflection
{
    /// <summary>
    /// Decorate a method with this attribute to enable its ID to be output in the stack strace when using ReflectionUtil.GetStackTrace()
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodIdAttribute : Attribute
    {
        public string Id { get; set; }

        public MethodIdAttribute(string guid)
        {
            Id = guid;
        }
    }
}
