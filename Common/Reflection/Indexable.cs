using System;

namespace Socha3.Common.AppEnvironment
{
    /// <summary>
    /// Provides a base class to achieve the indexing syntax utilized in arrays or dictionaries on a static member.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Indexable<TDerivedClass, TItem, TIndex> where TDerivedClass : class, new()
    {
        public static readonly string StaticClassID = Guid.NewGuid().ToString();
        protected static Func<TItem> GetDefault { get; set; } = () => ThrowNotImplemented(nameof(GetDefault));
        protected static Func<TIndex, TItem> Get { get; set; } = (index) => ThrowNotImplemented(nameof(Get));
        protected static Action<TIndex, TItem> Set {  get; set; } = (index, item) => ThrowNotImplemented(nameof(Set));

        private static TItem ThrowNotImplemented(string accessor)
        {
            if(accessor != string.Empty)
                throw new NotImplementedException($"{nameof(TDerivedClass)} has not implemented the {accessor} accessor.");

            return default(TItem);
        }

        static Indexable()
        {
            if (Get == null || Set == null || GetDefault == null)
                throw new Exception($"Properties '{nameof(Get)}', '{nameof(Set)}' and '{nameof(GetDefault)}' must all be set prior to use.");
        }

        public TItem Default
        {
            get
            {
                return GetDefault();
            }
        }

        public TItem this[TIndex name]
        {
            get
            {
                return Get(name);
            }
            set
            {
                Set(name, value);
            }
        }

        private static TDerivedClass _instance;
        public static TDerivedClass Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new TDerivedClass();

                return _instance;
            }
        }        
    }
}
