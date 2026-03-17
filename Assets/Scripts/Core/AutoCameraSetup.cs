using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Fixes magenta/pink screen by ensuring URP pipeline asset exists,
/// and creates a camera if none exists in the scene.
/// </summary>
public static class AutoCameraSetup
{
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void EnsureRenderingWorks()
        {
                    // === FIX 1: Ensure URP Pipeline Asset exists ===
                    if (GraphicsSettings.currentRenderPipeline == null)
                    {
                                    Debug.Log("[AutoCameraSetup] No render pipeline asset found. Creating URP asset.");
                                    var urpAsset = ScriptableObject.CreateInstance<UniversalRenderPipelineAsset>();
                                    GraphicsSettings.defaultRenderPipeline = urpAsset;
                                    QualitySettings.renderPipeline = urpAsset;
                                    Debug.Log("[AutoCameraSetup] URP pipeline asset created and assigned.");
                    }
                    else
                    {
                                    Debug.Log("[AutoCameraSetup] Render pipeline OK: " + GraphicsSettings.currentRenderPipeline.name);
                    }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void EnsureCameraExists()
        {
                    // === FIX 2: Ensure a Camera exists ===
                    if (Camera.main != null) return;

                    Camera[] allCameras = Object.FindObjectsByType<Camera>(FindObjectsSortMode.None);
                    if (allCameras.Length > 0) return;

                    Debug.Log("[AutoCameraSetup] No camera found. Creating default camera.");

                    GameObject camObj = new GameObject("Main Camera");
                    camObj.tag = "MainCamera";

                    Camera cam = camObj.AddComponent<Camera>();
                    cam.clearFlags = CameraClearFlags.SolidColor;
                    cam.backgroundColor = new Color(0.1f, 0.1f, 0.15f);
                    cam.orthographic = false;
                    cam.fieldOfView = 60f;
                    cam.nearClipPlane = 0.1f;
                    cam.farClipPlane = 1000f;

                    // Add URP camera data for proper rendering
                    camObj.AddComponent<UniversalAdditionalCameraData>();

                    Debug.Log("[AutoCameraSetup] Camera created with URP support.");
        }
}
