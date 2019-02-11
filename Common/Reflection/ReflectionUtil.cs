using Socha3.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using SysIO = System.IO;

namespace Socha3.Common.Reflection
{
    public static class ReflectionUtil
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MethodBase GetCurrentMethod()
        {
            var sf = new StackFrame(1);

            return sf.GetMethod();
        }        

        /// <summary>
        /// Gets a method previously called in this current stack trace.<para/>
        /// Use the [MethodImpl(MethodImplOptions.NoInlining)] Attribute to ensure that a method<para/>
        /// previously called in your stack doesn't get optimized out into a line.<para/>
        /// (Do this to make sure a certain method is included as part of the call stack.)
        /// </summary>
        /// <param name="offset">
        /// By default, '0' will be used and the method calling the currently executing<para/>
        /// method will be returned.  Increment to 1 and so on to step frame by frame up the stack.
        /// </param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MethodBase GetPreviousMethod(int offset = 0)
        {
            if(offset < 0)
                throw new ArgumentOutOfRangeException("offset cannot be < 0");

            var sf = new StackFrame(2 + offset);

            return sf.GetMethod();
        }

        public static dynamic CreateListOf(Type objType)
        {
            var listType = typeof(List<>).GetType().MakeGenericType(objType);
            dynamic obj = Activator.CreateInstance(listType);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeExcludeFilter">Truncate the stackframe output before the point at which the method in the stack frame's declaring type equals the excludeFilterType.<para/>
        /// Used commonly with loggers.  If you don't want to include stack frames whose methods' declaring types are "Logger", for example.</param>
        /// <returns></returns>
        public static string GetStackTrace(Type excludeFilterType = null, bool showParameterValues = false)
        {
            var stackTrace = new StackTrace(true); // the true value is used to include source file info            
            
            var trace = new StringBuilder();
            for(int i = 1; i < stackTrace.FrameCount; i++)//start at i = 1 to exclude GetStackTrace() from trace
            {
                var frame = stackTrace.GetFrame(i);
                if (excludeFilterType == null || (excludeFilterType != frame.GetMethod().DeclaringType))
                    trace.AppendLine(MethodCallLog(frame));
            }

            return trace.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <typeparam name="TField"></typeparam>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static List<TField> GetPublicStaticPropValues<TClass, TField>(bool excludeNonReadonlyFields = false) where TClass : class
        {
            var vals = GetPublicStaticPropValues(typeof(TClass), typeof(TField), excludeNonReadonlyFields);
            return vals.Cast<TField>().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <typeparam name="TField"></typeparam>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static List<object> GetPublicStaticPropValues(Type classType, Type fieldType, bool excludeNonReadonlyFields = false)
        {
            var classValues = new List<object>();

            var flags = BindingFlags.Public | BindingFlags.Static;

            if(!classType.IsClass)
                throw new ArgumentException($"{nameof(classType)} must be a class.");
            
            var props = classType.GetProperties(flags).Where(f => f.PropertyType == fieldType && (!excludeNonReadonlyFields || !f.CanWrite));            
            props.ForEach(p => classValues.Add(p.GetValue(null, null)));

            classValues.Sort();
            return classValues;
        }

        public static List<TField> GetPublicStaticFieldValues<TClass, TField>(bool excludeNonReadonlyFields = false) where TClass : class
        {
            var vals = GetPublicStaticFieldValues(typeof(TClass), typeof(TField), excludeNonReadonlyFields);
            return vals.Cast<TField>().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <typeparam name="TField"></typeparam>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static List<object> GetPublicStaticFieldValues(Type classType, Type fieldType, bool excludeNonReadonlyFields = false)
        {
            var classValues = new List<object>();

            var flags = BindingFlags.Public | BindingFlags.Static;

            if (!classType.IsClass)
                throw new ArgumentException($"{nameof(classType)} must be a class.");

            var fields = classType.GetFields(flags).Where(f => f.FieldType == fieldType && (!excludeNonReadonlyFields || f.IsInitOnly));
            fields.ForEach(f => classValues.Add(f.GetValue(null)));

            classValues.Sort();
            return classValues;
        }

        /// <summary>
        /// Instead of visiting each field of stackFrame,
        /// the StackFrame.ToString() method could be used, 
        /// but the returned text would not include the class name.
        /// </summary>
        /// <param name="stackFrame"></param>
        /// <returns></returns>
        private static String MethodCallLog(StackFrame stackFrame, bool outputParameterValues = false)
        {
            var trace = new StringBuilder();
                        
            var method = stackFrame.GetMethod();            

            trace.AppendFormat("{0}.{1}({2})", method.DeclaringType.Name, method.Name, FormatParams(method.GetParameters()));

            var methodId = method.GetCustomAttributes(typeof(MethodIdAttribute), false).Cast<MethodIdAttribute>().FirstOrDefault();
            trace.AppendFormat("{0}", methodId != null ? "{ID:" + methodId.Id + "}" : "");

            var sourceFileName = stackFrame.GetFileName();
            if (!String.IsNullOrEmpty(sourceFileName))
            {
                var file = SysIO.Path.GetFileName(sourceFileName);
                var path = sourceFileName.Replace( "\\" + file, "");
                trace.AppendFormat(" {0}:{1} ({2})", file, stackFrame.GetFileLineNumber().ToString(), path);
            }

            trace.AppendFormat(" [{0}]", method.DeclaringType.Namespace);

            return trace.ToString();
        }

        /// <summary>
        /// Concatinates the parameter names into a string separated by commas.
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        private static string FormatParams(ParameterInfo[] parms)
        {
            int i = 0;
            var retVal = new StringBuilder();
            foreach (var param in parms)
            {
                if (i > 0)
                    retVal.Append(", ");
                retVal.Append(param.ParameterType.Name);
                i++;
            }
            return retVal.ToString();
        }
    }
}
