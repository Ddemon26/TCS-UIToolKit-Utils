#if !ODIN_INSPECTOR_3_3
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static ToolKit_Helper.ParserHelpers;

namespace ToolKit_Helper {
    public class ToolKitHelperEditorWindow : EditorWindow {
        [MenuItem("Tools/TCS/UIToolKit Helper")]
        static void OpenWindow() => GetWindow<ToolKitHelperEditorWindow>().Show();

        StyleSheet m_styleSheet;
        VisualTreeAsset m_visualTreeAsset;
        int m_maxLength = 20;
        string m_namespace;
        string m_customFilePath;

        string m_styleSheetPreview;
        string m_uxmlPreview;
        ScrollView m_scrollView;
        StaticClassGenerator m_staticClassGenerator;

        string SafeNamespace => m_namespace.ConvertToAlphanumeric();

        void OnEnable() {
            InitializeComponents();
            CreateToolbar();
            CreateFields();
        }

        void InitializeComponents() {
            m_staticClassGenerator = new StaticClassGenerator();
            m_scrollView = new ScrollView();
            rootVisualElement.Add(m_scrollView);
        }

        void CreateToolbar() {
            var toolbar = new Toolbar();
            toolbar.Add(new ToolbarButton(() => ShowPreview("StyleSheet")) { text = "StyleSheet" });
            toolbar.Add(new ToolbarButton(() => ShowPreview("UXML")) { text = "UXML" });
            toolbar.Add(new ToolbarButton(HidePreviews) { text = "Hide Previews" });
            rootVisualElement.Add(toolbar);
        }

        void CreateFields() {
            // StyleSheet field
            var styleSheetField = new ObjectField("StyleSheet") {
                objectType = typeof(StyleSheet),
                value = m_styleSheet
            };
            styleSheetField.RegisterValueChangedCallback(evt =>
            {
                m_styleSheet = evt.newValue as StyleSheet;
                OnStyleSheetChanged();
            });
            rootVisualElement.Add(styleSheetField);

            // VisualTreeAsset field
            var uxmlField = new ObjectField("UXML File") {
                objectType = typeof(VisualTreeAsset),
                value = m_visualTreeAsset
            };
            uxmlField.RegisterValueChangedCallback(evt =>
            {
                m_visualTreeAsset = evt.newValue as VisualTreeAsset;
                OnVisualTreeAssetChanged();
            });
            rootVisualElement.Add(uxmlField);

            // Max Length field
            var maxLengthField = new IntegerField("Max Length") {
                value = m_maxLength
            };
            maxLengthField.RegisterValueChangedCallback(evt => m_maxLength = evt.newValue);
            rootVisualElement.Add(maxLengthField);

            // Namespace field
            var namespaceField = new TextField("Namespace") {
                value = m_namespace
            };
            namespaceField.RegisterValueChangedCallback(evt => m_namespace = evt.newValue);
            rootVisualElement.Add(namespaceField);

            // Custom File Path field
            var filePathField = new TextField("Custom File Path") {
                value = m_customFilePath
            };
            filePathField.RegisterValueChangedCallback(evt => m_customFilePath = evt.newValue);
            rootVisualElement.Add(filePathField);

            // Button to generate static classes
            var generateButton = new Button(GenerateStaticClasses) {
                text = "Generate Static Classes"
            };
            rootVisualElement.Add(generateButton);
        }

        void HidePreviews() => m_scrollView.Clear();

        void ShowPreview(string previewType) {
            m_scrollView.Clear();

            switch (previewType) {
                case "StyleSheet":
                    if (m_styleSheet) {
                        var styleSheetTextArea = CreateTextArea(m_styleSheetPreview);
                        m_scrollView.Add(styleSheetTextArea);
                    }

                    break;

                case "UXML":
                    if (m_visualTreeAsset) {
                        var uxmlTextArea = CreateTextArea(m_uxmlPreview);
                        m_scrollView.Add(uxmlTextArea);
                    }

                    break;
            }
        }

        void OnStyleSheetChanged() {
            m_styleSheetPreview = m_styleSheet ? LoadAssetContent(m_styleSheet) : string.Empty;
            ShowFirstPreview();
        }

        void OnVisualTreeAssetChanged() {
            m_uxmlPreview = m_visualTreeAsset ? LoadAssetContent(m_visualTreeAsset) : string.Empty;
            ShowFirstPreview();
        }

        void ShowFirstPreview() {
            if (!string.IsNullOrEmpty(m_styleSheetPreview)) {
                ShowPreview("StyleSheet");
            }
            else if (!string.IsNullOrEmpty(m_uxmlPreview)) {
                ShowPreview("UXML");
            }
        }

        void GenerateStaticClasses() {
            if (!ValidateAssetsSelected()) return;

            string namespaceToUse = string.IsNullOrEmpty(SafeNamespace) ? "UI_Toolkit" : SafeNamespace;
            string filePath = string.IsNullOrEmpty(m_customFilePath) ? "Assets/UI Toolkit/StringLibrary/" : m_customFilePath;

            if (m_styleSheet) {
                GenerateStyleSheetClass(namespaceToUse, filePath);
            }

            if (m_visualTreeAsset) {
                GenerateUxmlClass(namespaceToUse, filePath);
            }

            EditorUtility.DisplayDialog("Success", "Static classes generated successfully!", "OK");
            AssetDatabase.Refresh();
        }

        bool ValidateAssetsSelected() {
            if (m_styleSheet || m_visualTreeAsset) return true;
            EditorUtility.DisplayDialog("Error", "No StyleSheet or UXML file selected!", "OK");
            return false;
        }

        void GenerateStyleSheetClass(string namespaceToUse, string filePath) {
            string className = m_styleSheet.name.EndsWith("SS")
                ? $"{m_styleSheet.name[..^2]}Classes"
                : $"{m_styleSheet.name}Classes";
            List<string> classNames = ExtractClassNamesFromStyleSheet(m_styleSheet);
            m_staticClassGenerator.SaveToFile(namespaceToUse, className, classNames, filePath, m_maxLength);
        }

        void GenerateUxmlClass(string namespaceToUse, string filePath) {
            var className = $"{m_visualTreeAsset.name}Strings";
            List<string> nameVariables = ExtractNamesFromUxml(m_visualTreeAsset);
            m_staticClassGenerator.SaveToFile(namespaceToUse, className, nameVariables, filePath, m_maxLength);
        }
    }
}
#endif