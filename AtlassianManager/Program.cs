using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AtlassianManager
{
	internal static class Program
	{
		[DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		private static extern Boolean AllocConsole();

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr CreateFile(
			string lpFileName,
			uint dwDesiredAccess,
			uint dwShareMode,
			uint lpSecurityAttributes,
			uint dwCreationDisposition,
			uint dwFlagsAndAttributes,
			uint hTemplateFile);

		private const int MY_CODE_PAGE = 437;
		private const uint GENERIC_WRITE = 0x40000000;
		private const uint FILE_SHARE_WRITE = 0x2;
		private const uint OPEN_EXISTING = 0x3;

		[DllImport("User32.dll")]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("User32.dll")]
		private static extern IntPtr SetForegroundWindow(IntPtr hWnd);

		[DllImport("User32.dll")]
		private static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);

		private const int SW_SHOWNORMAL = 1;

		/// <summary>
		/// 해당 응용 프로그램의 주 진입점입니다.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			ShowConsoleWindow();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain());
		}

		private static void ShowConsoleWindow()
		{
			bool OpenConsole = false;
#if DEBUG
			OpenConsole = true;
#else
            if (File.Exists(Application.StartupPath + @"\CTest.dat"))
                OpenConsole = true;
#endif
			if (OpenConsole)
			{
				if (!AllocConsole())
					MessageBox.Show("Console Window Load Failed");
				else
				{
					IntPtr stdHandle = CreateFile("CONOUT$", GENERIC_WRITE, FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0);
					SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
					FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
					Encoding encoding = System.Text.Encoding.GetEncoding(MY_CODE_PAGE);
					StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
					standardOutput.AutoFlush = true;
					Console.SetOut(standardOutput);
					Console.Write("This will show up in the Console window.");
				}
			}
		}
	}
}