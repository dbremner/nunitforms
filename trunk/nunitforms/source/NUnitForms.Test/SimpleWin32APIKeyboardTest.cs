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

using System.Windows.Forms;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.TestApplications
{
	[TestFixture]
	[Category("DisplayHidden")]
	[Category("ControlsKeyboard")]
	public class SimpleWin32APIKeyboardTest : NUnitFormTest
	{
		public override bool DisplayHidden
		{
			get { return true; }
		}

		[Test]
		public void PressEnterClicksButton()
		{
			Form form = new ButtonTestForm();
			form.Show();
			LabelTester label = new LabelTester("myLabel", form);
			ButtonTester button = new ButtonTester("myButton", form);

			Assert.AreEqual("0", label.Text);

			Keyboard.UseOn(button);
			Keyboard.Press(Win32Key.RETURN);

			Assert.AreEqual("1", label.Text);
		}

		[Test]
		public void TypeShiftAABC()
		{
			new TextBoxTestForm().Show();
			TextBoxTester box = new TextBoxTester("myTextBox");
			Assert.AreEqual("default", box.Text);

			Keyboard.UseOn(box);

			Keyboard.Press(Win32Key.SHIFT, Win32Key.A);
			Keyboard.Press(Win32Key.A);
			Keyboard.Press(Win32Key.B);
			Keyboard.Press(Win32Key.C);

			Assert.AreEqual("Aabc", box.Text);
		}

		[Test]
		public void ReplaceOneWithDIGIT_1WhenNotInBraces()
		{
			new TextBoxTestForm().Show();
			TextBoxTester box = new TextBoxTester("myTextBox");
			Assert.AreEqual("default", box.Text);

			Keyboard.UseOn(box);

			Keyboard.Press(Win32Key.NUMPAD1);
			Keyboard.Press(Win32Key.NUMPAD2);
			Keyboard.Press(Win32Key.NUMPAD3);
			Keyboard.Press(Win32Key.NUMPAD1);

			Assert.AreEqual("1231", box.Text);
		}
	}
}