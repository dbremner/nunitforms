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
using System.Collections;
using System.Reflection;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms
{
	/// <summary>
	/// Used to specify a handler for a Modal form that is displayed during testing.
	/// </summary>
	public delegate void ModalFormActivated();

	internal delegate void ModalFormActivatedHwnd(IntPtr hWnd);

	///<summary>
	/// A class for testing Modal Forms.
	///</summary>
	public class ModalFormTester : IDisposable
	{
		/// <summary>
		/// This class encapsulates a event handler
		/// along with information on whether it was
		/// expected to be called, and if it was actually called.
		/// </summary>
		private class Handler
		{
			private bool invoked = false;

			private readonly bool expected = false;
			private readonly Delegate handler = null;

			/// <summary>
			/// Constructs a new <see cref="Handler"/>.
			/// </summary>
			public Handler(Delegate handler, bool expected)
			{
				this.handler = handler;
				this.expected = expected;
			}

			/// <summary>
			/// Verify that this handler was either expected and invoked,
			/// or not expected and not invoked.
			/// </summary>
			public bool Verify()
			{
				return expected == invoked;
			}

			/// <summary>
			/// Invokes the wrapped event handler with the given window handle.
			/// </summary>
			/// <param name="hWnd"></param>
			public void Invoke(IntPtr hWnd)
			{
				invoked = true;
				try
				{
					if (handler is ModalFormActivated)
					{
						handler.DynamicInvoke(new object[] {});
					}
					else if (handler is ModalFormActivatedHwnd)
					{
						handler.DynamicInvoke(new object[] {hWnd});
					}
				}
				catch (TargetInvocationException ex)
				{
					// Unwrap any exceptions that happen in the Reflection layer.
					if (ex.InnerException != null)
					{
						throw ex.InnerException;
					}
				}
			}
		}

		/// <summary>
		/// The mapping of form titles to event handlers.
		/// </summary>
		private readonly Hashtable handlers = new Hashtable();

		/// <summary>
		/// A token representing "any form".
		/// </summary>
		public readonly string ANY = Guid.NewGuid().ToString();

		///<summary>
		/// Constructs a new <see cref="ModalFormTester"/>.
		///</summary>
		public ModalFormTester()
		{
			Add(ANY,
			    (ModalFormActivatedHwnd)
			    Delegate.CreateDelegate(typeof (ModalFormActivatedHwnd), this, "UnexpectedModal"), false);
		}

		///<summary>
		/// A <see cref="ModalFormActivatedHwnd"/> that tries to click the OK button of the modal form.
		///</summary>
		public void UnexpectedModal(IntPtr hWnd)
		{
			MessageBoxTester messageBox = new MessageBoxTester(hWnd);
			messageBox.ClickOk();
		}

		///<summary>
		/// Registers an expected handler for the given form caption.
		///</summary>
		///<param name="name">The caption of the form to handle.</param>
		///<param name="handler">The handler.</param>
		public void ExpectModal(string name, ModalFormActivated handler)
		{
			ExpectModal(name, handler, true);
		}

		///<summary>
		/// Registers an expected or unexpected handler for the given form caption.
		///</summary>
		///<param name="name">The caption of the form to handle.</param>
		///<param name="handler">The handler.</param>
		///<param name="expected">True if this handler is expected; false if this handler is not expected.</param>
		public void ExpectModal(string name, ModalFormActivated handler, bool expected)
		{
			BeginListening();
			handlers[name] = new Handler(handler, expected);
		}

		///<summary>
		/// Registers an expected or unexpected handler for the given form caption.
		///</summary>
		///<param name="name">The caption of the form to handle.</param>
		///<param name="handler">The handler.</param>
		///<param name="expected">True if this handler is expected; false if this handler is not expected.</param>
		internal void Add(string name, ModalFormActivatedHwnd handler, bool expected)
		{
			BeginListening();
			handlers[name] = new Handler(handler, expected);
		}

		~ModalFormTester()
		{
			Dispose();
		}

		/// <summary>
		/// Verifies that all expected handlers were invoked,
		/// and that no unexpected ones were.
		/// </summary>
		public bool Verify()
		{
			foreach (string name in handlers.Keys)
			{
				Handler h = (Handler) handlers[name];
				if (!h.Verify())
					return false;
			}

			return true;
		}

		private IntPtr handleToHook = IntPtr.Zero;

		private const int CbtHookType = 5;
		private const int HCBT_ACTIVATE = 5;

		/// <summary>
		/// True if we have begun listening for CBT Activate events.
		/// </summary>
		private bool listening = false;

		private Win32.CBTCallback callback = null;

		private void BeginListening()
		{
			if (!listening)
			{
				listening = true;
				// Note: the callback is saved as a member to keep the CLR from shuffling off the pointer
				// before the callback is used.
				// If we try to assign the call back "inline" we get memory violation errors.
				callback = Callback_ModalListener;
				handleToHook = Win32.SetWindowsHookEx(CbtHookType, callback, IntPtr.Zero, Win32.GetCurrentThreadId());
			}
		}

		///<summary>
		/// Disposes any resources being managed.
		///</summary>
		public void Dispose()
		{
			if (handleToHook != IntPtr.Zero)
			{
				Win32.UnhookWindowsHookEx(handleToHook);
				handleToHook = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		private void Invoke(string name, IntPtr hWnd)
		{
			if (name == null) return;

			Handler namedHandler = handlers[name] as Handler;
			if (namedHandler != null)
			{
				namedHandler.Invoke(hWnd);
				return;
			}

			Handler anyHandler = handlers[ANY] as Handler;
			if (anyHandler != null)
			{
				anyHandler.Invoke(hWnd);
				return;
			}
		}

		/// <summary>
		/// CBT callback called when a form is activated.
		/// If the newly activated form is modal and matches any registered names,
		/// invoke the appropriate hander.
		/// </summary>
		private IntPtr Callback_ModalListener(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code == HCBT_ACTIVATE)
			{
				FindWindowNameAndInvokeHandler(wParam);
			}

			return Win32.CallNextHookEx(handleToHook, code, wParam, lParam);
		}

		private void FindWindowNameAndInvokeHandler(IntPtr hwnd)
		{
			string name = null;

			Form form = Form.FromHandle(hwnd) as Form;
			if (form != null && form.Modal)
			{
				name = form.Name;
			}
			else if (WindowHandle.IsDialog(hwnd))
			{
				name = WindowHandle.GetCaption(hwnd);
				if (name == null)
				{
					name = string.Empty;
				}
			}

			Invoke(name, hwnd);
		}
	}
}