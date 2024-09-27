using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TCS.UIToolKitUtils {
    public static class GeneratorExtensions {
        /// <summary>
        /// Checks if the specified string is null or an empty string.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>True if the string is null or empty; otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        /// <summary>
        /// Checks if the specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>True if the string is null, empty, or consists only of white-space characters; otherwise, false.</returns>
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);
        
        /// <summary>
        /// Converts the specified string to a static format by replacing hyphens with underscores and converting characters to uppercase.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>The converted string with hyphens replaced by underscores and characters in uppercase.</returns>
        public static string ConvertToStatic(this string str) {
            if (str.IsNullOrEmpty()) return str; // Quick return for null or empty strings

            var sb = new StringBuilder(str.Length); // Preallocate capacity based on input length

            foreach (char c in str) {
                sb.Append(c == '-' ? '_' : char.ToUpper(c)); // Append uppercase character
                // Replace '-' with '_'
            }

            return sb.ToString();
        }
        
        /// <summary>
        /// Converts the specified string to an alphanumeric format, optionally allowing periods.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <param name="allowPeriods">If set to true, periods are allowed in the output string.</param>
        /// <returns>The converted alphanumeric string with optional periods.</returns>
        public static string ConvertToAlphanumeric(this string input, bool allowPeriods = false) {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            List<char> filteredChars = new List<char>();
            int lastValidIndex = -1;

            // Iterate over the input string, filtering and determining valid start/end indices
            foreach (char character in input
                         .Where(character => char
                                    .IsLetterOrDigit(character) || character == '_' || (allowPeriods && character == '.'))
                         .Where(character => filteredChars.Count != 0 || (!char.IsDigit(character) && character != '.'))) {

                filteredChars.Add(character);
                lastValidIndex = filteredChars.Count - 1; // Update lastValidIndex for valid characters
            }

            // Remove trailing periods
            while (lastValidIndex >= 0 && filteredChars[lastValidIndex] == '.') {
                lastValidIndex--;
            }

            // Return the filtered string
            return lastValidIndex >= 0
                ? new string(filteredChars.ToArray(), 0, lastValidIndex + 1) : string.Empty;
        }
    }
}