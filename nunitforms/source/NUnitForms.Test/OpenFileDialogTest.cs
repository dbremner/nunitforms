#region Copyright (c) 2006, Luke T. Maxon

/********************************************************************************************************************
'
' Copyright (c) 2006, Luke T. Maxon
' All rights reserved.
' Author: Anders Lillrank
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
using System.IO;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.TestApplications
{
	///<summary>
	/// Test Fixture for the OpenFileDialogTester class.
	///</summary>
	[TestFixture]
	public class OpenFileDialogTest : NUnitFormTest
	{
		///<summary>
		/// Sets up this test by showing a new OpenFileDialogTestForm form.
		///</summary>
		public override void Setup()
		{
			new OpenFileDialogTestForm().Show();
		}

		///<summary>
		/// Tests opening a file.
		///</summary>
		[Test]
		public void OpenTest()
		{
			LabelTester label1 = new LabelTester("lblFileName");
			ExpectFileDialog("OpenFileHandler");
			string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NUnitForms.dll");
			ButtonTester open_btn = new ButtonTester("btOpenFile");
			open_btn.Click();
			Assert.AreEqual(label1.Text, fileName);
		}


		///<summary>
		/// Tests cancelling.
		///</summary>
		[Test]
		public void CancelTest()
		{
			LabelTester label1 = new LabelTester("lblFileName");
			ExpectFileDialog("CancelFileHandler");
			ButtonTester open_btn = new ButtonTester("btOpenFile");
			open_btn.Click();
			Assert.AreEqual(label1.Text, "cancel pressed");
		}

		///<summary>
		/// Modal handler to click the open button.
		///</summary>
		public void OpenFileHandler()
		{
			OpenFileDialogTester dlg_tester = new OpenFileDialogTester("Open");
			string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NUnitForms.dll");
			dlg_tester.OpenFile(fileName);
		}

		///<summary>
		/// Modal handler to click the cancel button.
		///</summary>
		public void CancelFileHandler()
		{
			OpenFileDialogTester dlg_tester = new OpenFileDialogTester("Open");
			dlg_tester.ClickCancel();
		}
	}
}