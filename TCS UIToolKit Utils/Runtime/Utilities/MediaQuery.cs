using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
namespace UIToolkitDemo {
    public static class MediaQueryEvents {
        public static Action<Vector2> ResolutionUpdated;
        public static Action<MediaAspectRatio> AspectRatioUpdated;
    }

    // Categorize by aspect ratio
    public enum MediaAspectRatio {
        Undefined,
        Landscape,
        Portrait
    }

    [ExecuteInEditMode]
    public class MediaQuery : MonoBehaviour {
        [SerializeField] UIDocument m_document;

        const float K_LANDSCAPE_MIN = 1.2f;

        Vector2 m_currentResolution;

        // Landscape, Portrait, or Undefined
        MediaAspectRatio m_currentAspectRatio;

        public Vector2 CurrentResolution => m_currentResolution;

        void OnEnable() {
            if (!m_document) {
                Debug.Log("[MediaQuery]: Assign UI Document.");
                return;
            }

            var root = m_document.rootVisualElement;

            root?.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);

            QueryResolution();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
        void Start() => QueryResolution();
        void OnGeometryChanged(GeometryChangedEvent evt) => UpdateResolution();

        // Update if resolution changed from previous
        public void QueryResolution() {

            var newResolution = new Vector2(Screen.width, Screen.height);

            if (newResolution != m_currentResolution) {
                m_currentResolution = newResolution;
                MediaQueryEvents.ResolutionUpdated?.Invoke(newResolution);
            }

            var newAspectRatio = CalculateAspectRatio(newResolution);

            if (newAspectRatio != m_currentAspectRatio) {
                m_currentAspectRatio = newAspectRatio;
                MediaQueryEvents.AspectRatioUpdated?.Invoke(newAspectRatio);
            }
        }

        // Force update resolution and aspect ratio
        public void UpdateResolution() {
            var newResolution = new Vector2(Screen.width, Screen.height);
            MediaQueryEvents.ResolutionUpdated?.Invoke(newResolution);
            var newAspectRatio = CalculateAspectRatio(newResolution);
            MediaQueryEvents.AspectRatioUpdated?.Invoke(newAspectRatio);
        }

        public static MediaAspectRatio CalculateAspectRatio(Vector2 resolution) {
            if (Math.Abs(resolution.y) < float.Epsilon) {
                Debug.LogWarning("[MediaQuery] CalculateAspectRatio: Height is zero. Cannot calculate aspect ratio.");
                return MediaAspectRatio.Undefined;
            }

            float aspectRatio = resolution.x / resolution.y;

            return aspectRatio >= K_LANDSCAPE_MIN ? MediaAspectRatio.Landscape : MediaAspectRatio.Portrait;

        }

        public static MediaAspectRatio CalculateAspectRatio(float width, float height) => CalculateAspectRatio(new Vector2(width, height));
        void OnSceneLoaded(Scene scene, LoadSceneMode mode) => UpdateResolution();
    }
}