using System;
using UnityEngine;
namespace UIToolkitDemo {
    public static class ThemeEvents {
        public static Action<string> ThemeChanged;
        public static Action<Camera> CameraUpdated;
    }
}