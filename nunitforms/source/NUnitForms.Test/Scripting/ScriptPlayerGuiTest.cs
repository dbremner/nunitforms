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

using NUnit.Extensions.Forms.Player;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.Scripting.Test
{
    [TestFixture]
    [Category("Scripting")]
    public class ScriptPlayerGuiTest : NUnitFormTest
    {
        public override bool DisplayHidden
        {
            get
            {
                return true;
            }
        }

        public override bool UseHidden
        {
            get
            {
                //somehow if this is true, it takes your computer
                //to a bad place.  you can not escape the hidden
                //desktop.  (Tell me if you know how to fix this.)
                return false;
            }
        }

        [Test]
        ///<summary>
                ///The <c>AppForm</c> contains two buttons with names <c>btnOpen</c> and <c>btnRun</c>. When both
                ///buttons are clicked, the status bar of <c>AppForm</c> should display <c>Success!</c>, i.e. the
                ///control with name <c>statusBar1</c> (the name of the <c>StatusBar</c> in <c>AppForm</c> displays
                ///<c>Success!</c>.
                ///The <c>ControlTester</c> is initialized with the name of the <c>AppForm</c> <c>StatusBar</c>, hence
                ///it should contain the <c>Success!</c> message.
                ///</summary>
                public void GeneralPath()
        {
            new AppForm().Show();

            ButtonTester openScript = new ButtonTester("btnOpen");
            ButtonTester runScript = new ButtonTester("btnRun");

            openScript.Click();
            runScript.Click();


            ControlTester statusBar = new ControlTester("statusBar1");

            Assert.AreEqual("Success!", statusBar.Text);
        }
    }
}