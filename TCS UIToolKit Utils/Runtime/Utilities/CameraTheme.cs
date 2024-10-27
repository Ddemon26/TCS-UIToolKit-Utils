using UnityEngine;
using System;
using UnityEngine.Serialization;


namespace UIToolkitDemo {
    // Pairs a Theme StyleSheet with a string 
    [Serializable]
    public struct CameraTheme {
        [FormerlySerializedAs("camera")]
        public Camera m_camera;
        [FormerlySerializedAs("theme")]
        public string m_theme;
    }
}