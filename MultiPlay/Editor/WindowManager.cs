using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

internal class WindowManager
{
	private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
	[DllImport("user32.dll")]
	private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

	internal List<Window> GetWindows()
	{
		List<Window> windows = new List<Window>();
		EnumWindows((IntPtr wnd, IntPtr param) =>
		{
			windows.Add(new Window(wnd));
			return true;
		}, IntPtr.Zero);

		return windows;
	}

	internal void AdjustWindows(List<Window> windows)
	{
		if (windows.Count == 2)
		{
			int width = Screen.currentResolution.width / 2;
			int height = Screen.currentResolution.height;
			windows[0].Move(0, 0, width, height);
			windows[1].Move(width, 0, width, height);
		}
		else if (windows.Count == 3)
		{
			int width = Screen.currentResolution.width / 3;
			int height = Screen.currentResolution.height;
			windows[0].Move(0, 0, width, height);
			windows[1].Move(width, 0, width, height);
			windows[2].Move(width * 2, 0, width, height);
		}
		else if (windows.Count == 4)
		{
			int width = Screen.currentResolution.width / 2;
			int height = Screen.currentResolution.height / 2;
			windows[0].Move(0, 0, width, height);
			windows[1].Move(width, 0, width, height);
			windows[2].Move(0, height, width, height);
			windows[3].Move(width, height, width, height);
		}
	}
}