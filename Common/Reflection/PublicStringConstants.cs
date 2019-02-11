using Socha3.Common.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Socha3.Common.Reflection
{
    /// <summary>
    /// Derive classes from this to provide the 'Props' property, which is a dynamically-generated ReadOnlyCollection&lt;string&gt;<para/>
    /// containing all the values for all the public static readonly fields of type string in TClass.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public abstract class PublicStringConstants<TClass> : PublicConstantsClassBase<TClass, string> where TClass : class
    {
        private static ReadOnlyCollection<string> _propValuesLowered;
        /// <summary>
        /// Returns the same as 'Props' except each item has had a ToLower() executed on each item.
        /// </summary>
        public static ReadOnlyCollection<string> PropValuesLowered
        {
            get
            {
                if (_propValuesLowered == null)
                {                    
                    var temp = new List<string>();
                    PropValues.ForEach(val =>
                    {                        
                        if(val != null)
                            temp.Add(val.ToLower());
                        else
                            temp.Add(null);
                    });
                    _propValuesLowered = new ReadOnlyCollection<string>(temp);
                }
                return _propValuesLowered;
            }
        }

        public static string JoinLoweredPropValues(string seperator)
        {
            var joined = string.Join(seperator, PropValues);
            return joined;
        }

        private static ReadOnlyCollection<string> _fieldValuesLowered;
        /// <summary>
        /// Returns the same as 'Fields' except each item has had a ToLower() executed on each item.
        /// </summary>
        public static ReadOnlyCollection<string> FieldValuesLowered
        {
            get
            {
                if (_fieldValuesLowered == null)
                {
                    var temp = new List<string>();
                    PropValues.ForEach(val =>
                    {
                        if (val != null)
                            temp.Add(val.ToLower());
                        else
                            temp.Add(null);
                    });
                    _fieldValuesLowered = new ReadOnlyCollection<string>(temp);
                }
                return _fieldValuesLowered;
            }
        }

        public static string JoinLoweredFieldValues(string seperator)
        {
            var joined = string.Join(seperator, FieldValues);
            return joined;
        }
    }
}
