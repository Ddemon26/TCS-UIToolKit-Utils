using System.Collections.Generic;
using UnityEngine.UIElements;

namespace TCS.UIToolKitUtils {
    public static class StyleSheetExtensions {
        public static void RemoveAllStyleSheets(this UIDocument uiDocument) {   
            List<VisualElement> allElements = uiDocument.rootVisualElement.Query().ToList();
            foreach (var element in allElements) {  
                element.styleSheets.Clear();
            }
        }
        public static void AddAllStyleSheet(this UIDocument uiDocument, StyleSheet styleSheet) {
            List<VisualElement> allElements = uiDocument.rootVisualElement.Query().ToList();
            foreach (var element in allElements) {
                element.styleSheets.Add(styleSheet);
            }
        }
        
        public static void AddStyleSheetQuery(this UIDocument uiDocument, StyleSheet styleSheet, string query) {
            List<VisualElement> allElements = uiDocument.rootVisualElement.Query(query).ToList();
            foreach (var element in allElements) {
                element.styleSheets.Add(styleSheet);
            }
        }
        
        public static void RemoveStyleSheetQuery(this UIDocument uiDocument, StyleSheet styleSheet, string query) {
            List<VisualElement> allElements = uiDocument.rootVisualElement.Query(query).ToList();
            foreach (var element in allElements) {
                element.styleSheets.Remove(styleSheet);
            }
        }
        
        public static void RemoveStyleSheetQ(this UIDocument uiDocument, string q) {
            var element = uiDocument.rootVisualElement.Q(q);
            element.styleSheets.Clear();
        }
        
        public static void AddStyleSheetQ(this UIDocument uiDocument, StyleSheet styleSheet, string q) {
            var element = uiDocument.rootVisualElement.Q(q);
            element.styleSheets.Add(styleSheet);
        }
    }
}