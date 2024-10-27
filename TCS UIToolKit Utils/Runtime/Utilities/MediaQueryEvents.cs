using System;
using UnityEngine;
namespace UIToolkitDemo {
    public static class MediaQueryEvents {
        public static Action<Vector2> ResolutionUpdated;
        public static Action<MediaAspectRatio> AspectRatioUpdated;
    }
}