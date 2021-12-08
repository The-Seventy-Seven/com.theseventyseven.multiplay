using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

internal class Builder
{
    internal static string BuildPath() => GetBuildPlayerOptions().locationPathName;

    internal static void Build() => Build(() => { });
    internal static void Build(Action onSucceeded)
    {
        BuildPlayerOptions buildOptions = GetBuildPlayerOptions();
        BuildReport report = BuildPipeline.BuildPlayer(buildOptions.scenes, buildOptions.locationPathName,
            BuildTarget.StandaloneWindows, BuildOptions.UncompressedAssetBundle | BuildOptions.AllowDebugging | BuildOptions.Development);
        switch (report.summary.result)
        {
            case BuildResult.Failed:
            case BuildResult.Unknown:
                Debug.LogError("[MultiPlay] Build error!");
                break;

            case BuildResult.Cancelled:
                Debug.LogWarning("[MultiPlay] Compilation cancelled!");
                break;

            case BuildResult.Succeeded:
                Debug.Log("[MultiPlay] Compilation succeeded (" + report.summary.totalSize + " bytes)");
                onSucceeded();
                break;
        }
    }

    private static BuildPlayerOptions GetBuildPlayerOptions(bool askForLocation = false, BuildPlayerOptions defaultOptions = new BuildPlayerOptions())
    {
        MethodInfo method = typeof(BuildPlayerWindow.DefaultBuildMethods).GetMethod("GetBuildPlayerOptionsInternal", BindingFlags.NonPublic | BindingFlags.Static);
        return (BuildPlayerOptions)method.Invoke(null, new object[] { askForLocation, defaultOptions });
    }
}
