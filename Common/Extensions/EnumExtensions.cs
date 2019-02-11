using System;
using System.ComponentModel;
using System.Reflection;

namespace Socha3.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        /// <summary>
        /// Get the underlying int for an enum.  
        /// </summary>
        /// <example>
        /// enum WidgetType
        /// {
        ///     Contoso = 1,
        ///     Northwinds = 2
        /// }
        /// 
        /// int num = WidgetType.Northwind.Int(); //returns 2
        /// </example>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int ToInt(this Enum en)
        {
            int retVal;

            var code = en.GetTypeCode();            

            if(code == TypeCode.Int32)
                retVal = (int)Convert.ChangeType(en, code);
            else
                throw new InvalidOperationException("The enum's underlying type must be int32.");

            return (int)retVal;
        }
    }
}