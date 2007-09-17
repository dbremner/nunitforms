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

			Assert.AreEqual(8, parser.Groups.Length);

			int groupIndex = 0;
			AssertGroup(parser.Groups[groupIndex++], "", "111");
			AssertGroup(parser.Groups[groupIndex++], "+", "aaa");
			AssertGroup(parser.Groups[groupIndex++], "", "22");
			AssertGroup(parser.Groups[groupIndex++], "+^", "bbb");
			AssertGroup(parser.Groups[groupIndex++], "", "33");
			AssertGroup(parser.Groups[groupIndex++], "", "{");
			AssertGroup(parser.Groups[groupIndex++], "", "4");
			AssertGroup(parser.Groups[groupIndex], "%", "a");
		}

		[Test]
		public void Key_ENTER()
		{
			ISendKeysParser parser = new SendKeysParser("{ENTER}");

			Assert.AreEqual(1, parser.Groups.Length);

			Assert.AreEqual("", parser.Groups[0].ModifierCharacters);
			Assert.AreEqual(VirtualKeyCodes.RETURN, parser.Groups[0].EscapedKey);
			Assert.AreEqual("", parser.Groups[0].Body);
		}

		private static void AssertGroup(ISendKeysParserGroup group, string modifierCharacters, string bodyText)
		{
			Assert.AreEqual(group.ModifierCharacters, modifierCharacters);
			Assert.AreEqual(group.Body, bodyText);
		}
	}
}