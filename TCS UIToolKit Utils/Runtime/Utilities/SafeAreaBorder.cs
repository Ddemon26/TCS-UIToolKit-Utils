using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UIToolkitDemo
{
    /// <summary>
    /// Manages the safe area borders for devices with notches or rounded corners. This uses a
    /// container element on top of all UIs that need to respect the safe area. Then, it adjusts the
    /// borderWidth property to match the Screen.safeArea property values.
    /// </summary>
    [ExecuteInEditMode]
    public class SafeAreaBorder : MonoBehaviour
    {
        [FormerlySerializedAs("m_Document")]
        [Tooltip("UI document that contains the UXML hierarchy")]
        [SerializeField] UIDocument m_document;
        [FormerlySerializedAs("m_BorderColor")]
        [Tooltip("Color for the border area. Use a transparent color to show the background.")]
        [SerializeField] Color m_borderColor = Color.black;
        [FormerlySerializedAs("m_Element")]
        [Tooltip("Name of top-level element container. Leave empty to use rootVisualElement.")]
        [SerializeField] string m_element;
        [FormerlySerializedAs("m_Multiplier")]
        [Tooltip("Percentage multiplier for safe area distance")]
        [Range(0, 1f)]
        [SerializeField] float m_multiplier = 1f;
        [FormerlySerializedAs("m_Debug")]
        [Tooltip("Show debug messages in the console.")]
        [SerializeField] bool m_debug;

        VisualElement m_root;
        float m_leftBorder;
        float m_rightBorder;
        float m_topBorder;
        float m_bottomBorder;

        public VisualElement RootElement => m_root;
        public float LeftBorder => m_leftBorder;
        public float RightBorder => m_rightBorder;
        public float TopBorder => m_topBorder;
        public float BottomBorder => m_bottomBorder;

        public float Multiplier { get => m_multiplier; set => m_multiplier = value; }

        void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {

            if (m_document == null || m_document.rootVisualElement == null)
            {
                Debug.LogWarning("UIDocument or rootVisualElement is null. Delaying initialization.");
                return;
            }

            // Choose the root VisualElement if nothing is specified
            if (string.IsNullOrEmpty(m_element))
            {
                m_root = m_document.rootVisualElement;
            }
            // Otherwise, try to find the container by name
            else
            {
                m_root = m_document.rootVisualElement.Q<VisualElement>(m_element);
            }

            if (m_root == null)
            {
                if (m_debug)
                {
                    Debug.LogWarning("[SafeAreaBorder]: m_Root is null. Element not found or UIDocument is not initialized.");
                }
                return;
            }

            // Register a callback for when the UI geometry changes
            m_root.RegisterCallback<GeometryChangedEvent>(evt => OnGeometryChangedEvent());

            ApplySafeArea();
        }

        void OnGeometryChangedEvent()
        {
            ApplySafeArea();
        }

        void OnValidate()
        {
            // Call ApplySafeArea when m_Multiplier is changed
            ApplySafeArea();
        }

        // Applies the safe area to the borders
        void ApplySafeArea()
        {
            if (m_root == null)
                return;

            var safeArea = Screen.safeArea;

            // Calculate borders based on safe area rect
            m_leftBorder = safeArea.x;
            m_rightBorder = Screen.width - safeArea.xMax;
            m_topBorder = Screen.height - safeArea.yMax;
            m_bottomBorder = safeArea.y;


            // Set border widths regardless of orientation
            m_root.style.borderTopWidth = m_topBorder * m_multiplier;
            m_root.style.borderBottomWidth = m_bottomBorder * m_multiplier;
            m_root.style.borderLeftWidth = m_leftBorder * m_multiplier;
            m_root.style.borderRightWidth = m_rightBorder * m_multiplier;


            // Apply border color
            m_root.style.borderBottomColor = m_borderColor;
            m_root.style.borderTopColor = m_borderColor;
            m_root.style.borderLeftColor = m_borderColor;
            m_root.style.borderRightColor = m_borderColor;

            if (m_debug)
            {
                Debug.Log($"[SafeAreaBorder] Applied Safe Area | Screen Orientation: {Screen.orientation} | Left: {m_leftBorder}, Right: {m_rightBorder}, Top: {m_topBorder}, Bottom: {m_bottomBorder}");
            }

        }
    }
}