using UnityEngine;
using UnityEngine.UIElements;
namespace TCS.UIToolKitUtils {
    /// <summary>
    /// Class <c>DisablePanelMouseInput</c> is responsible for handling functionality to disable input on a UI panel.
    /// </summary>
    /// <remarks>
    /// This class requires a <c>UIDocument</c> component to be attached to the same GameObject.
    /// </remarks>
    [RequireComponent(typeof(UIDocument))]
    public class DisablePanelMouseInput : MonoBehaviour {
        /// <summary>
        /// Represents the configuration settings for the UI panel,
        /// managing aspects of its behavior and interaction within the
        /// <see cref="UIDocument"/> component in Unity.
        /// </summary>
        PanelSettings m_panelSettings;

        /// <summary>
        /// Initializes the component by retrieving the PanelSettings from the attached UIDocument.
        /// </summary>
        void Awake() {
            m_panelSettings = GetComponent<UIDocument>()
                .panelSettings;
        }

        /// <summary>
        /// Activates the script component and disables input for the UI panel.
        /// </summary>
        void OnEnable() => DisablePanelInputFunction();

        /// <summary>
        /// Disables the input for the UI panel by setting the screen-to-panel space
        /// function to return invalid coordinates. This effectively prevents interactions
        /// with the UI panel associated with the attached UIDocument.
        /// </summary>
        void DisablePanelInputFunction() {
            if (m_panelSettings) {
                m_panelSettings.SetScreenToPanelSpaceFunction(_ => new Vector2(float.NaN, float.NaN));
            }
        }
    }
}