using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Nondisplayable.Extras
{
    /// <summary>
    /// Runtime assertions and sanity checks
    /// </summary>
    public static class Validate
    {
        /// <summary>
        /// Crash if the object is null.
        /// </summary>
        /// <param name="o">The object that cannot be null.</param>
        /// <param name="objectName">The human-readable name of the object, if any.</param>
        public static void NotNull(object o, string objectName = null, [CallerMemberName] string callingMember = "unknown", [CallerFilePath] string callingFile = "unknown", [CallerLineNumber] int callingLineNumber = -1)
        {
            if (o == null)
            {
                if (objectName != null)
                {
                    Fail($"Object '{objectName}' must not be null.", callingMember, callingFile, callingLineNumber);
                }
                else
                {
                    Fail("Object must not be null.", callingMember, callingFile, callingLineNumber);
                }
            }
        }

        /// <summary>
        /// Crash if the string is null, blank, or consists entirely of whitespace.
        /// </summary>
        /// <param name="s">The string that cannot be empty.</param>
        /// <param name="objectName">The human-readable name of the string, if any.</param>
        public static void NotNullOrWhitespace(string s, string objectName = null, [CallerMemberName] string callingMember = "unknown", [CallerFilePath] string callingFile = "unknown", [CallerLineNumber] int callingLineNumber = -1)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                string reason = "";

                if (s != null)
                {
                    reason = "it is null.";
                }
                else
                {
                    reason = "it is blank.";
                }

                if (objectName != null)
                {
                    Fail($"The string '{objectName}' must not be null or blank, but {reason}", callingMember, callingFile, callingLineNumber);
                }
                else
                {
                    Fail($"The string must not be null or blank, but {reason}", callingMember, callingFile, callingLineNumber);
                }
            }
        }

        public static void IsOfType<TExpectedType>(object o, string objectName = null, [CallerMemberName] string callingMember = "unknown", [CallerFilePath] string callingFile = "unknown", [CallerLineNumber] int callingLineNumber = -1)
        {
            if(!(o is TExpectedType))
            {
                string actualType = "null";

                if(o != null)
                {
                    actualType = o.GetType().FullName;
                }
                
                if(objectName != null)
                {
                    Fail($"The object '{objectName}' is not of type {typeof(TExpectedType).FullName}; it is a {actualType}", callingMember, callingFile, callingLineNumber);
                }
                else
                {
                    Fail($"The object is not of type {typeof(TExpectedType).FullName}; it is a {actualType}", callingMember, callingFile, callingLineNumber);
                }
            }
        }

        public static void IsEnumType<T>([CallerMemberName] string callingMember = "unknown", [CallerFilePath] string callingFile = "unknown", [CallerLineNumber] int callingLineNumber = -1)
        {
            if(!typeof(T).IsEnum)
            {
                Fail($"The type '{typeof(T).FullName}' is not an enum type.", callingMember, callingFile, callingLineNumber);
            }
        }

        private static void Fail(string message, string callingMember, string callingFilePath, int lineNumber)
        {
            var fullMessage = $"{message}\nThe assertion failed in {callingMember} ({callingFilePath}:{lineNumber}).";
            throw new ValidationException(fullMessage);
        }
    }

    /// <summary>
    /// Describes a failure of a runtime assertion
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {

        }
    }
}
