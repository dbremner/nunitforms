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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace NUnit.Extensions.Forms
{
	internal class KeyboardControllerWin32 : IDisposable
	{
		//TODO: should make sure caps lock is off and return it to its pretest state

		private bool restoreUserInput = false;
		private readonly Dictionary<Win32Key, Win32Key> keysHeldDown = new Dictionary<Win32Key, Win32Key>();
		private Win32.KEYBDINPUT keyboardInput = new Win32.KEYBDINPUT();

		public KeyboardControllerWin32()
		{
			keyboardInput.type = Win32.INPUT_KEYBOARD;
			keyboardInput.dwExtraInfo = Win32.GetMessageExtraInfo();
			keyboardInput.time = 0;
			keyboardInput.wScan = 0;
		}

		public void BlockUserInput()
		{
			if (!restoreUserInput)
			{
				restoreUserInput = Win32.BlockInput(true);
			}
		}

		public void PressAndRelease(params Win32Key[] keys)
		{
			SendKeys(keys, Win32.KEYEVENTF_KEYDOWN);
			Array.Reverse(keys);
			SendKeys(keys, Win32.KEYEVENTF_KEYUP);
		}

		private void SendKeys(IEnumerable<Win32Key> keys, int flags)
		{
			foreach (Win32Key key in keys)
			{
				SendKeyboardInput(key, flags);
			}
		}

		private void SendKeyboardInput(Win32Key key, int flags)
		{
			keyboardInput.dwFlags = flags;
			keyboardInput.wVk = (short)key;

			if (Win32.SendKeyboardInput(1, ref keyboardInput, Marshal.SizeOf(keyboardInput)) == 0)
			{
				throw new Win32Exception();
			}

			if (flags == Win32.KEYEVENTF_KEYDOWN)
			{
				keysHeldDown.Add(key, key);
			}

			if (flags == Win32.KEYEVENTF_KEYUP)
			{
				keysHeldDown.Remove(key);
			}

			Application.DoEvents();
		}

		private void ReleaseAllKeys()
		{
			foreach (Win32Key key in keysHeldDown.Keys)
			{
				SendKeyboardInput(key, Win32.KEYEVENTF_KEYUP);
			}
			keysHeldDown.Clear();
		}

		public void Dispose()
		{
			try
			{
				ReleaseAllKeys();
			}
			finally
			{
				if (restoreUserInput)
				{
					Win32.BlockInput(false);
					restoreUserInput = false;
				}
			}
		}
	}
}