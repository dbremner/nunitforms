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
using NUnit.Extensions.Forms.TestApplications;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.TestApplications
{
  [TestFixture]
  public class SaveFileDialogTest : NUnitFormTest
  {
    private LabelTester label1 = new LabelTester("lblFileName");
    private string _fileName = "";
    

    [SetUp]
    public void Init()
    {
      base.init();
     
      new SaveFileDialogTestForm().Show();
    }

    [Test]
    public void SaveTest()
    {
      ExpectFileDialog("SaveFileHandler");

      // Generate a temporary file
      _fileName = System.IO.Path.GetTempPath() + "NUnitFormsTestFile.tmp";
      // If exists remove it
      if (System.IO.File.Exists(_fileName))
      {
        System.IO.File.Delete(_fileName);
      }

      
      ButtonTester save_btn = new ButtonTester("btSave");
      save_btn.Click();
      Assert.AreEqual(label1.Text, _fileName);
    }



    [Test]
    public void CancelTest()
    {
      ExpectFileDialog("CancelFileHandler");
      ButtonTester save_btn = new ButtonTester("btSave");
      save_btn.Click();
      Assert.AreEqual(label1.Text, "cancel pressed");
    }
    public void SaveFileHandler()
    {
      SaveFileDialogTester dlg_tester = new SaveFileDialogTester("Save As");
      dlg_tester.SaveFile(_fileName);
    }

    public void CancelFileHandler()
    {
      SaveFileDialogTester dlg_tester = new SaveFileDialogTester("Save As");
      dlg_tester.ClickCancel();
    }

  }
}
