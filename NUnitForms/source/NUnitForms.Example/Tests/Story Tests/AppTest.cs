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

using NUnit.Extensions.Forms;
using NUnit.Framework;
using NUnit.Extensions.Forms.ExampleApplication;

namespace NUnit.Forms.ExampleApplication
{
    [TestFixture]
    public class AppTest : NUnitFormTest
    {
        public override Form ActivateForm()
        {
            return new AppForm(new AppController(new AppModel(), new FormManager()));
        }

        [Test]
        public void ButtonLabelShouldDefaultToZero()
        {
            ButtonTester button = new ButtonTester("countButton");
            Assert.AreEqual("0", button.Text);
        }

        [Test]
        public void ButtonLabelShouldBeOneAfterClicked()
        {
            ButtonTester button = new ButtonTester("countButton");
            button.Click();
            Assert.AreEqual("1", button.Text);
        }

        [Test]
        public void ButtonLabelShouldBeTwoAfterClickedTwice()
        {
            ButtonTester button = new ButtonTester("countButton");
            button.Click();
            button.Click();
            Assert.AreEqual("2", button.Text);
        }

        [Test]
        public void ShowModalButtonShouldShowModalDialog()
        {
            ExpectModal("Alert", "handleModal");
            ButtonTester button = new ButtonTester("showModalButton");
            button.Click();
        }

        public void handleModal()
        {
            new MessageBoxTester("Alert").ClickOk();
        }
    }
}