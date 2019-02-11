using Socha3.Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Socha3.Common.Reflection
{
    public class JsonUtil
    {
        private static readonly JsonSerializerSettings _defaultSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        /// <summary>
        /// Serialize an object to a Json-formatted string.  Default setting is to ignore reference loops.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string Serialize(object obj, JsonSerializerSettings settings = null, bool maskErrors = false)
        {
            var retVal = "";
            if (settings == null)
                settings = _defaultSettings;

            if (maskErrors)
                settings.Error = new EventHandler<ErrorEventArgs>(MaskError);

            retVal = JsonConvert.SerializeObject(obj, settings);

            return retVal;
        }

        private static void MaskError(object sender, ErrorEventArgs e)
        {
            e.ErrorContext.Handled = true;
        }

        /// <summary>
        /// Deserialize a Json-formatted string to an object.  Default setting is to ignore reference loops.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json, JsonSerializerSettings settings = null, bool maskErrors = false)
        {
            T retVal = default(T);

            if (settings == null)
                settings = _defaultSettings;

            if (maskErrors)
                settings.Error = new EventHandler<ErrorEventArgs>(MaskError);

                retVal = JsonConvert.DeserializeObject<T>(json, settings);

            return retVal;
        }

        /// <summary>
        /// Takes a json object and attempts a conversion to dictionary.  Dictionary count = 0 if failed.<para/>
        /// <para/>
        /// {"DemoObj":<para/>
        ///     {<para/>
        ///         "prop1":34,<para/>
        ///         "prop2":{["subProp1":"adsfhlkajhsdf"},{"subProp1":"adsfhlkajhsdf"}]},<para/>
        ///         "prop3":"aseffea",<para/>
        ///         "prop4":{"subprop1":47777.23, "subprop2":3423566666}<para/>
        ///     }<para/>
        /// }<para/>
        /// <para/>
        /// Looks like:<para/>
        /// <para/>
        /// demoObj["prop2"] = "[{"subProp1":"adsfhlkajhsdf"},{"subProp1":"adsfhlkajhsdf"}]"<para/>
        /// demoObj["prop3"] = "aseffea"<para/>
        /// ...<para/>
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ConvertToDictionary(string json)
        {
            var dict = new Dictionary<string, string>(EqualityComparers.CaseInsensitiveString);

            var obj = JObject.Parse(json);

            var props = obj.Children().Children().Children();
            foreach (JProperty prop in props)
            {
                dict.Add(prop.Name, prop.Value.ToString());
            }

            return dict;
        }
    }
}
