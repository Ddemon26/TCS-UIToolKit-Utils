using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
namespace UIToolkitDemo {
    /// <summary>
    /// This component pairs a specific camera with a specific theme, enabling the
    /// corresponding camera when switching.
    /// </summary>
    [ExecuteInEditMode]
    public class ActiveThemeCamera : MonoBehaviour {
        [FormerlySerializedAs("m_CameraThemes")]
        [Tooltip("Pairs a camera with a theme.")]
        [SerializeField] List<CameraTheme> m_cameraThemes;
        [FormerlySerializedAs("m_SendEvent")]
        [Tooltip("Sends a Theme Event to notify other components of the updated camera.")]
        [SerializeField] bool m_sendEvent;
        [FormerlySerializedAs("m_Debug")]
        [Tooltip("Logs debug messages at the console.")]
        [SerializeField] bool m_debug;

        string m_currentTheme;
        Camera m_activeCamera;

        public List<CameraTheme> CameraThemes => m_cameraThemes;

        public Camera ActiveCamera => m_activeCamera;

        void OnEnable() {
            if (m_cameraThemes.Count == 0) {
                Debug.LogWarning("[ActiveThemeCamera]: Add CameraThemes to toggle theme cameras");
                return;
            }

            ThemeEvents.ThemeChanged += OnThemeChanged;

            MediaQueryEvents.AspectRatioUpdated += OnAspectRatioUpdated;

            m_activeCamera = m_cameraThemes[0].m_camera;
            m_currentTheme = m_cameraThemes[0].m_theme;
        }


        void OnDisable() {
            ThemeEvents.ThemeChanged -= OnThemeChanged;
            MediaQueryEvents.AspectRatioUpdated -= OnAspectRatioUpdated;

        }

        public void ShowCamera(int index) {
            for (var i = 0; i < m_cameraThemes.Count; i++) {
                m_cameraThemes[i].m_camera.gameObject.SetActive(false);

                if (index == i)
                    m_activeCamera = m_cameraThemes[i].m_camera;
            }

            m_activeCamera.gameObject.SetActive(true);

            if (m_debug)
                Debug.Log("[Active Theme Camera]: " + m_activeCamera.name);

            if (m_sendEvent)
                ThemeEvents.CameraUpdated?.Invoke(m_activeCamera);

        }

        // Event-handling methods

        void OnThemeChanged(string themeName) {
            int index = m_cameraThemes.FindIndex(x => x.m_theme == themeName);
            ShowCamera(index);
        }

        // Apply Landscape or Portrait Theme StyleSheets 
        void OnAspectRatioUpdated(MediaAspectRatio mediaAspectRatio) {
            // Save the suffix to Default, Christmas, or Halloween
            string suffix = ThemeManager.GetSuffix(m_currentTheme, "--");

            // Add Portrait or Landscape as the basename
            string newThemeName = mediaAspectRatio + suffix;

            int index = m_cameraThemes.FindIndex(x => x.m_theme == newThemeName);


            ShowCamera(index);
        }
    }
}