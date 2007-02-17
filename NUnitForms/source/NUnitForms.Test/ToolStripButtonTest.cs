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
  public class ToolStripButtonTest : NUnitFormTest
  {
    private LabelTester label1 = new LabelTester("label1");

    [SetUp]
    public void Init()
    {
      new ToolStripButtonTestForm().Show();
    }

    [Test]
    public void MainMenuButton()
    {
      base.init();
      ToolStripButtonTester tester = new ToolStripButtonTester("toolStripButtonOnMain");
      tester.Click();
      Assert.AreEqual(label1.Text, "toolStripButtonOnMain clicked");
      base.Verify();
    }

    [Test]
    public void PanelButton()
    {
      base.init();
      ToolStripButtonTester tester = new ToolStripButtonTester("toolStripButtonInPanel");
      tester.Click();
      Assert.AreEqual(label1.Text, "toolStripButtonInPanel clicked");
      base.Verify();
    }

    [Test]
    public void TopContainerButton()
    {
      base.init();
      ToolStripButtonTester tester = new ToolStripButtonTester("toolStripButtonTopInContainer");
      tester.Click();
      Assert.AreEqual(label1.Text, "toolStripButtonTopInContainer clicked");
      base.Verify();
    }

    [Test]
    public void LeftContainerButton()
    {
      base.init();
      ToolStripButtonTester tester = new ToolStripButtonTester("toolStripButtonLeftInContainer");
      tester.Click();
      Assert.AreEqual(label1.Text, "toolStripButtonLeftInContainer clicked");
      base.Verify();
    }

    [Test]
    public void CenterContainerButton()
    {
      base.init();
      ToolStripButtonTester tester = new ToolStripButtonTester("toolStripButtonCenterInContainer");
      tester.Click();
      Assert.AreEqual(label1.Text, "toolStripButtonCenterInContainer clicked");
      base.Verify();
    }

    [Test]
    public void RightContainerButton()
    {
      base.init();
      ToolStripButtonTester tester = new ToolStripButtonTester("toolStripButtonRightInContainer");
      tester.Click();
      Assert.AreEqual(label1.Text, "toolStripButtonRightInContainer clicked");
      base.Verify();
    }

    [Test]
    public void BottomContainerButton()
    {
      base.init();
      ToolStripButtonTester tester = new ToolStripButtonTester("toolStripButtonBottomInContainer");
      tester.Click();
      Assert.AreEqual(label1.Text, "toolStripButtonBottomInContainer clicked");
      base.Verify();
    }

    [Test]
    public void StatusBarDropDownButton()
    {
      base.init();
      ToolStripButtonTester tester = new ToolStripButtonTester("toolStripDropDownButtonOnStatusStrip");
      tester.Click();
      Assert.AreEqual(label1.Text, "toolStripDropDownButtonOnStatusStrip clicked");
      base.Verify();
    }
  }
}
