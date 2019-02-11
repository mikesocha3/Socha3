using Socha3.Common.Reflection;
using System.Collections.ObjectModel;

namespace Socha3.Common.Reflection
{
    /// <summary>
    /// Derive classes from this to provide the 'AllValues' property, which is a dynamically-generated ReadOnlyCollection&lt;TField&gt;<para/>
    /// containing all the values for all the public static readonly properties of type TProp in TClass.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TProp"></typeparam>
    public abstract class PublicConstantsClassBase<TClass, TProp> where TClass : class
    {
        protected static ReadOnlyCollection<TProp> _propValues;
        /// <summary>
        /// Gets a ReadOnlyCollection&lt;TField&gt; of all the public static readonly prop values of type TProp within TClass
        /// </summary>
        public static ReadOnlyCollection<TProp> PropValues
        {
            get
            {
                if (_propValues == null)
                    _propValues = ReflectionUtil.GetPublicStaticPropValues<TClass, TProp>(true).AsReadOnly();

                return _propValues;
            }
        }

        public static string JoinPropValues(string seperator)
        {
            var joined = string.Join(seperator, PropValues);
            return joined;
        }

        protected static ReadOnlyCollection<TProp> _fieldValues;
        /// <summary>
        /// Gets a ReadOnlyCollection&lt;TField&gt; of all the public static readonly field values of type TProp within TClass
        /// </summary>
        public static ReadOnlyCollection<TProp> FieldValues
        {
            get
            {
                if (_fieldValues == null)
                    _fieldValues = ReflectionUtil.GetPublicStaticFieldValues<TClass, TProp>(true).AsReadOnly();

                return _fieldValues;
            }
        }

        public static string JoinFieldValues(string seperator)
        {
            var joined = string.Join(seperator, PropValues);
            return joined;
        }
    }
}
