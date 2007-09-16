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

using NMock2;
using NUnit.Extensions.Forms.Util;
using NUnit.Extensions.Forms.Win32Interop;
using NUnit.Framework;


namespace NUnit.Extensions.Forms.UnitTests
{
	[TestFixture]
	public class SendKeysTests : MockingTestFixture
	{
		private SendKeys keyboardSendKeys;
		private ISendKeyboardInput keyboardInput;
		private ISendKeysParserFactory parserFactory;
		private ISendKeysParser parser;

		protected override void SetUp()
		{
			keyboardInput = NewMock<ISendKeyboardInput>();
			parserFactory = NewMock<ISendKeysParserFactory>();
			parser = NewMock<ISendKeysParser>();

			keyboardSendKeys = new SendKeys(keyboardInput, parserFactory);
		}

		protected override void TearDown()
		{
			keyboardSendKeys.Dispose();
		}

		[Test]
		public void SendWait_SingleCharLowerCase()
		{
			string[] modifiers = new string[] { "" };
			string[] text = new string[] { "b" };

			StubFormatter(text, modifiers);

			ExpectKeyDownAndRelease(VirtualKeyCodes.B);

			keyboardSendKeys.SendWait(string.Join("", text));
		}

		[Test]
		public void SendWait_SingleCharUpperCase()
		{
			string[] modifiers = new string[] { "" };
			string[] text = new string[] { "B" };

			StubFormatter(text, modifiers);

			ExpectKeyDown(VirtualKeyCodes.SHIFT);
			ExpectKeyDownAndRelease(VirtualKeyCodes.B);
			ExpectKeyUp(VirtualKeyCodes.SHIFT);

			keyboardSendKeys.SendWait(string.Join("", text));
		}

		[Test]
		public void SendWait_ShiftFormatted()
		{
			string[] modifiers = new string[] { "+" };
			string[] text = new string[] { "ab" };

			StubFormatter(text, modifiers);

			ExpectKeyDown(VirtualKeyCodes.SHIFT);
			ExpectKeyDownAndRelease(VirtualKeyCodes.A);
			ExpectKeyDownAndRelease(VirtualKeyCodes.B);
			ExpectKeyUp(VirtualKeyCodes.SHIFT);

			keyboardSendKeys.SendWait(string.Join("", text));
		}

		[Test]
		public void SendWait_ControlFormatted()
		{
			string[] modifiers = new string[] { "^" };
			string[] text = new string[] { "ab" };

			StubFormatter(text, modifiers);

			ExpectKeyDown(VirtualKeyCodes.CONTROL);
			ExpectKeyDownAndRelease(VirtualKeyCodes.A);
			ExpectKeyDownAndRelease(VirtualKeyCodes.B);
			ExpectKeyUp(VirtualKeyCodes.CONTROL);

			keyboardSendKeys.SendWait(string.Join("", text));
		}

		[Test]
		public void SendWait_AltFormatted()
		{
			string[] modifiers = new string[] { "%" };
			string[] text = new string[] { "ab" };

			StubFormatter(text, modifiers);

			ExpectKeyDown(VirtualKeyCodes.MENU);
			ExpectKeyDownAndRelease(VirtualKeyCodes.A);
			ExpectKeyDownAndRelease(VirtualKeyCodes.B);
			ExpectKeyUp(VirtualKeyCodes.MENU);

			keyboardSendKeys.SendWait(string.Join("", text));
		}

		[Test]
		public void SendWait_AltShiftControlFormatted()
		{
			string[] modifiers = new string[] { "%+^" };
			string[] text = new string[] { "ab" };

			StubFormatter(text, modifiers);

			ExpectKeyDown(VirtualKeyCodes.MENU);
			ExpectKeyDown(VirtualKeyCodes.CONTROL);
			ExpectKeyDown(VirtualKeyCodes.SHIFT);

			ExpectKeyDownAndRelease(VirtualKeyCodes.A);
			ExpectKeyDownAndRelease(VirtualKeyCodes.B);

			ExpectKeyUp(VirtualKeyCodes.SHIFT);
			ExpectKeyUp(VirtualKeyCodes.CONTROL);
			ExpectKeyUp(VirtualKeyCodes.MENU);

			keyboardSendKeys.SendWait(string.Join("", text));
		}

		[Test]
		[Ignore("This test is keyboard layout dependent.")]
		public void SendWait_SimpleText()
		{
			string[] modifiers = new string[] { "" };
			string[] text = new string[] { "aA {1." };

			StubFormatter(text, modifiers);

			ExpectKeyDownAndRelease(VirtualKeyCodes.A);

			ExpectKeyDown(VirtualKeyCodes.SHIFT);
			ExpectKeyDownAndRelease(VirtualKeyCodes.A);
			ExpectKeyUp(VirtualKeyCodes.SHIFT);

			ExpectKeyDownAndRelease(VirtualKeyCodes.SPACE);

			// This will be keyboard layout dependent
			ExpectKeyDown(VirtualKeyCodes.SHIFT);
			ExpectKeyDownAndRelease(VirtualKeyCodes.OEM_4);	// '[' key shifted to give '{'
			ExpectKeyUp(VirtualKeyCodes.SHIFT);

			ExpectKeyDownAndRelease(VirtualKeyCodes.DIGIT_1);
			ExpectKeyDownAndRelease(VirtualKeyCodes.OEM_PERIOD);

			keyboardSendKeys.SendWait(string.Join("", text));
		}

		private void ExpectKeyDown(VirtualKeyCodes keyCode)
		{
			Expect.Once.On(keyboardInput).Method("SendInput").With(keyCode, SendInputFlags.KeyDown);
		}

		private void ExpectKeyUp(VirtualKeyCodes keyCode)
		{
			Expect.Once.On(keyboardInput).Method("SendInput").With(keyCode, SendInputFlags.KeyUp);
		}

		private void ExpectKeyDownAndRelease(VirtualKeyCodes keyCode)
		{
			Expect.Once.On(keyboardInput).Method("SendInput").With(keyCode, SendInputFlags.KeyDown);
			Expect.Once.On(keyboardInput).Method("SendInput").With(keyCode, SendInputFlags.KeyUp);
		}

		private void StubFormatter(string[] text, string[] modifiers)
		{
			Stub.On(parserFactory).Method("Create").With(string.Join("", text)).Will(Return.Value(parser));
			Stub.On(parser).GetProperty("GroupCount").Will(Return.Value(1));
			Stub.On(parser).GetProperty("Modifiers").Will(Return.Value(modifiers));
			Stub.On(parser).GetProperty("Text").Will(Return.Value(text));
		}
	}
}
