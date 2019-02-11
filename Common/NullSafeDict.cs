using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socha3.Common
{
    public class NullSafeDict<TKey, TValue> : Dictionary<TKey, TValue> where TValue : class
    {
        public new TValue this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                    return null;
                else
                    return base[key];
            }
            set
            {
                if (!ContainsKey(key))
                    Add(key, value);
                else
                    base[key] = value;
            }
        }
    }
}
