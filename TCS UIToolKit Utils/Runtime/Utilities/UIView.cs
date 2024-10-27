using UnityEngine.UIElements;
using System;

namespace UIToolkitDemo {
    /// <summary>
    /// This is a base class for a functional unit of the UI. This can make up a full-screen interface or just
    /// part of one.
    /// </summary>
    public class UIView : IDisposable {
        protected bool HideOnAwake = true;

        // UI reveals other underlaying UIs, partially see-through
        protected bool IsOverlay;

        protected VisualElement TopElement;

        // Properties
        public VisualElement Root => TopElement;
        public bool IsTransparent => IsOverlay;
        public bool IsHidden => TopElement.style.display == DisplayStyle.None;

        // Constructor
        /// <summary>
        /// Initializes a new instance of the UIView class.
        /// </summary>
        /// <param name="topElement">The topmost VisualElement in the UXML hierarchy.</param>
        public UIView(VisualElement topElement) {
            TopElement = topElement ?? throw new ArgumentNullException(nameof(topElement));
            Initialize();
        }

        public virtual void Initialize() {
            if (HideOnAwake) {
                Hide();
            }

            SetVisualElements();
            RegisterButtonCallbacks();
        }

        // Sets up the VisualElements for the UI. Override to customize.
        protected virtual void SetVisualElements() { }

        // Registers callbacks for buttons in the UI. Override to customize.
        protected virtual void RegisterButtonCallbacks() { }

        // Displays the UI.
        public virtual void Show() {
            TopElement.style.display = DisplayStyle.Flex;
        }

        // Hides the UI.
        public virtual void Hide() {
            //this here sets your flex box to none, hiding the UI
            TopElement.style.display = DisplayStyle.None;
        }

        // Unregisters any callbacks or event handlers. Override to customize.
        public virtual void Dispose() { }
    }
}