#region Copyright (c) 2003-2005, Luke T. Maxon

/********************************************************************************************************************
'
' Copyright (c) 2003-2005, Luke T. Maxon
' All rights reserved.
' 
' Redistribution and use in source and binary forms, with or without modification, are permitted provided
' that the following conditions are met:
' 
' * Redistributions of source code must retain the above copyright notice, this list of conditions and the
' 	following disclaimer.
' 
' * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
' 	the following disclaimer in the documentation and/or other materials provided with the distribution.
' 
' * Neither the name of the author nor the names of its contributors may be used to endorse or 
' 	promote products derived from this software without specific prior written permission.
' 
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
' WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
' PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
' ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
' LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
' INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
' OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
' IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'
'*******************************************************************************************************************/

#endregion

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NUnit.Extensions.Forms
{
	internal class Win32
	{
        #region Delegates

        public delegate bool EnumDesktopsDelegate(string desktop, IntPtr lParam);

        public delegate bool EnumThreadDelegate(IntPtr hwnd, IntPtr lParam);

        #endregion

        internal const int BM_CLICK = 0x00F5; //Button
        public const int GENERIC_ALL = 0x10000000;
        internal const int INPUT_KEYBOARD = 1;
        internal const int INPUT_MOUSE = 0;
        internal const int KEYEVENTF_KEYUP = 0x0002;
        internal const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        internal const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        internal const int MOUSEEVENTF_LEFTUP = 0x0004;
        internal const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        internal const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        internal const int MOUSEEVENTF_MOVE = 0x0001;
        internal const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        internal const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //internal const int MOUSEEVENTF_WHEEL = 0x80;
        internal const int MOUSEEVENTF_WHEEL = 0x0800;
        //internal const int MOUSEEVENTF_XDOWN = 0x100;
        internal const int MOUSEEVENTF_XDOWN = 0x0080;
        internal const int MOUSEEVENTF_XUP = 0x0100;
        internal const int SPI_GETMOUSEHOVERTIME = 102;
        internal const short VK_CONTROL = 0x11;
        internal const short VK_MENU = 0x12;
        internal const short VK_SHIFT = 0x10;
        internal const int WHEEL_DELTA = 120;
        private const UInt32 WM_CLOSE = 0x0010;
        internal const int XBUTTON1 = 0x1;
        //internal const int XBUTTON1 = 8388608;
        internal const int XBUTTON2 = 0x2; //16777216

	    public const uint WM_KEYDOWN = 0x0100;
	    public const uint WM_KEYUP = 0x0101;
	    public const uint WM_SYSKEYDOWN = 0x104;
	    public const uint WM_SYSKEYUP = 0x105;

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr OpenDesktop(string lpszDesktop, uint dwFlags, bool fInherit, uint dwDesiredAccess);


		public static bool EnumDesktopsCallback(string desktop, IntPtr lParam)
		{
			return lParam != IntPtr.Zero;
		}

		[DllImport("user32.dll")]
		public static extern bool EnumDesktops(IntPtr hwinsta, EnumDesktopsDelegate lpEnumFunc, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetProcessWindowStation();

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr OpenWindowStation(string lpszWinSta, bool fInherit, uint dwDesiredAccess);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr CreateWindowStation(string pwinsta, uint dwReserved, uint dwDesiredAccess,
		                                                IntPtr lpsa);

		[DllImport("user32.dll")]
		public static extern bool SetProcessWindowStation(IntPtr hWinSta);

		[DllImport("user32.dll")]
		public static extern bool CloseWindowStation(IntPtr hWinSta);

		public static bool EnumThreadCallback(IntPtr hWnd, IntPtr lParam)
		{
			// Close the enumerated window.
			return PostMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
		}

		[DllImport("user32.dll", SetLastError = true)]
		[return : MarshalAs(UnmanagedType.Bool)]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern bool EnumThreadWindows(uint dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);


		[DllImport("kernel32")]
		[return : MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean CloseHandle(IntPtr handle);

		[DllImport("user32.dll")]
		internal static extern IntPtr GetDesktopWindow();

		[DllImport("user32.dll")]
		internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

		internal static bool EnumWindowsProc(IntPtr hWnd, int lParam)
		{
			//StringBuilder title = new StringBuilder(255);
			//int titleLength = GetWindowText(hWnd, title, title.Capacity + 1);
			//title.Length = titleLength;

			return true;
		}

		[DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", ExactSpelling = false, CharSet = CharSet.Auto,
			SetLastError = true)]
		internal static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction,
		                                               IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetWindowText(IntPtr handleToWindow, StringBuilder windowText, int maxTextLength);

		[DllImport("user32.dll")]
		internal static extern IntPtr GetDlgItem(IntPtr handleToWindow, int ControlId);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetClassName(IntPtr handleToWindow, StringBuilder className, int maxClassNameLength);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr SetWindowsHookEx(int code, CBTCallback callbackFunction, IntPtr handleToInstance,
                                                       int threadID);
        [DllImport("user32.dll", EntryPoint="SetWindowsHookEx")]
        internal static extern IntPtr SetMSGWindowsHookEx(int code, MSGCallback callbackFunction, IntPtr handleToInstance,
                                                       int threadID);

		[DllImport("user32.dll")]
		internal static extern bool UnhookWindowsHookEx(IntPtr handleToHook);

		[DllImport("user32.dll")]
		internal static extern IntPtr CallNextHookEx(IntPtr handleToHook, int nCode, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll", EntryPoint="CallNextHookEx")]
		internal static extern IntPtr CallNextMSGHookEx(IntPtr handleToHook, int nCode, IntPtr wParam, ref System.Windows.Forms.Message lParam);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr SendMessage(IntPtr handleToWindow, uint Message, UIntPtr wParam, IntPtr lParam);

		[DllImport("user32.Dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr SendDlgItemMessage(IntPtr handleToWindow, int dlgItem, uint message,
		                                                 UIntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		internal static extern int GetCursorPos(out Point lpWinPoint);

		[DllImport("user32.dll")]
		internal static extern bool ShowCursor(bool bShow);

		[DllImport("user32.dll")]
		internal static extern int SetCursorPos(int x, int y);

		[DllImport("user32.dll")]
		internal static extern int GetSystemMetrics(int nIndex);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int SystemParametersInfo(int uAction, int uParam, out int lpvParam, int fuWinIni);

		/// <summary>
		/// Specifies the function's purpose. If this parameter is TRUE, keyboard and mouse input events are 
		/// blocked. If this parameter is FALSE, keyboard and mouse events are unblocked. Note that only 
		/// the thread that blocked input can successfully unblock input. 
		/// </summary>
		/// <param name="blockIt"></param>
		/// <returns></returns>
		/// <remarks>
		/// When input is blocked, real physical input from the mouse or keyboard will not affect the input queue's 
		/// synchronous key state (reported by GetKeyState and GetKeyboardState), nor will it affect the asynchronous 
		/// key state (reported by GetAsyncKeyState). However, the thread that is blocking input can affect both of 
		/// these key states by calling SendInput. No other thread can do this.
		/// The system will unblock input in the following cases: 
		/// The thread that blocked input unexpectedly exits without calling BlockUserInput with fBlock set to FALSE. 
		/// In this case, the system cleans up properly and re-enables input. Windows 95/98/Me: The system displays the
		/// Close Program/Fault dialog box. This can occur if the thread faults or if the user presses CTRL+ALT+DEL. 
		/// Windows 2000/XP: The user presses CTRL+ALT+DEL or the system invokes the Hard System Error modal message
		/// box (for example, when a program faults or a device fails). 
		/// </remarks>
		[DllImport("user32.dll", SetLastError=true)]
		internal static extern bool BlockInput(bool blockIt);

		[DllImport("user32.dll", EntryPoint="SendInput", SetLastError=true)]
		internal static extern int SendMouseInput(int cInputs, ref MOUSEINPUT pInputs, int cbSize);

		[DllImport("user32.dll", EntryPoint="SendInput", SetLastError=true)]
		internal static extern int SendKeyboardInput(int cInputs, ref KEYBDINPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateDesktop(string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, int dwFlags,
                                                  int dwDesiredAccess, IntPtr lpsa);

        [DllImport("user32", SetLastError = true)]
        public static extern int CloseDesktop(IntPtr hDesktop);
		internal const int KEYEVENTF_KEYDOWN = 0x0000;

        [DllImport("user32", SetLastError = true)]
        public static extern IntPtr GetThreadDesktop(int dwThreadId);

        [DllImport("user32", SetLastError = true)]
        public static extern int SetThreadDesktop(IntPtr hDesktop);

        [DllImport("user32", SetLastError = true)]
        public static extern int SwitchDesktop(IntPtr hDesktop);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool SetDlgItemText(IntPtr hDlg, int nIDDlgItem, string lpString);
        
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool SetWindowText(IntPtr hWnd, string lpString);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetDlgItemText(IntPtr hDlg, int nIDDlgItem, StringBuilder lpString, int maxCount);

        [DllImport("user32.dll")]
        internal static extern bool IsWindowVisible(IntPtr hDlg);

        [DllImport("kernel32", SetLastError = true)]
        public static extern int GetCurrentThreadId();

        #region Nested type: CBTCallback

        internal delegate IntPtr CBTCallback(int code, IntPtr wParam, IntPtr lParam);
        internal delegate IntPtr MSGCallback(int code, IntPtr wParam, ref System.Windows.Forms.Message lParam);

        #endregion

        #region Nested type: EnumDelegate

        internal delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        #endregion

        #region Nested type: KEYBDINPUT

		/// <summary>
		/// Another example can be found on http://www.pinvoke.net/default.aspx/user32/SendInput.html. Notice
		/// that KEYBDINPUT on pinvoke.net is split in two structs : INPUT and KEYBDINPUT. The KEYBDINPUT that is
		/// used here, contains both structs of pinvoke.net.
		/// </summary>
		[StructLayout(LayoutKind.Explicit, Size=28)]
		internal struct KEYBDINPUT
		{
            [FieldOffset(16)] internal IntPtr dwExtraInfo;
            [FieldOffset(8)] internal int dwFlags;

            [FieldOffset(12)] internal int time;
			[FieldOffset(0)] internal int type;
            [FieldOffset(6)] internal short wScan;
			[FieldOffset(4)] internal short wVk;
        }

        #endregion

        #region Nested type: MOUSEINPUT

        internal struct MOUSEINPUT
        {
            internal int dwExtraInfo;
            internal int dwFlags;
            internal int dx;

            internal int dy;

            internal int mouseData;

            internal int time;
            internal int type;

			///<summary>
			/// 
			/// </summary>
			/// <remarks>
			///dx
			///Specifies the absolute position of the mouse, or the amount of motion since the last mouse event was generated, depending on the value of the dwFlags member. Absolute data is specified as the x coordinate of the mouse; relative data is specified as the number of pixels moved. 
			///dy
			///Specifies the absolute position of the mouse, or the amount of motion since the last mouse event was generated, depending on the value of the dwFlags member. Absolute data is specified as the y coordinate of the mouse; relative data is specified as the number of pixels moved. 
			///mouseData
			///If dwFlags contains MOUSEEVENTF_WHEEL, then mouseData specifies the amount of wheel movement. A positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user. One wheel click is defined as WHEEL_DELTA, which is 120. 
			///Windows 2000/XP: IfdwFlags does not contain MOUSEEVENTF_WHEEL, MOUSEEVENTF_XDOWN, or MOUSEEVENTF_XUP, then mouseData should be zero. 
			///
			///If dwFlags contains MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP, then mouseData specifies which X buttons were pressed or released. This value may be any combination of the following flags. 
			///
			///XBUTTON1
			///Set if the first X button is pressed or released.
			///XBUTTON2
			///Set if the second X button is pressed or released.
			///dwFlags
			///A set of bit flags that specify various aspects of mouse motion and button clicks. The bits in this member can be any reasonable combination of the following values. 
			///The bit flags that specify mouse button status are set to indicate changes in status, not ongoing conditions. For example, if the left mouse button is pressed and held down, MOUSEEVENTF_LEFTDOWN is set when the left button is first pressed, but not for subsequent motions. Similarly, MOUSEEVENTF_LEFTUP is set only when the button is first released. 
			///
			///You cannot specify both the MOUSEEVENTF_WHEEL flag and either MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP flags simultaneously in the dwFlags parameter, because they both require use of the mouseData field. 
			///
			///MOUSEEVENTF_ABSOLUTE
			///Specifies that the dx and dy members contain normalized absolute coordinates. If the flag is not set, dxand dy contain relative data (the change in position since the last reported position). This flag can be set, or not set, regardless of what kind of mouse or other pointing device, if any, is connected to the system. For further information about relative mouse motion, see the following Remarks section.
			///MOUSEEVENTF_MOVE
			///Specifies that movement occurred.
			///MOUSEEVENTF_LEFTDOWN
			///Specifies that the left button was pressed.
			///MOUSEEVENTF_LEFTUP
			///Specifies that the left button was released.
			///MOUSEEVENTF_RIGHTDOWN
			///Specifies that the right button was pressed.
			///MOUSEEVENTF_RIGHTUP
			///Specifies that the right button was released.
			///MOUSEEVENTF_MIDDLEDOWN
			///Specifies that the middle button was pressed.
			///MOUSEEVENTF_MIDDLEUP
			///Specifies that the middle button was released.
			///MOUSEEVENTF_VIRTUALDESK
			///Windows 2000/XP:Maps coordinates to the entire desktop. Must be used with MOUSEEVENTF_ABSOLUTE.
			///MOUSEEVENTF_WHEEL
			///Windows NT/2000/XP: Specifies that the wheel was moved, if the mouse has a wheel. The amount of movement is specified in mouseDataÂ� .
			///MOUSEEVENTF_XDOWN
			///Windows 2000/XP: Specifies that an X button was pressed.
			///MOUSEEVENTF_XUP
			///Windows 2000/XP: Specifies that an X button was released.
			///time
			///Time stamp for the event, in milliseconds. If this parameter is 0, the system will provide its own time stamp. 
			///dwExtraInfo
			///</remarks>
			public MOUSEINPUT(int mouseEvent)
			{
				type = INPUT_MOUSE;
				dx = 0;
				dy = 0;
				mouseData = 0;
				dwFlags = mouseEvent;
				time = 0;
				dwExtraInfo = 0;
			}
        } ;

        #endregion

        #region Nested type: Point

        internal struct Point
        {
            internal int x;

            internal int y;

			public Point(int x, int y)
			{
				this.x = x;
				this.y = y;
			}
		}

        #endregion

        #region Nested type: WindowEnumProc

        internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);

        #endregion

        #region Nested type: WindowMessages

        internal enum WindowMessages : uint
        {
            WM_CLOSE = 0x0010,
            WM_COMMAND = 0x0111
        }

        #endregion

        [DllImport("user32.dll")]
        public static extern short VkKeyScanEx(char ch, IntPtr dwhkl);

        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardLayout(int idThread);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)] //
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);

        [DllImport("user32.dll", EntryPoint = "PostMessage")] //
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, uint lParam);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        public static extern bool SetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostThreadMessage(uint idThread, uint Msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(int idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow")] //
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)] //
        public static extern int RegisterWindowMessage(string lpstring);


	}
}
