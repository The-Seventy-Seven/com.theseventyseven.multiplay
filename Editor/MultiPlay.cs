using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using UnityEditor;

[Serializable]
internal class MultiPlay
{
    internal static readonly EditorPrefVarInt WindowsCount = new EditorPrefVarInt("MultiPlay_WindowsCount");
    internal static readonly EditorPrefVarBool PlayInEditor = new EditorPrefVarBool("MultiPlay_PlayInEditor");
    internal static readonly EditorPrefVarBool PlayFromEditor = new EditorPrefVarBool("MultiPlay_PlayFromEditor");
    internal static readonly EditorPrefVarBool ForceWindowMode = new EditorPrefVarBool("MultiPlay_ForceWindowMode");

    internal static bool PlayedFromButton;
    private static List<GameInstance> gameInstances = new List<GameInstance>();

    internal static void Play()
    {
        gameInstances = Enumerable.Range(1, WindowsCount.Get()).Select(_ => new GameInstance()).ToList();
        gameInstances.ForEach(gameInstance => gameInstance.Start(Builder.BuildPath()));
        EditorCoroutineUtility.StartCoroutineOwnerless(AdjustWindowsCoroutine());
    }

    internal static void Build() => Builder.Build();

    internal static void BuildAndPlay() => Builder.Build(PlayFromWindowButton);

    internal static void PlayFromWindowButton()
    {
        if (PlayInEditor.Get())
        {
            PlayedFromButton = true;
            EditorApplication.isPlaying = true;
        }
        Play();
    }

    private static IEnumerator AdjustWindowsCoroutine()
    {
        gameInstances = gameInstances.FindAll(game => game.IsRunning());
        while (!gameInstances.FindAll(game => game.IsRunning()).All(gameInstance => gameInstance.TryGetWindow()))
            yield return null;
        yield return new EditorWaitForSeconds(5);
        AdjustWindows();
    }

    private static List<Window> GameInstanceWindows() => gameInstances.Select(gameInstance => gameInstance.Window()).ToList();
    internal static void AdjustWindows() => new WindowManager().AdjustWindows(GameInstanceWindows());
}
