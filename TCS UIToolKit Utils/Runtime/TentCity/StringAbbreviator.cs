using System.Text.RegularExpressions;

namespace TCS.UIToolKitUtils {
    public static class StringAbbreviator {
        static readonly Regex NonAlphaRegex = new("[^a-zA-Z_]");

        const int SHORT_WORD_MAX_LENGTH = 3;
        const int MEDIUM_WORD_MAX_LENGTH = 6;
        const int LONG_WORD_ABBREVIATION_LENGTH = 4;
        const int MEDIUM_WORD_ABBREVIATION_LENGTH = 3;
        const int FURTHER_REDUCTION_LENGTH = 2;

        /// <summary>
        /// Abbreviates the input string to ensure it does not exceed the specified maximum length.
        /// </summary>
        /// <param name="inputString">The input string to be abbreviated.</param>
        /// <param name="maxLength">The maximum allowed length for the final string.</param>
        /// <returns>The abbreviated string, trimmed to the maximum length and without trailing underscores.</returns>
        public static string AbbreviateString(this string inputString, int maxLength) {
            if (inputString.Length <= maxLength) {
                return inputString;
            }

            string cleanString = CleanString(inputString);

            if (cleanString.Length <= maxLength) {
                return cleanString.TrimEnd('_');
            }

            string[] parts = cleanString.Split('_');
            string abbreviatedString = AbbreviateParts(parts);

            if (abbreviatedString.Length > maxLength) {
                abbreviatedString = ReducePartsFurther(parts);
            }

            return FinalizeString(abbreviatedString, maxLength);
        }

        /// <summary>
        /// Cleans the input string by removing all non-alphabetic characters except underscores.
        /// </summary>
        /// <param name="input">The input string to be cleaned.</param>
        /// <returns>A cleaned string containing only alphabetic characters and underscores.</returns>
        static string CleanString(string input) => NonAlphaRegex.Replace(input, "");

        /// <summary>
        /// Abbreviates each part of the given array of strings.
        /// </summary>
        /// <param name="parts">An array of strings representing parts of the original string.</param>
        /// <returns>A single string with each part abbreviated and joined by underscores.</returns>
        static string AbbreviateParts(string[] parts) {
            for (var i = 0; i < parts.Length; i++) {
                parts[i] = AbbreviatePart(parts[i]);
            }

            return string.Join("_", parts);
        }

        /// <summary>
        /// Abbreviates a single part of a string based on its length.
        /// </summary>
        /// <param name="part">The part of the string to be abbreviated.</param>
        /// <returns>
        /// The abbreviated part of the string. If the part's length is less than or equal to the short word maximum length,
        /// it returns the part as is. If the part's length is less than or equal to the medium word maximum length,
        /// it returns the part truncated to the medium word abbreviation length. Otherwise, it returns the part truncated
        /// to the long word abbreviation length.
        /// </returns>
        static string AbbreviatePart(string part) {
            if (part.Length <= SHORT_WORD_MAX_LENGTH) {
                return part;
            }

            return part.Length
                   <= MEDIUM_WORD_MAX_LENGTH ? part[..MEDIUM_WORD_ABBREVIATION_LENGTH]
                : part[..LONG_WORD_ABBREVIATION_LENGTH];
        }

        /// <summary>
        /// Reduces the length of each part in the given array of strings further by truncating parts
        /// that exceed the short word maximum length to a predefined shorter length.
        /// </summary>
        /// <param name="parts">An array of strings representing parts of the original string.</param>
        /// <returns>A single string with each part truncated and joined by underscores.</returns>
        static string ReducePartsFurther(string[] parts) {
            for (var i = 0; i < parts.Length; i++) {
                if (parts[i].Length > SHORT_WORD_MAX_LENGTH) {
                    parts[i] = parts[i][..FURTHER_REDUCTION_LENGTH];
                }
            }

            return string.Join("_", parts);
        }

        /// <summary>
        /// Finalizes the abbreviated string by ensuring it does not exceed the specified maximum length
        /// and trims any trailing underscores.
        /// </summary>
        /// <param name="abbreviatedString">The string that has been abbreviated.</param>
        /// <param name="maxLength">The maximum allowed length for the final string.</param>
        /// <returns>The finalized string, trimmed to the maximum length and without trailing underscores.</returns>
        static string FinalizeString(string abbreviatedString, int maxLength) {
            if (abbreviatedString.Length > maxLength) {
                abbreviatedString = abbreviatedString[..maxLength];
            }

            return abbreviatedString.TrimEnd('_');
        }
    }
}