using UnityEngine;
using UnityEngine.UIElements;

// Static class to hold extension methods
public static class ExtensionMethods
{
    // Resets the Transform's position, rotation, and scale to their defaults.
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    // Returns the world space position of the center of a VisualElement
    public static Vector3 GetWorldPosition(this VisualElement visualElement, Camera camera = null, float zDepth = 10f)
    {
        if (camera == null)
            camera = Camera.main;

        var worldPos = Vector3.zero;

        if (camera == null || visualElement == null)
            return worldPos;

        return visualElement.worldBound.center.ScreenPosToWorldPos(camera, zDepth);
    }

    // Converts a UI Toolkit screen position to world space.
    public static Vector3 ScreenPosToWorldPos(this Vector2 screenPos, Camera camera = null, float zDepth = 10f)
    {

        if (camera == null)
            camera = Camera.main;

        if (camera == null)
            return Vector2.zero;

        float xPos = screenPos.x;
        float yPos = screenPos.y;
        var worldPos = Vector3.zero;

        if (!float.IsNaN(screenPos.x) && !float.IsNaN(screenPos.y) && !float.IsInfinity(screenPos.x) && !float.IsInfinity(screenPos.y))
        {
            // convert to world space position using Camera class
            var screenCoord = new Vector3(xPos, yPos, zDepth);
            worldPos = camera.ScreenToWorldPoint(screenCoord);
        }
        return worldPos;
    }

    // Gets screen coordinates from a UI Toolkit ClickEvent position.
    public static Vector2 GetScreenCoordinate(this Vector2 clickPosition, VisualElement rootVisualElement)
    {
        // Adjust the clickPosition for the borders (for the SafeAreaBorder)
        float borderLeft = rootVisualElement.resolvedStyle.borderLeftWidth;
        float borderTop = rootVisualElement.resolvedStyle.borderTopWidth;
        clickPosition.x += borderLeft;
        clickPosition.y += borderTop;

        // Normalize the UI Toolkit position to account for Panel Match settings
        var normalizedPosition = clickPosition.NormalizeClickEventPosition(rootVisualElement);

        // Multiply by Screen dimensions to get screen coordinates in pixels
        float xValue = normalizedPosition.x * Screen.width;
        float yValue = normalizedPosition.y * Screen.height;
        return new Vector2(xValue, yValue);
    }

    // Normalizes a UI Toolkit ClickEvent position to a range between (0,0) to (1,1).
    public static Vector2 NormalizeClickEventPosition(this Vector2 clickPosition, VisualElement rootVisualElement)
    {
        // Get a Rect that represents the boundaries of the screen in UI Toolkit
        var rootWorldBound = rootVisualElement.worldBound;

        float normalizedX = clickPosition.x / rootWorldBound.xMax;

        // Flip the y value so y = 0 is at the bottom of the screen
        float normalizedY = 1 - clickPosition.y / rootWorldBound.yMax;

        return new Vector2(normalizedX, normalizedY);

    }

    // Aligns a VisualElement's position with a specified world position.
    public static void MoveToWorldPosition(this VisualElement element, Vector3 worldPosition, Vector2 worldSize)
    {
        var rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, worldSize, Camera.main);
        element.transform.position = rect.position;
    }

    // Keeps a VisualElement within the camera viewport
    public static void ClampToScreenBounds(this VisualElement element, Camera camera = null)
    {
        camera ??= Camera.main;
        if (camera == null || element == null)
            return;

        // Calculate bounding rectangle for the entire hierarchy
        var boundingRect = new Rect(element.worldBound.position, element.worldBound.size);

        //// Extend the boundaries for any child elements
        foreach (var child in element.Children())
        {
            var childRect = child.worldBound;
            boundingRect.xMin = Mathf.Min(boundingRect.xMin, childRect.xMin);
            boundingRect.xMax = Mathf.Max(boundingRect.xMax, childRect.xMax);
            boundingRect.yMin = Mathf.Min(boundingRect.yMin, childRect.yMin);
            boundingRect.yMax = Mathf.Max(boundingRect.yMax, childRect.yMax);
        }

        var viewportPosition = camera.WorldToViewportPoint(boundingRect.center);

        // Clamp to screen space, considering the bounding rectangle dimensions
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, boundingRect.width / 2 / Screen.width, 1 - boundingRect.width / 2 / Screen.width);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, boundingRect.height / 2 / Screen.height, 1 - boundingRect.height / 2 / Screen.height);

        // Convert back to world position and set
        var newWorldPosition = camera.ViewportToWorldPoint(viewportPosition);
       
        var offset = newWorldPosition - new Vector3(boundingRect.center.x, boundingRect.center.y, newWorldPosition.z);

        element.transform.position += offset;
    }


}