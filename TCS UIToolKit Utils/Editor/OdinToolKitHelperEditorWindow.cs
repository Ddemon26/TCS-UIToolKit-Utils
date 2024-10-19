#if ODIN_INSPECTOR_3_3
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static TCS.UIToolKitUtils.ParserHelpers;

namespace TCS.UIToolKitUtils {
    public class OdinToolKitHelperEditorWindow : OdinEditorWindow {
        [MenuItem("Tools/TCS/UIToolKit Helper")]
        static void OpenWindow() => GetWindow<OdinToolKitHelperEditorWindow>().Show();

        [Title("UIToolKit Helper")]
        [InfoBox("Select your StyleSheet and UXML files and enter a custom namespace.")]
        [FoldoutGroup("Assets", expanded: true)]
        [Tooltip("Select the StyleSheet file.")]
        [OnValueChanged(nameof(OnStyleSheetChanged))]
        public StyleSheet m_styleSheet;

        [FoldoutGroup("Assets")]
        [Tooltip("Select the UXML file.")]
        [OnValueChanged(nameof(OnVisualTreeAssetChanged))]
        public VisualTreeAsset m_visualTreeAsset;

        [FoldoutGroup("Settings", expanded: true)]
        [Title("Max Length")]
        [InfoBox("Enter the maximum length of the generated variable names.")]
        [PropertyRange(5, 50)]
        [Tooltip("Maximum length for the generated variable names.")]
        public int m_maxLength = 20;

        [FoldoutGroup("Settings")]
        [Title("Namespace")]
        [InfoBox("Enter a custom namespace or leave empty to use the default.")]
        [Tooltip("Custom namespace for the generated classes.")]
        public string m_namespace;

        [FoldoutGroup("Settings")]
        [Title("File Path")]
        [InfoBox("Enter a custom file path or leave empty to use the default: Assets/UI Toolkit/StringLibrary/")]
        [FolderPath]
        [Tooltip("Custom file path for saving the generated classes.")]
        public string m_customFilePath;

        string m_styleSheetPreview;
        string m_uxmlPreview;
        ScrollView m_scrollView;
        StaticClassGenerator m_staticClassGenerator;

        string SafeNamespace => m_namespace.ConvertToAlphanumeric();

        protected override void OnEnable() {
            base.OnEnable();
            InitializeComponents();
            CreateToolbar();
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

        void HidePreviews() => m_scrollView.Clear();

        void ShowPreview(string previewType) {
#if UNITY_EDITOR
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
#endif
        }

        void OnStyleSheetChanged() {
#if UNITY_EDITOR
            m_styleSheetPreview = m_styleSheet ? LoadAssetContent(m_styleSheet) : string.Empty;
            ShowFirstPreview();
#endif
        }

        void OnVisualTreeAssetChanged() {
#if UNITY_EDITOR
            m_uxmlPreview = m_visualTreeAsset ? LoadAssetContent(m_visualTreeAsset) : string.Empty;
            ShowFirstPreview();
#endif
        }

        void ShowFirstPreview() {
            if (!string.IsNullOrEmpty(m_styleSheetPreview)) {
                ShowPreview("StyleSheet");
            }
            else if (!string.IsNullOrEmpty(m_uxmlPreview)) {
                ShowPreview("UXML");
            }
        }

        [Button("Generate Static Classes")]
        void GenerateStaticClasses() {
            if (!ValidateAssetsSelected()) return;

            string namespaceToUse = string.IsNullOrEmpty(SafeNamespace) ? "UI_Toolkit" : SafeNamespace;
            string filePath = string.IsNullOrEmpty(m_customFilePath) ? FILEPATH : m_customFilePath;

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
#if UNITY_EDITOR
            string className = m_styleSheet.name.EndsWith("SS")
                ? $"{m_styleSheet.name[..^2]}Classes"
                : $"{m_styleSheet.name}Classes";
            List<string> classNames = ExtractClassNamesFromStyleSheet(m_styleSheet);
            m_staticClassGenerator.SaveToFile(namespaceToUse, className, classNames, filePath, m_maxLength);
#endif
        }

        void GenerateUxmlClass(string namespaceToUse, string filePath) {
#if UNITY_EDITOR
            var className = $"{m_visualTreeAsset.name}Strings";
            List<string> nameVariables = ExtractNamesFromUxml(m_visualTreeAsset);
            m_staticClassGenerator.SaveToFile(namespaceToUse, className, nameVariables, filePath, m_maxLength);
#endif
        }
    }
}
#endif