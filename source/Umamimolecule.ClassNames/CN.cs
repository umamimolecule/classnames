using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Umamimolecule.ClassNames
{
    /// <summary>
    /// Contains methods to evaluate and create a class name string.
    /// </summary>
    public static class CN
    {
        /// <summary>
        /// Evaluates a collection of values to produce a space-delimited string of class names.
        /// </summary>
        /// <param name="values">The values to evaluate.</param>
        /// <returns>A string containing the class names.</returns>
        /// <remarks>
        /// <a href="link">https://github.com/umamimolecule/classnames#conditions</a>
        /// <para/>A value can be one of the following:
        /// <br/> - <b>Tuple (string, bool)</b>: If the bool part is true then the string part is included.
        /// <br/> - <b>Tuple (string, Func&lt;bool&gt;)</b>: If the bool part evaluates to true then the string part is included.
        /// <br/> - <b>KeyValuePair&lt;string, bool&gt;</b>: If the value is true then the key is included.
        /// <br/> - <b>KeyValuePair&lt;string, Func&lt;bool&gt;&gt;</b>: If the value evaluates to true then the key is included.
        /// <br/> - <b>Dictionary&lt;string, bool&gt;</b>: For each true value, the key is included.
        /// <br/> - <b>Dictionary&lt;string, Func&lt;bool&gt;&gt;</b>: For each value that evaluates to true, the key is included.
        /// <br/> - <b>Func&lt;string&gt;</b>: The evaluated string is included.
        /// <para/>All other types will be converted to a string.
        /// <para/>Notes:
        /// <br/> - Any null or whitespace values are excluded.
        /// <br/> - Any collections passed in are flattened.
        /// </remarks>
        public static string Create(params object[] values)
        {
            if (values == null || values.Length == 0)
            {
                return string.Empty;
            }

            // Flatten any elements that are collections themselves
            var flat = values.SelectMany<object, object>(x =>
            {
                return x switch
                {
                    string s => new object[] { s },
                    IEnumerable e => e.Cast<object>(),
                    _ => new object[] { x },
                };
            });

            return string.Join(" ", flat.Select(Stringify).Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        private static string Stringify(object value)
        {
            if (value == null)
            {
                return null;
            }

            return value switch
            {
                ValueTuple<string, bool> tuple => tuple.Item2 ? tuple.Item1 : null,
                ValueTuple<string, Func<bool>> tuple => tuple.Item2() ? tuple.Item1 : null,
                KeyValuePair<string, bool> kvp => kvp.Value ? kvp.Key : null,
                KeyValuePair<string, Func<bool>> kvp => kvp.Value() ? kvp.Key : null,
                Func<string> f => f(),
                _ => value.ToString(),
            };
        }
    }
}
