using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class MultiPlayStaticEditor
{
    static MultiPlayStaticEditor()
    {
        EditorApplication.playModeStateChanged += LogPlayModeState;
        UnityToolbarExtender.ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
    }

    private static void LogPlayModeState(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            if (MultiPlay.PlayedFromButton)
                MultiPlay.PlayedFromButton = false;
            else if (MultiPlay.PlayFromEditor.Get())
                MultiPlay.Play();
        }
    }

    private static void OnToolbarGUI()
    {
        GUIStyle buttonStyle = new GUIStyle("Command");

        GUIStyle buttonLeft = new GUIStyle(EditorStyles.toolbarButton)
        {
            stretchHeight = buttonStyle.stretchHeight,
            stretchWidth = buttonStyle.stretchWidth,
            fixedHeight = buttonStyle.fixedHeight,
            fixedWidth = buttonStyle.fixedWidth,
            border = EditorStyles.miniButtonLeft.border,
            overflow = EditorStyles.miniButtonLeft.overflow,
            padding = EditorStyles.miniButtonLeft.padding,
            margin = EditorStyles.miniButtonLeft.margin
        };

        GUIStyle buttonRight = new GUIStyle(EditorStyles.toolbarButton)
        {
            stretchHeight = buttonStyle.stretchHeight,
            stretchWidth = buttonStyle.stretchWidth,
            fixedHeight = buttonStyle.fixedHeight,
            fixedWidth = buttonStyle.fixedWidth,
            border = EditorStyles.miniButtonRight.border,
            overflow = EditorStyles.miniButtonRight.overflow,
            padding = EditorStyles.miniButtonRight.padding,
            margin = EditorStyles.miniButtonRight.margin
        };

        Texture2D playButtonTexture = EditorGUIUtility.IconContent("BuildSettings.N3DS").image as Texture2D;
        Texture2D buildAndPlayButtonTexture = EditorGUIUtility.IconContent("SettingsIcon").image as Texture2D;

        GUILayout.Space(20f);
        if (GUILayout.Button(new GUIContent(playButtonTexture, "MultiPlay"), buttonLeft))
            MultiPlay.Play();
        if (GUILayout.Button(new GUIContent(buildAndPlayButtonTexture, "Build and MultiPlay"), buttonRight))
            Builder.Build(MultiPlay.PlayFromWindowButton);
    }
}
