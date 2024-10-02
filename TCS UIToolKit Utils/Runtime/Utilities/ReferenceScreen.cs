using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UIToolkitDemo
{
    // overlay texture for reference; disable when not in use
    [ExecuteInEditMode]
    [RequireComponent(typeof(UIDocument))]
    public class ReferenceScreen : MonoBehaviour
    {
        UIDocument m_document;
        VisualElement m_root;


        [FormerlySerializedAs("m_Opacity")]
        [SerializeField] [Range(0f, 1f)] float m_opacity = 0.5f;
        [FormerlySerializedAs("disableOnPlay")]
        public bool m_disableOnPlay = true;

        void OnEnable()
        {
            m_document = GetComponent<UIDocument>();
            m_root = m_document.rootVisualElement;

            // overlay on top
            m_document.sortingOrder = 9999;
        }

        void Start()
        {
            if (m_disableOnPlay)
            {
                gameObject.SetActive(false);
            }
        }

        void Update()
        {
            if (m_root != null)
                m_root.style.opacity = m_opacity;
        }
    }
}
