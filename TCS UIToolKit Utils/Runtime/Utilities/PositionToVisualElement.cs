using UnityEngine;
using UnityEngine.UIElements;
namespace UIToolkitDemo {
    public class PositionToVisualElement : MonoBehaviour {
        [Header("Transform")]
        [SerializeField] GameObject m_objectToMove;

        [Header("Camera parameters")]
        [SerializeField] Camera m_camera;
        [SerializeField] float m_depth = 10f;

        [Header("UI Target")]
        [SerializeField] UIDocument m_document;
        [SerializeField] string m_elementName;

        VisualElement m_targetElement;

        void OnEnable() {
            if (!m_document) {
                Debug.LogError("[PositionToVisualElement]: UIDocument is not assigned.");
                return;
            }

            var root = m_document.rootVisualElement;
            m_targetElement = root.Q<VisualElement>(name: m_elementName);

            ThemeEvents.CameraUpdated += OnCameraUpdated;

            if (m_targetElement == null) {
                Debug.LogError($"[PositionToVisualElement]: Element '{m_elementName}' not found.");
                return;
            }

            m_targetElement.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);

        }

        void OnDisable() {
            ThemeEvents.CameraUpdated -= OnCameraUpdated;

            m_targetElement?.UnregisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }


        void Start() => MoveToElement();

        public void MoveToElement() {
            if (!m_camera) {
                Debug.LogError("[PositionToVisualElement] MoveToElement: Camera is not assigned.");
                return;
            }

            if (!m_objectToMove) {
                Debug.LogError("[PositionToVisualElement] MoveToElement: Object to move is not assigned.");
                return;
            }

            // Locate the center screen position in UI Toolkit
            var worldBound = m_targetElement.worldBound;
            var centerPosition = new Vector2(worldBound.x + worldBound.width / 2, worldBound.y + worldBound.height / 2);

            // Convert to pixel coordinates using extension method
            var screenPos = centerPosition.GetScreenCoordinate(m_document.rootVisualElement);

            // Convert to world position using extension method
            var worldPosition = screenPos.ScreenPosToWorldPos(m_camera, m_depth);

            if (m_objectToMove) {
                m_objectToMove.transform.position = worldPosition;
            }
        }

        // Update the Camera for Portrait/Landscape Themes
        void OnCameraUpdated(Camera cam) {
            m_camera = cam;
            MoveToElement();
        }

        // Move the GameObject whenever the UI element sets up or moves
        void OnGeometryChanged(GeometryChangedEvent evt) => MoveToElement();
    }
}