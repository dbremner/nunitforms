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

// Author Anders Lillrank

#endregion

using NUnit.Extensions.Forms.TestApplications;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.TestApplications
{
  [TestFixture]
  public class ToolStripDropDownButtonTest : NUnitFormTest
  {
    private TextBoxTester textbox = new TextBoxTester("textBox1");

    [SetUp]
    public void Init()
    {
      new ToolStripDropDownButtonTestForm().Show();
    }

    [Test]
    public void MainToolbar()
    {
      base.init();
      ToolStripDropDownButtonTester tester = new ToolStripDropDownButtonTester("toolStripDropDownButton1");
      tester.Click();
      Assert.IsTrue(textbox.Text == "toolStripDropDownButton1 clicked");
      base.Verify();
    }

    [Test]
    public void ClickDropDownItem()
    {
      base.init();
      ToolStripDropDownButtonTester tester = new ToolStripDropDownButtonTester("toolStripDropDownButton1");
      tester.ClickDropDownItem(1);
      Assert.IsTrue(textbox.Text == "twoToolStripMenuItem clicked");
      base.Verify();

    }
  }
}
