using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Automatically creates a Camera if none exists in the scene.
/// This runs before any other script via RuntimeInitializeOnLoadMethod.
/// </summary>
public static class AutoCameraSetup
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EnsureCameraExists()
{
        // Check if a camera already exists
        if (Camera.main != null) return;

        Camera[] allCameras = Object.FindObjectsByType<Camera>(FindObjectsSortMode.None);
        if (allCameras.Length > 0) return;

        Debug.Log("[AutoCameraSetup] No camera found in scene. Creating default camera.");

        // Create camera GameObject
        GameObject camObj = new GameObject("Main Camera");
        camObj.tag = "MainCamera";

        // Add Camera component
        Camera cam = camObj.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.1f, 0.1f, 0.15f);
        cam.orthographic = false;
        cam.fieldOfView = 60f;
        cam.nearClipPlane = 0.1f;
        cam.farClipPlane = 1000f;

        // Position the camera to see the scene
        camObj.transform.position = new Vector3(0f, 5f, -10f);
        camObj.transform.rotation = Quaternion.Euler(20f, 0f, 0f);

        // Add AudioListener
        camObj.AddComponent<AudioListener>();

        // Try to add URP camera data if URP is available
        try
{
            var urpCamDataType = System.Type.GetType("UnityEngine.Rendering.Universal.UniversalAdditionalCameraData, Unity.RenderPipelines.Universal.Runtime");
            if (urpCamDataType != null)
{
                camObj.AddComponent(urpCamDataType);
                Debug.Log("[AutoCameraSetup] Added URP camera data.");
}
}
        catch (System.Exception e)
{
            Debug.LogWarning("[AutoCameraSetup] Could not add URP camera data: " + e.Message);
}

        // Don't destroy on load so camera persists between scenes
        Object.DontDestroyOnLoad(camObj);

        Debug.Log("[AutoCameraSetup] Default camera created successfully.");
}
}
