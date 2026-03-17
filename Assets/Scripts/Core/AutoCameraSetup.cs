using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.EventSystems;

/// <summary>
/// Runtime bootstrap: ensures Camera, GameManager, EventSystem exist.
/// URP pipeline is handled by GraphicsSettings (project-level).
/// </summary>
public static class AutoCameraSetup
{
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void EnsureEssentialObjectsExist()
        {
                    EnsureCameraExists();
                    EnsureGameManagerExists();
                    EnsureEventSystemExists();
        }

        static void EnsureCameraExists()
        {
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
                    var urpData = camObj.AddComponent<UniversalAdditionalCameraData>();
                    urpData.renderType = CameraRenderType.Base;
        }

        static void EnsureGameManagerExists()
        {
                    if (GameManager.Instance != null) return;
                    if (Object.FindFirstObjectByType<GameManager>() != null) return;

                    Debug.Log("[AutoCameraSetup] No GameManager found. Creating one.");
                    GameObject gmObj = new GameObject("GameManager");
                    gmObj.AddComponent<GameManager>();
                    Object.DontDestroyOnLoad(gmObj);
        }

        static void EnsureEventSystemExists()
        {
                    if (Object.FindFirstObjectByType<EventSystem>() != null) return;

                    Debug.Log("[AutoCameraSetup] No EventSystem found. Creating one.");
                    GameObject esObj = new GameObject("EventSystem");
                    esObj.AddComponent<EventSystem>();
                    esObj.AddComponent<StandaloneInputModule>();
        }
}
