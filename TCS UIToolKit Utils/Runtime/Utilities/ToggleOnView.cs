using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

namespace UIToolkitDemo {
    public static class MainMenuUIEvents {
        public static Action<string> CurrentViewChanged;
    }

    [Serializable]
    public struct ViewState {
        public string m_viewName;
        public bool m_state;
    }

    /// <summary>
    ///  Utility class for toggling UI elements based on the current UI View
    /// </summary>
    public class ToggleOnView : MonoBehaviour {
        [Tooltip("UXML Document containing the element to toggle")]
        [SerializeField] UIDocument m_document;
        [Tooltip("Name of the Visual Element to toggle")]
        [SerializeField] string m_elementID;
        [Header("Enable on states")]
        [Tooltip("Specify a display state based on the name of the currently active UIView (HomeView, CharView, etc.)")]
        [SerializeField] List<ViewState> m_viewStates = new();

        VisualElement m_elementToToggle;

        void OnEnable() {
            Initialize();
            MainMenuUIEvents.CurrentViewChanged += OnCurrentViewChanged;
        }

        void OnDisable() => MainMenuUIEvents.CurrentViewChanged -= OnCurrentViewChanged;

        void Initialize() {
            if (!m_document) {
                Debug.LogWarning("[ToggleOnMenu] UIDocument required.");
                return;
            }

            m_elementToToggle = m_document.rootVisualElement.Q<VisualElement>(m_elementID);

            if (m_elementToToggle == null) {
                Debug.LogWarning("[ToggleOnMenu]: Element not found.");
                return;
            }

            m_elementToToggle.style.visibility = Visibility.Visible;
        }

        // 
        void OnCurrentViewChanged(string newViewName) {

            // Find a ViewState where the viewName == newViewName
            var matchingViewState = m_viewStates.FirstOrDefault(x => x.m_viewName == newViewName);

            bool isMatchingView = matchingViewState is { m_viewName: not null, m_state: true };


            m_elementToToggle.style.display = (isMatchingView) ? DisplayStyle.Flex : DisplayStyle.None;
            m_elementToToggle.style.visibility = (isMatchingView) ? Visibility.Visible : Visibility.Hidden;


        }
    }
}