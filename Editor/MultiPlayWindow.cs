using UnityEditor;
using UnityEngine;

internal class MultiPlayWindow : EditorWindow
{
    [MenuItem("Window/MultiPlay")]
    private static void ShowWindow()
    {
        EditorWindow editorWindow = GetWindow(typeof(MultiPlayWindow));
        Texture2D playButtonTexture = EditorGUIUtility.IconContent("PlayButton").image as Texture2D;
        editorWindow.titleContent = new GUIContent("MultiPlay", playButtonTexture, "MultiPlay");
    }

    private void OnGUI()
    {
        // Styles

        GUIStyle titleStyle = GUI.skin.GetStyle("label");
        titleStyle.padding = new RectOffset(8, 8, 8, 0);
        titleStyle.fontSize = 18;
        titleStyle.fontStyle = FontStyle.Bold;

        GUIStyle subtitleStyle = GUI.skin.GetStyle("label");
        titleStyle.padding = new RectOffset(8, 8, 8, 0);
        titleStyle.fontSize = 14;
        titleStyle.fontStyle = FontStyle.Bold;

        // ========== MultiPlay ==========

        GUILayout.Label("MultiPlay", titleStyle);
        GUILayout.Space(10f);

        GUIStyle inputsStyle = GUI.skin.GetStyle("label");
        inputsStyle.padding = new RectOffset(20, 50, 0, 0);
        GUILayout.BeginVertical(inputsStyle);
        MultiPlay.WindowsCount.Set(EditorGUILayout.IntField("Windows count", MultiPlay.WindowsCount.Get()));
        MultiPlay.PlayInEditor.Set(EditorGUILayout.Toggle("Play in editor", MultiPlay.PlayInEditor.Get()));
        MultiPlay.PlayFromEditor.Set(EditorGUILayout.Toggle("Play from editor", MultiPlay.PlayFromEditor.Get()));
        MultiPlay.ForceWindowMode.Set(EditorGUILayout.Toggle("Force window mode", MultiPlay.ForceWindowMode.Get()));
        GUILayout.EndVertical();

        GUILayout.Space(20f);

        GUIStyle buttonBoxStyle = GUI.skin.GetStyle("label");
        buttonBoxStyle.padding = new RectOffset(10, 10, 0, 0);
        buttonBoxStyle.fixedWidth = 0;
        GUILayout.BeginHorizontal(buttonBoxStyle);
        if (GUILayout.Button("Build"))
            MultiPlay.Build();
        if (GUILayout.Button("Build & Play"))
            MultiPlay.BuildAndPlay();
        else if (GUILayout.Button("Play"))
            MultiPlay.Play();
        else if (GUILayout.Button("Adjust windows"))
            MultiPlay.AdjustWindows();
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        // ========== Folders ==========

        GUILayout.Label("Folders", subtitleStyle);
        GUILayout.Space(10f);

        GUILayout.BeginHorizontal(buttonBoxStyle);
        if (GUILayout.Button("Open Assets Folder"))
            EditorUtility.RevealInFinder(Application.dataPath);
        if (GUILayout.Button("Open Build Folder"))
            EditorUtility.RevealInFinder(Builder.BuildPath());
        if (GUILayout.Button("Open Persistence Data Folder"))
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        GUILayout.EndHorizontal();
    }
}
