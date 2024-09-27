using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace TCS.UIToolKitUtils {
    public static class ParserHelpers {
        public const string FILEPATH = "Assets/UI Toolkit/StringLibrary/";
        /// <summary>
        /// Creates a read-only, multi-line text area with the specified text content.
        /// </summary>
        /// <param name="textContent">The text content to display in the text area.</param>
        /// <returns>A <see cref="TextField"/> object configured as a read-only, multi-line text area.</returns>
        public static TextField CreateTextArea(string textContent) 
            => new() { value = textContent, multiline = true, isReadOnly = true };
        
        /// <summary>
        /// Loads the content of the specified asset as a string.
        /// </summary>
        /// <param name="asset">The asset to load content from.</param>
        /// <returns>A string containing the content of the asset.</returns>
        public static string LoadAssetContent(Object asset) {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            return System.IO.File.ReadAllText(assetPath);
        }
        
        /// <summary>
        /// Extracts the names of all elements from the given UXML asset.
        /// </summary>
        /// <param name="visualTreeAsset">The UXML asset to extract names from.</param>
        /// <returns>A list of names of all elements in the UXML asset.</returns>
        public static List<string> ExtractNamesFromUxml(VisualTreeAsset visualTreeAsset) {
            List<string> nameList = new();

            var root = new VisualElement();
            visualTreeAsset.CloneTree(root);

            TraverseVisualElementTree(root, nameList);

            return nameList;
        }

        /// <summary>
        /// Recursively traverses the visual element tree and collects the names of all elements.
        /// </summary>
        /// <param name="element">The root visual element to start the traversal from.</param>
        /// <param name="nameList">The list to store the names of the elements.</param>
        static void TraverseVisualElementTree(VisualElement element, List<string> nameList) {
            if (!string.IsNullOrEmpty(element.name)) {
                nameList.Add(element.name);
            }

            foreach (var child in element.Children()) {
                TraverseVisualElementTree(child, nameList);
            }
        }
        
        /// <summary>
        /// Extracts the class names from the given StyleSheet asset.
        /// </summary>
        /// <param name="styleSheet">The StyleSheet asset to extract class names from.</param>
        /// <returns>A list of class names found in the StyleSheet asset.</returns>
        public static List<string> ExtractClassNamesFromStyleSheet(StyleSheet styleSheet) {
            List<string> classNames = new();
            string styleSheetPath = AssetDatabase.GetAssetPath(styleSheet);
            string styleSheetContent = System.IO.File.ReadAllText(styleSheetPath);

            var regex = new System.Text.RegularExpressions.Regex(@"[#.]([a-zA-Z0-9_-]+)\s*\{");
            var matches = regex.Matches(styleSheetContent);

            foreach (System.Text.RegularExpressions.Match match in matches) {
                if (match.Groups.Count > 1) {
                    classNames.Add(match.Groups[1].Value);
                }
            }

            return classNames;
        }
    }
}