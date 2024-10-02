using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
namespace UIToolkitDemo {
    public static class SettingsEvents {
        public static Action<bool> FpsCounterToggled;
        public static Action<int> TargetFrameRateSet;
    }

    public class FpsCounter : MonoBehaviour {
        public const int K_TARGET_FRAME_RATE = 60; // 60 for mobile platforms, -1 for fast as possible
        public const int K_BUFFER_SIZE = 50; // Number of frames to sample
        
        [SerializeField] UIDocument m_document;

        float m_fpsValue;
        int m_currentIndex;
        float[] m_deltaTimeBuffer;

        Label m_fpsLabel;
        bool m_isEnabled;

        public float FpsValue => m_fpsValue;

        // MonoBehaviour event messages
        void Awake() {
            m_deltaTimeBuffer = new float[K_BUFFER_SIZE];
            Application.targetFrameRate = K_TARGET_FRAME_RATE;
        }

        void OnEnable() {
            SettingsEvents.FpsCounterToggled += OnFpsCounterToggled;
            SettingsEvents.TargetFrameRateSet += OnTargetFrameRateSet;

            var root = m_document.rootVisualElement;

            m_fpsLabel = root.Q<Label>("fps-counter");

            if (m_fpsLabel == null) {
                Debug.LogWarning("[FPSCounter]: Display label is null.");
            }
        }

        void OnDisable() {
            SettingsEvents.FpsCounterToggled -= OnFpsCounterToggled;
            SettingsEvents.TargetFrameRateSet -= OnTargetFrameRateSet;
        }

        void Update() {
            if (m_isEnabled) {
                m_deltaTimeBuffer[m_currentIndex] = Time.deltaTime;
                m_currentIndex = (m_currentIndex + 1) % m_deltaTimeBuffer.Length;
                m_fpsValue = Mathf.RoundToInt(CalculateFps());

                m_fpsLabel.text = $"FPS: {m_fpsValue}";
            }

        }

        // Methods
        float CalculateFps() {
            float totalTime = m_deltaTimeBuffer.Sum();

            return m_deltaTimeBuffer.Length / totalTime;
        }

        // Event-handling methods
        void OnFpsCounterToggled(bool state) {
            m_isEnabled = state;
            m_fpsLabel.style.visibility = (state) ? Visibility.Visible : Visibility.Hidden;
        }

        // Set the target frame rate:  -1 = as fast as possible (PC) or 60/30 fps (mobile) 
        void OnTargetFrameRateSet(int newFrameRate) => Application.targetFrameRate = newFrameRate;
    }
}