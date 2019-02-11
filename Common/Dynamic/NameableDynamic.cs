using Socha3.Common.Extensions;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Socha3.Common.Dynamic
{
    /// <summary>
    /// An dynamic object whose properties are created from a dictionary dynamically at run-time.
    /// </summary>
    public class NameableDynamic : DynamicObject
    {
        private Dictionary<string, object> _properties;

        /// <summary>
        /// Instantiates the dynamic object from a Dictionary&lt;string, object&gt; whose keys are the names of the properties accessible off of the object<para/>
        /// Example:<para/>
        /// var propDict = new Dictionary&lt;string, object&gt;();<para/>
        /// propDict.Add("Bob", bobPersonObject);<para/>
        /// propDict.Add("Joe", joePersonObject);<para/>
        /// propDict.Add("ReasonForMakingThisObject", "To illustrate the dyno-amazement!");<para/>
        /// propDict.Add("IsItFunToBePoor", false);<para/>
        /// <para/>
        /// dynamic myObj = new NameableDynamic(propDict);<para/>
        /// var bobsZip = myObj.Bob.Address.ZipCode;<para/>
        /// MessageBox.Show(myObj.IsItFunToBePoor ? "You're a liar" : "I know, right?");<para/>
        /// </summary>
        /// <param name="properties">string=name of property, object=value of property</param>
        /// <param name="caseInsensitivePropNames"></param>
        public NameableDynamic(Dictionary<string, object> properties, bool caseInsensitivePropNames = false)
        {
            SetProperties(properties, caseInsensitivePropNames);
        }

        public NameableDynamic(bool caseInsensitivePropNames = false)
        {
            SetProperties();
        }

        protected NameableDynamic()
        {
        }

        public static void ValidatePropertyNames(Dictionary<string, object> properties)
        {
            properties.ForEach(p =>
            {
                if (!p.Key.IsValidCsIdentifier())
                    throw new ArgumentOutOfRangeException($"Property name '{p.Key}' is not a valid identifier.");
            });
        }

        protected void SetProperties(Dictionary<string, object> properties = null, bool caseInsensitivePropNames = false)
        {
            if (properties != null)
            {
                ValidatePropertyNames(properties);

                if (caseInsensitivePropNames)
                    _properties = new Dictionary<string, object>(properties, EqualityComparers.CaseInsensitiveString);
                else
                    _properties = new Dictionary<string, object>(properties);
            }
            else
            {
                if (caseInsensitivePropNames)
                    _properties = new Dictionary<string, object>(EqualityComparers.CaseInsensitiveString);
                else
                    _properties = new Dictionary<string, object>();
            }
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _properties.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_properties.ContainsKey(binder.Name))
            {
                result = _properties[binder.Name];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_properties.ContainsKey(binder.Name))
            {
                _properties[binder.Name] = value;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}