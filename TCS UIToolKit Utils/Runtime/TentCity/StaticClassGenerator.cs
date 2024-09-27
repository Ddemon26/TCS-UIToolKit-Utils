using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TCS.UIToolKitUtils {
    public class StaticClassGenerator {
        const string FILEPATH = "Assets/UI Toolkit/StringLibrary/";

        /// <summary>
        /// Generates a static class definition as a string, containing constant string fields
        /// based on the provided list of strings.
        /// </summary>
        /// <param name="namespaceName">The namespace for the generated class.</param>
        /// <param name="className">The name of the generated class.</param>
        /// <param name="stringList">The list of strings to be included as constant fields in the class.</param>
        /// <param name="length">The length of the variable name before it gets Abbreviated</param>
        /// <returns>A string containing the generated static class definition.</returns>
        static string GenerateStaticClass(string namespaceName, string className, List<string> stringList, int length = 25) {
            int estimatedSize = (stringList.Count * 50) + 200;
            var classBuilder = new StringBuilder(estimatedSize);

            classBuilder.AppendLine("// This file was generated using TC Generation");
            classBuilder.AppendLine($"namespace {namespaceName}");
            classBuilder.AppendLine("{");
            classBuilder.AppendLine($"    public static class {className}");
            classBuilder.AppendLine("    {");

            const string fieldTemplate = "        public const string {0} = \"{1}\";";

            foreach (string s in stringList) {
                if (s.IsNullOrWhiteSpace()) continue;
                string formattedStr = s.ConvertToStatic().AbbreviateString(length);
                classBuilder.AppendLine(string.Format(fieldTemplate, formattedStr, s));
            }

            classBuilder.AppendLine("    }");
            classBuilder.AppendLine("}");

            return classBuilder.ToString();
        }

        /// <summary>
        /// Saves the generated static class to a file.
        /// </summary>
        /// <param name="namespaceName">The namespace for the generated class.</param>
        /// <param name="className">The name of the generated class.</param>
        /// <param name="stringList">The list of strings to be included as constant fields in the class.</param>
        /// <param name="filePath">The file path where the class file will be saved. Defaults to FILEPATH.</param>
        /// <param name="length">The length of the variable name before it gets Abbreviated</param>
        /// <exception cref="ArgumentException">Thrown when the namespace or class name is null or whitespace.</exception>
        /// <seealso cref="GenerateStaticClass"/>
        public void SaveToFile(string namespaceName, string className, List<string> stringList, string filePath = FILEPATH, int length = 25) {
            if (namespaceName.IsNullOrWhiteSpace() || className.IsNullOrWhiteSpace())
                throw new ArgumentException("Namespace and class name must be provided.");

            // Generate the static class code as a string
            string classCode = GenerateStaticClass(namespaceName, className, stringList, length); // ref GenerateStaticClass

            // Combine the file path and class name to get the full file path
            string fullFilePath = Path.Combine(filePath, $"{className}.cs");
            string directoryPath = Path.GetDirectoryName(fullFilePath);

            // Create the directory if it does not exist
            if (!directoryPath.IsNullOrEmpty() && !Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            // Write the generated class code to the file
            File.WriteAllText(fullFilePath, classCode);
        }
    }
}