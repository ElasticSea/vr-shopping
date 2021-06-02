#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class BuildUtils
{
    [MenuItem("Build/Windows64")]
    public static void BuildWin64()
    {
        Build(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, $"{Application.productName}.exe");
    }

    private static void Build(BuildTargetGroup buildTargetGroup, BuildTarget buildTarget, string filename)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);

        var scenes = from scene in EditorBuildSettings.scenes
                     where scene.enabled
                     select scene.path;

        var directory = new DirectoryInfo($"Build/{buildTarget.ToString().ToLowerInvariant()}/{Application.productName}");
        var buildPath = filename == null ? directory.FullName : Path.Combine(directory.FullName, filename);

        BuildPipeline.BuildPlayer(scenes.ToArray(),buildPath, buildTarget, BuildOptions.None);

        switch (buildTarget)
        {
            case BuildTarget.StandaloneWindows64:
                var unityCrashHandlerFile = directory.EnumerateFiles("*.exe", SearchOption.AllDirectories)
                    .FirstOrDefault(f => f.FullName.ToLower().Contains("unitycrashhandler"));

                unityCrashHandlerFile?.Delete();
                break;
        }

        Application.OpenURL($"file://{directory.FullName}");
    }
}
#endif