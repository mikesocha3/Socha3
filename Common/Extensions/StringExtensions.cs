using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Socha3.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Get this string's equivalent enum value by matching to an enum value's description or the name of the enum value. (Comparison method is case-insensitive if left null.)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="comparer">Specify how to compare this string with the enum's description or value name.  Comparison method is case-insensitive if comparer is left null.</param>
        /// <param name="defaultIfNotFound">Specify to return the Enum's default if this string doesn't correspond to any of the enum's values.</param>
        /// <param name="matchEnumName">Specify whether to return the enum if the input string is matched on the enum's field name.</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string input, IEqualityComparer<string> comparer = null, bool defaultIfNotFound = false, bool matchOnDescription = true, bool matchOnFieldName = true)
            where T : struct, IConvertible //limit to only enum types
        {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentException("Input string is null or white space.");
            if (!matchOnFieldName && !matchOnDescription) throw new ArgumentException("matchOnFieldName and matchOnDescription cannot both be false.");

            var type = typeof(T);

            if (!type.IsEnum) throw new InvalidOperationException();

            if (comparer == null)
                comparer = EqualityComparers.CaseInsensitiveString;

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                string description = null;
                if (attribute != null)
                    description = attribute.Description;

                if ((matchOnFieldName && comparer.Equals(input, field.Name)) || (matchOnDescription && comparer.Equals(input, description)))
                    return (T)field.GetValue(null);
            }

            if (!defaultIfNotFound)
                throw new ArgumentException($"Enum for '{input}' of type '{type.FullName}' not found.");
            else
                return default(T);
        }

        public static string Truncate(this String input, int length)
        {
            if (input.Length < length) return input;
            int index = input.LastIndexOf(' ', length - 3, 15);
            if (index == -1)
            {
                index = length - 3;
            }
            return input.Substring(0, index) + "...";
        }

        public static DateTime ToDate(this string input)
        {
            DateTime result;
            if (!DateTime.TryParse(input, out result))
                throw new FormatException($"'{input}' cannot be converted as DateTime.");
            return result;
        }

        public static int ToInt(this string input)
        {
            int result;
            if (!int.TryParse(input, out result))
                throw new FormatException($"'{input}' cannot be converted to int.");
            return result;
        }

        public static bool ToBool(this string input)
        {
            bool result;
            if (!bool.TryParse(input, out result))
                throw new FormatException($"'{input}' cannot be converted as bool.");

            return result;
        }

        public static double ToDouble(this string input)
        {
            double result;
            if (!double.TryParse(input, out result))
                throw new FormatException($"'{input}' cannot be converted as double.");
            return result;
        }

        public static bool Contains(this string input, string search, StringComparison comparer)
        {
            return input.IndexOf(search, comparer) >= 0;
        }

        public static bool ContainsCaseInsensitive(this string input, string search)
        {
            return input.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string ExtractQueryStringValue(this string input, string key)
        {
            return HttpUtility.ParseQueryString(input)[key];
        }

        public static bool IsValidEmailAddress(this string input)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(input);
        }

        public static string IsNullOrWhiteSpace(this string input, string defaultString)
        {
            if (string.IsNullOrWhiteSpace(input))
                return defaultString;
            else return input;
        }

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Returns true if the path is a dir, false if it's a file and null if it's neither or doesn't exist.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool? IsDirFile(this string path)
        {
            bool? result = null;

            if (Directory.Exists(path) || File.Exists(path))
            {
                // get the file attributes for file or directory
                var fileAttr = File.GetAttributes(path);

                if (fileAttr.HasFlag(FileAttributes.Directory))
                    result = true;
                else
                    result = false;
            }

            return result;
        }

        /// <summary>
        /// Convert a string to a MemoryString.  (UTF8 is the default encoding.)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding">If left null will default to UTF8</param>
        /// <returns></returns>
        public static MemoryStream ToMemoryStream(this string input, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var stream = new MemoryStream(encoding.GetBytes(input));

            return stream;
        }

        /// <summary>
        /// Encrypts a string using the supplied key. Encoding is done using RSA encryption.
        /// </summary>
        /// <param name="input">String that must be encrypted.</param>
        /// <param name="key">Encryptionkey.</param>
        /// <returns>A string representing a byte array separated by a minus sign.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToEncrypt or key is null or empty.</exception>
        public static string Encrypt(this string input, string key)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("An empty string value cannot be encrypted.");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");

            CspParameters cspp = new CspParameters
            {
                KeyContainerName = key
            };

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp)
            {
                PersistKeyInCsp = true
            };

            byte[] bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(input), true);

            return BitConverter.ToString(bytes);
        }

        /// <summary>
        /// Decryptes a string using the supplied key. Decoding is done using RSA encryption.
        /// </summary>
        /// <param name="input">String that must be decrypted.</param>
        /// <param name="key">Decryptionkey.</param>
        /// <returns>The decrypted string or null if decryption failed.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToDecrypt or key is null or empty.</exception>
        public static string Decrypt(this string input, string key)
        {
            string result = null;

            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
            }

            try
            {
                CspParameters cspp = new CspParameters();
                cspp.KeyContainerName = key;

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;

                string[] decryptArray = input.Split(new string[] { "-" }, StringSplitOptions.None);
                byte[] decryptByteArray = Array.ConvertAll<string, byte>(decryptArray, (s => Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber))));


                byte[] bytes = rsa.Decrypt(decryptByteArray, true);

                result = System.Text.UTF8Encoding.UTF8.GetString(bytes);

            }
            finally
            {
                // no need for further processing
            }

            return result;
        }

        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Formats the string according to the specified mask
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="mask">The mask for formatting. Like "A##-##-T-###Z"</param>
        /// <returns>The formatted string</returns>
        public static string FormatWithMask(this string input, string mask)
        {
            if (input.IsNullOrEmpty()) return input;
            var output = string.Empty;
            var index = 0;
            foreach (var m in mask)
            {
                if (m == '#')
                {
                    if (index < input.Length)
                    {
                        output += input[index];
                        index++;
                    }
                }
                else
                    output += m;
            }
            return output;
        }

        public static string RemoveWhitespace(this string input)
        {
            var output = new StringBuilder();

            input.ForEach(c =>
                {
                    if (!Char.IsWhiteSpace(c))
                        output.Append(c);
                });

            return output.ToString();
        }

        public static bool ContainsWhitespace(this string input)
        {
            bool contains = input.Any(c => Char.IsWhiteSpace(c));
            return contains;
        }

        public static string ReplaceCaseInsensitive(this string input, string token, string replacement)
        {
            int count, position0, position1;
            count = position0 = position1 = 0;
            string upperString = input.ToUpper();
            string upperPattern = token.ToUpper();
            int inc = (input.Length / token.Length) *
                      (replacement.Length - token.Length);
            char[] chars = new char[input.Length + System.Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern,
                                              position0)) != -1)
            {
                for (int i = position0; i < position1; ++i)
                    chars[count++] = input[i];
                for (int i = 0; i < replacement.Length; ++i)
                    chars[count++] = replacement[i];
                position0 = position1 + token.Length;
            }
            if (position0 == 0) return input;
            for (int i = position0; i < input.Length; ++i)
                chars[count++] = input[i];
            return new string(chars, 0, count);

        }

        /// <summary>
        /// Converts the string to byte[] (2 bytes per char) and then returns the concatenated string of each byte's 3-digit int representation.
        /// </summary>
        /// <example>
        /// "abc" in utf-16 is stored as: byte[] { 97, 0, 98, 0, 99, 0 }
        /// Concatenate the 3-digit (0-255) int version of each byte and you get: "097000098000099000"
        /// 097000 = 'a'
        /// 098000 = 'b'
        /// 099000 = 'c'
        /// </example>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToIntUtf16Representation(this string input)
        {
            var chars = input.ToCharArray();

            var bytes = Encoding.Unicode.GetBytes(chars);

            var output = new StringBuilder();

            foreach (var @byte in bytes)
            {
                output.Append(Convert.ToInt32(@byte).ToString("D3"));
            }

            return output.ToString();
        }

        public static string FromIntUtf16Representation(this string input)
        {
            if (!input.IsNumeric() || input.Length % 3 != 0)
                throw new FormatException("Input string must be numeric and length must be a factor of 6. (Example inputs: 255255, 032000097000 or 012000)");

            var chars = input.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (0 % 3 == 0)
                {
                    string num = "" + chars[i] + chars[i + 1] + chars[i + 2];
                    int number;
                    int.TryParse(num, out number);
                }
            }

            var bytes = Encoding.Unicode.GetBytes(chars);

            var output = new StringBuilder();

            foreach (var @byte in bytes)
            {
                output.Append(Convert.ToInt32(@byte).ToString("D3"));
            }

            return output.ToString();
        }

        /// <summary>
        /// Used with IsNumeric() and is equal to long.MaxValue.ToString().Length - 1.
        /// When parsing a number with as many or more digits than long.MaxValue, the number is broken into chunks of this length (18) 
        /// and tried to parse.  The -1 at the end of this calculation is to account for the fact that 99999 99999 99999 9999 (19 digits) is too large to parse
        /// because it is greater than long.MaxValue (also 19 digits but smaller).
        /// </summary>
        private static readonly int LongMaxChunkLength = long.MaxValue.ToString().Length - 1;

        /// <summary>
        /// Returns whether the input string contains only numeric chars.<para/>
        /// Note: "1231.564657.777" IsNumeric according to this method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="allowSign">If true, a number preceded by a '+' or '-' will be allowed.</param>
        /// <param name="allowWhitespace">If true, whitespace will be removed before determining if is numeric.</param>
        /// <returns></returns>
        public static bool IsNumeric(this string input, bool allowSign = true, bool allowWhitespace = false)
        {
            //for decimal numbers
            input = input.Replace(".", "");

            bool isNumeric = false;

            if (input.Length <= 0)
            {
                isNumeric = false;
            }
            else
            {
                string preppedNumStr = input;

                if (preppedNumStr.ContainsWhitespace())
                {
                    if (allowWhitespace)
                        preppedNumStr = preppedNumStr.RemoveWhitespace();
                    else
                        return false;
                }

                char firstChar = preppedNumStr[0];
                bool firstCharIsSign = firstChar == '+' || firstChar == '-';

                if (firstCharIsSign)
                {
                    if (allowSign)
                        preppedNumStr = preppedNumStr.Substring(1);
                    else
                        return false;
                }

                long num;
                if (long.TryParse(preppedNumStr, out num))
                    isNumeric = true;
                else
                {
                    // if was not parseable to long, it may just be because
                    // the number is reallllly big.  Check that here.
                    // trim any leading zeroes first though.
                    if (preppedNumStr.TrimStart(new char[] { '0' }).Length > LongMaxChunkLength)
                    {
                        // attempt parse of number blob in chunks
                        var length = preppedNumStr.Length;
                        for (int i = 0; i < length; i = i + LongMaxChunkLength)
                        {
                            //if last substring to be chunked needs to be shorter, make sure to just grab what's left to avoid an exception
                            var chunkLength = (i + LongMaxChunkLength > length) ? length - i : LongMaxChunkLength;
                            string chunk = preppedNumStr.Substring(i, chunkLength);
                            if (!chunk.IsNumeric(false, allowWhitespace))
                                return false;
                        }
                        isNumeric = true;
                    }
                    else
                        isNumeric = false;
                }
            }

            return isNumeric;
        }

        /// <summary>
        /// Convert the given string to pascal case to be valid as a C# identifier.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Pascalize(this string value)
        {
            Regex rx = new Regex(@"(?:[^a-zA-Z0-9]*)(?<first>[a-zA-Z0-9])(?<reminder>[a-zA-Z0-9]*)(?:[^a-zA-Z0-9]*)");
            var result = rx.Replace(value.ToString(), m => m.Groups["first"].ToString().ToUpper() + m.Groups["reminder"].ToString().ToLower());

            if (!result.IsValidCsIdentifier())
                result = $"_{result}";

            return result;
        }

        public static bool IsValidCsIdentifier(this string identifier)
        {
            const string start = @"(\p{Lu}|\p{Ll}|\p{Lt}|\p{Lm}|\p{Lo}|\p{Nl})";
            const string extend = @"(\p{Mn}|\p{Mc}|\p{Nd}|\p{Pc}|\p{Cf})";

            var ident = new Regex(string.Format("{0}({0}|{1})*", start, extend));
            var normalizedIdent = identifier.Normalize();

            return ident.IsMatch(normalizedIdent);
        }

        public static bool IsInt(this string input)
        {
            var isInt = true;
            foreach (char c in input)
            {
                if (!c.In('0', '1', '2', '3', '4', '5', '6', '7', '8', '9'))
                {
                    isInt = false;
                    break;
                }
            }

            return isInt;
        }

        /// <summary>
        /// Assumes the format of the string is in the format "D" like: "xxxxxxxx-xxxx-xxxx-xxxxxxxxxxxx"
        /// </summary>
        /// <param name="input"></param>
        /// <param name="endianReversed">Certain hex/string/guid conversions reverse certain bits in the guid string, causing the need to reorder the characters to get the same Guid.<para/>
        /// If this parameter is set to true, the guid string's bits will be reordered before conversion as follows:<para/>
        /// "12345678-1234-1234-1234-1234567890AB" is the same as the endian-reversed below:<para/>
        /// "87654321-4321-4321-2143-2143658709BA"
        /// </param>
        /// <returns></returns>
        public static Guid ToGuid(this string input, bool endianReversed = false)
        {
            if (input?.Length != 36)
                throw new Exception($"The input string was not in the correct format. Length must be (32 + 4).  Actual: {(input == null ? 0 : input.Length)}.");

            Guid guid;
            if (endianReversed)
            {
                var parts = input.Split('-').ToList();
                var chars = new List<char>();
                chars.AddRange(parts[0].Reverse());
                chars.AddRange(parts[1].Reverse());
                chars.AddRange(parts[2].Reverse());
                parts[3] += parts[4];
                for(int i = 0; i < parts[3].Length; i = i + 2)
                    chars.AddRange(new[] { parts[3][i + 1], parts[3][i] });

                var guidStr = "";
                chars.ForEach(c => guidStr += c);
                guid = Guid.ParseExact(guidStr, "N");
            }
            else
            {
                guid = Guid.ParseExact(input, "D");
            }
            return guid;
        }

        /// <summary>
        /// Shortcut for Environment.ExpandEnvironmentVariables()
        /// </summary>
        /// <param name="vars"></param>
        /// <returns></returns>
        public static string ExpandEnvars(this string vars)
        {
            return Environment.ExpandEnvironmentVariables(vars);
        }

        /// <summary>
        /// Takes a filename such as photo.jpg and returns photo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TruncateExtension(this string input)
        {
            if (input != null && input.Contains("."))
                return input.Substring(0, input.LastIndexOf('.'));
            else
                return input;
        }

        /// <summary>
        /// Takes a filename such as photo.jpg and returns jpg
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetExtension(this string input)
        {
            if (input != null && input.Contains("."))
                return input.Substring(input.LastIndexOf('.') + 1, input.Length - input.LastIndexOf('.') - 1);
            else
                return input;
        }

        public static bool IsEveryFourth(int test)
        {
            return test % 4 == 0;
        }
    }
}