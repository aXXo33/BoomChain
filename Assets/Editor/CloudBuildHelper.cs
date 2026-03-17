using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CloudBuildHelper : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        if (report.summary.platform == BuildTarget.Android)
        {
            EditorUserBuildSettings.buildAppBundle = true;
            Debug.Log("[CloudBuildHelper] Set buildAppBundle = true for AAB output");
        }
        EnsureURPPipelineAsset();
    }

    private static void EnsureURPPipelineAsset()
    {
        var currentRP = GraphicsSettings.defaultRenderPipeline;
        if (currentRP != null && currentRP is UniversalRenderPipelineAsset)
        {
            Debug.Log("[CloudBuildHelper] URP Pipeline Asset already assigned: " + currentRP.name);
            return;
        }
        Debug.Log("[CloudBuildHelper] No valid URP Pipeline Asset found. Creating one...");
        if (!AssetDatabase.IsValidFolder("Assets/Settings"))
            AssetDatabase.CreateFolder("Assets", "Settings");
        var rendererData = ScriptableObject.CreateInstance<UniversalRendererData>();
        AssetDatabase.CreateAsset(rendererData, "Assets/Settings/URP_Renderer.asset");
        var urpAsset = UniversalRenderPipelineAsset.Create(rendererData);
        AssetDatabase.CreateAsset(urpAsset, "Assets/Settings/URP_PipelineAsset.asset");
        GraphicsSettings.defaultRenderPipeline = urpAsset;
        QualitySettings.renderPipeline = urpAsset;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("[CloudBuildHelper] URP Pipeline setup complete!");
    }
}
