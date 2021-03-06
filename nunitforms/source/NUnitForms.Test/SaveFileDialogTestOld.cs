#region Copyright (c) 2006-2007, Luke T. Maxon (Authored by Anders Lillrank)

/********************************************************************************************************************
'
' Copyright (c) 2006-2007, Luke T. Maxon
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

using System.IO;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.TestApplications
{
    [TestFixture]
    [Ignore("This dialog caused my tests to hang.")]
    public class SaveFileDialogTestOld : NUnitFormTest
    {
        private LabelTester label1 = new LabelTester("lblFileName");
        private string _fileName = "";
        private SaveFileDialogTestForm form;

        public override void Setup()
        {
            base.Setup();

            form = new SaveFileDialogTestForm();
            form.Show();
        }

        private void ClickSaveButton()
        {
            ButtonTester save_btn = new ButtonTester("btSave");
            save_btn.Click();
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

        public void SaveDefaultFileHandler()
        {
            SaveFileDialogTester dlg_tester = new SaveFileDialogTester("Save As");
            dlg_tester.SaveFile();
        }

        private void EnsureFileDoesntExist()
        {
            // If exists remove it
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }

        [Test, System.STAThread]
        public void CancelTest()
        {
            ExpectFileDialog("CancelFileHandler");
            ClickSaveButton();
            Assert.AreEqual(label1.Text, "cancel pressed");
        }

        [Test, System.STAThread]
        public void SaveTest()
        {
            ExpectFileDialog("SaveFileHandler");

            // Generate a temporary file
            _fileName = Path.GetTempPath() + "NUnitFormsTestFile.tmp";
            EnsureFileDoesntExist();

            ClickSaveButton();
            Assert.AreEqual(label1.Text.ToLowerInvariant(), _fileName.ToLowerInvariant());
        }


        [Test, System.STAThread]
        public void SaveWithDefaultFile()
        {
            ExpectFileDialog("SaveDefaultFileHandler");

            _fileName = Path.GetTempPath() + "NUnitFormsDefaultTestFile.tmp";
            EnsureFileDoesntExist();

            form.SetDefaultTestFileName(_fileName);
            ClickSaveButton();
            Assert.AreEqual(label1.Text.ToLowerInvariant(), _fileName.ToLowerInvariant());
        }
    }
}