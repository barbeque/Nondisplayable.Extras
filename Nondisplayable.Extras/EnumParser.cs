using System;
using System.Collections.Generic;
using System.Linq;

namespace Nondisplayable.Extras
{
    /// <summary>
    /// Parse strings into enums, providing a human-readable error message in the case of failure.
    /// </summary>
    public static class EnumParser
    {
        public static TEnumType Parse<TEnumType>(string input) where TEnumType : struct
        {
            Validate.IsEnumType<TEnumType>();

            TEnumType val;
            if(Enum.TryParse(input, out val))
            {
                return val;
            } else
            {
                // The point of this method is to just produce a better error message when it fails.
                var allNamesOfEnum = Enum.GetNames(typeof(TEnumType));
                throw new EnumParseException($"The key '{input}' is not a valid one for the enum '{typeof(TEnumType).FullName}'. Valid keys include: {string.Join(", ", allNamesOfEnum)}",
                    input, allNamesOfEnum);
            }
        }

        /// <summary>
        /// Attempts to parse an enum in a case-insensitive way.
        /// </summary>
        /// <typeparam name="TEnumType">The type of the enum</typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TEnumType ParseCaseInsensitive<TEnumType>(string input) where TEnumType : struct
        {
            Validate.IsEnumType<TEnumType>(); // sure wish the compiler could do this

            var allNames = Enum.GetNames(typeof(TEnumType)).ToList();
            var closestHit = allNames.Find(validKey => StringEx.LooseEquals(validKey, input));

            if (closestHit != null)
            {
                return Parse<TEnumType>(closestHit);
            }

            // No valid choices
            throw new EnumParseException($"The key '{input}' is not a valid one for the enum '{typeof(TEnumType)}', even when case insensitive. Valid keys include: {string.Join(", ", allNames)}",
                input,
                allNames);
        }
    }

    public class EnumParseException : Exception
    {
        public string InvalidName;
        public List<string> ValidNames { get; }

        public EnumParseException(string message, string invalidName, IEnumerable<string> validNames) : base(message)
        {
            InvalidName = invalidName;
            if(validNames != null)
            {
                ValidNames = validNames.ToList();
            }
        }
    }
}
