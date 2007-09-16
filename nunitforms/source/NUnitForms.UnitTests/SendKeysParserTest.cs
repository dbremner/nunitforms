#region Copyright (c) 2003-2007, Luke T. Maxon

/********************************************************************************************************************
'
' Copyright (c) 2003-2007, Luke T. Maxon
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

using NUnit.Extensions.Forms.Util;
using NUnit.Extensions.Forms.Win32Interop;
using NUnit.Framework;


namespace NUnit.Extensions.Forms.UnitTests
{
	[TestFixture]
	public class SendKeysParserTest
	{
		[Test]
		public void GroupExtraction()
		{
			ISendKeysParser parser = new SendKeysParser("111+(aaa)22+^(bbb)33{{}4%(a)");

			Assert.AreEqual(8, parser.GroupCount);

			int groupIndex = 0;
			Assert.AreEqual("", parser.Modifiers[groupIndex++]);
			Assert.AreEqual("+", parser.Modifiers[groupIndex++]);
			Assert.AreEqual("", parser.Modifiers[groupIndex++]);
			Assert.AreEqual("+^", parser.Modifiers[groupIndex++]);
			Assert.AreEqual("", parser.Modifiers[groupIndex++]);
			Assert.AreEqual("", parser.Modifiers[groupIndex++]);
			Assert.AreEqual("", parser.Modifiers[groupIndex++]);
			Assert.AreEqual("%", parser.Modifiers[groupIndex]);

			groupIndex = 0;
			Assert.AreEqual("111", parser.Text[groupIndex++]);
			Assert.AreEqual("aaa", parser.Text[groupIndex++]);
			Assert.AreEqual("22", parser.Text[groupIndex++]);
			Assert.AreEqual("bbb", parser.Text[groupIndex++]);
			Assert.AreEqual("33", parser.Text[groupIndex++]);
			Assert.AreEqual("{", parser.Text[groupIndex++]);
			Assert.AreEqual("4", parser.Text[groupIndex++]);
			Assert.AreEqual("a", parser.Text[groupIndex]);
		}

		[Test]
		public void Key_ENTER()
		{
			ISendKeysParser parser = new SendKeysParser("{ENTER}");

			Assert.AreEqual(1, parser.GroupCount);

			Assert.AreEqual("", parser.Modifiers[0]);
			Assert.AreEqual(VirtualKeyCodes.RETURN, parser.EscapedKeys[0]);
			Assert.AreEqual("", parser.Text[0]);
		}
	}
}