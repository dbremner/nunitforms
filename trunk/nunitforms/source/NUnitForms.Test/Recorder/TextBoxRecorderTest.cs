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
using NUnit.Extensions.Forms.TestApplications;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.Recorder.Test
{
	///<summary>
	/// Test Fixture for the <see cref="TextBoxRecorder"/>.
	///</summary>
	[TestFixture]
	[Category("Recorder")]
	public class TextBoxRecorderTest : NUnitFormTest
	{
		public override Type FormType
		{
			get { return typeof (TextBoxTestForm); }
		}

		///<summary>
		/// Tests text entry events.
		///</summary>
		[Test]
		public void TextBoxEnter()
		{
			TestWriter writer = new TestWriter(CurrentForm);
			Assert.AreEqual("", writer.Test);

			TextBoxTester textBox = new TextBoxTester("myTextBox");
			//doing 2 of these tests the collapsing processor.
			textBox.Enter("abc");
			textBox.Enter("abcd");

			Assert.AreEqual(
				@"[Test]
public void Test()
{

	TextBoxTester myTextBox = new TextBoxTester(""myTextBox"");

	myTextBox.Enter(""abcd"");

}",
				writer.Test);
		}

		///<summary>
		/// Tests multiline text entry events.
		///</summary>
		[Test]
		[Ignore]
		public void TextBoxEnterMultiline()
		{
			TestWriter writer = new TestWriter(CurrentForm);
			Assert.AreEqual("", writer.Test);

			TextBoxTester textBox = new TextBoxTester("myTextBox");
			textBox.Properties.Multiline = true;

			textBox.Enter("abc\nabcd\nabcde");

			Assert.AreEqual(
				@"[Test]
public void Test()
{

	TextBoxTester myTextBox = new TextBoxTester(""myTextBox"");

	myTextBox.Enter(""abc\nabcd\nabcde"");

}",
				writer.Test);
		}

		[Test]
		public void ProgrammaticallyChangeTextIsNotRecorded()
		{
			TestWriter writer = new TestWriter(CurrentForm);
			Assert.AreEqual("", writer.Test);

			TextBoxTester textBox = new TextBoxTester("myTextBox");
			textBox.Properties.Text = "abc";

			Assert.AreEqual(@"", writer.Test);
		}

		[Test]
		public void ProgrammaticallyChangeTextIsNotRecordedTwoBoxes()
		{
			TestWriter writer = new TestWriter(CurrentForm);
			Assert.AreEqual("", writer.Test);

			TextBoxTester anotherBox = new TextBoxTester("anotherTextBox");
			anotherBox.FireEvent("Enter");

			TextBoxTester textBox = new TextBoxTester("myTextBox");
			textBox.Properties.Text = "abc";

			anotherBox.FireEvent("Leave");

			Assert.AreEqual(@"", writer.Test);
		}
	}
}