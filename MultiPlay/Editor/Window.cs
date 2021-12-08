using System;
using System.Runtime.InteropServices;

internal class Window
{
	[DllImport("user32.dll", SetLastError = true)]
	private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
	[DllImport("user32.dll")]
	private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
	[DllImport("user32.dll", SetLastError = true)]
	private static extern bool DestroyWindow(IntPtr hWnd);

	private readonly IntPtr intPtr;

	internal Window(IntPtr intPtr) => this.intPtr = intPtr;

	internal void Move(int posX, int posY, int width, int height) => MoveWindow(intPtr, posX, posY, width, height, true);

	private int ProcessId()
	{
		GetWindowThreadProcessId(intPtr, out int lpdwProcessId);
		return lpdwProcessId;
	}

	internal bool IsFromProcess(int processId) => ProcessId() == processId;

	internal void Destroy() => DestroyWindow(intPtr);
}