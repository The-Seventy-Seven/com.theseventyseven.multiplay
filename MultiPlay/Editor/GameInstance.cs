using System.Diagnostics;

public class GameInstance
{
    private readonly Process process = new Process();
    private Window window;

    internal void Start(string path)
    {
        process.StartInfo.FileName = path;
        process.EnableRaisingEvents = false;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        if (MultiPlay.ForceWindowMode.Get())
            process.StartInfo.Arguments = "-screen-fullscreen 0";
        process.Start();
    }

    internal bool IsRunning() => !process.HasExited;

    internal bool TryGetWindow()
    {
        if (window != null)
            return true;
        window = new WindowManager().GetWindows().Find(window => window.IsFromProcess(process.Id));
        return window != null;
    }

    internal Window Window() => window;
}
