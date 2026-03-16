using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

/// <summary>
/// Pre-build script that forces Android App Bundle (AAB) output
/// for Unity Cloud Build. This ensures Play Console compatibility.
/// </summary>
public class CloudBuildHelper : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
{
          if (report.summary.platform == BuildTarget.Android)
{
            EditorUserBuildSettings.buildAppBundle = true;
            UnityEngine.Debug.Log("[CloudBuildHelper] Set buildAppBundle = true for AAB output");
}
}
}
