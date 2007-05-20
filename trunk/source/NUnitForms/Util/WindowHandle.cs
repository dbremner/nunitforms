using System;
using System.Text;

namespace NUnit.Extensions.Forms
{
	///<summary>
	/// Additional methods for working with Window Handles.
	///</summary>
	public static class WindowHandle
	{
		const string DialogClass = "#32770";

		///<summary>
		/// Returns true if the given window handle references a Dialog.
		///</summary>
		///<param name="handle"></param>
		///<returns></returns>
		public static bool IsDialog(IntPtr handle)
		{
			return GetClassName(handle) == DialogClass;
		}

		///<summary>
		/// Returns the text of the references control.
		///</summary>
		///<param name="handle">A window handle to a control.</param>
		///<returns>The text of the control.</returns>
		public static string GetText(IntPtr handle)
		{
			IntPtr handleToDialogText = Win32.GetDlgItem(handle, 0xFFFF);
			StringBuilder buffer = new StringBuilder(255);
			Win32.GetWindowText(handleToDialogText, buffer, 255);
			return buffer.ToString();
		}

		///<summary>
		/// Gets the caption of the referenced window.
		///</summary>
		public static string GetCaption(IntPtr handle)
		{
			StringBuilder buffer = new StringBuilder(255);
			Win32.GetWindowText(handle, buffer, 255);
			return buffer.ToString();
		}

		///<summary>
		/// Returns the window class name for the referenced window.
		///</summary>
		public static string GetClassName(IntPtr handle)
		{
			StringBuilder className = new StringBuilder();
			className.Capacity = 255;
			Win32.GetClassName(handle, className, 255);
			return className.ToString();
		}
	}
}
