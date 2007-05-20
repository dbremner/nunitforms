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

using System.Collections;

using NUnit.Framework;

namespace NUnit.Extensions.Forms.Scripting.Test
{
    [TestFixture]
    [Category("Scripting")]
    public class ScriptPlayerTest
    {
        private ArrayList before;

        private ArrayList after;

        private bool beforeFlag;

        private bool afterFlag;

        private bool successFlag;

        [Test]
        public void ScriptPlay()
        {
            string script =
                    @"NUnit.Extensions.Forms.TestApplications.TextBoxTestForm, NUnitForms.Test
myTextBox Enter abC
anotherTextBox Enter qrs
myTextBox Assert Text abC
anotherTextBox Assert Text qrs";

            RunScript(script);

            Assert.AreEqual(5, before.Count);
            Assert.AreEqual(5, after.Count);
            Assert.IsTrue(beforeFlag);
            Assert.IsTrue(afterFlag);
            Assert.IsTrue(successFlag);
        }

        [Test]
        public void ScriptPlayPropertyNumbers()
        {
            string script =
                    @"NUnit.Extensions.Forms.TestApplications.TextBoxTestForm, NUnitForms.Test
myTextBox Assert Width 208";

            successFlag = false;

            ScriptPlayer player = new ScriptPlayer();
            player.Success += new ScriptPlayer.SuccessHandler(SuccessListener);

            player.Play(script);

            Assert.IsTrue(successFlag);
        }

        private void RunScript(string script)
        {
            before = new ArrayList();
            after = new ArrayList();
            beforeFlag = false;
            afterFlag = false;
            successFlag = false;

            ScriptPlayer player = new ScriptPlayer();
            player.BeforeExecute += new ScriptPlayer.BeforeExecuteHandler(BeforeListener);
            player.Success += new ScriptPlayer.SuccessHandler(SuccessListener);
            player.AfterExecute += new ScriptPlayer.AfterExecuteHandler(AfterListener);
            player.Play(script);
        }

        public void BeforeListener(int lineNumber)
        {
            before.Add(lineNumber);
            if(lineNumber == 1)
            {
                Assert.AreEqual("default", new ControlTester("myTextBox").Text);
                beforeFlag = true;
            }
        }

        public void AfterListener(int lineNumber, bool success, bool assert, string message)
        {
            after.Add(lineNumber);
            if(lineNumber == 1)
            {
                Assert.AreEqual("abC", new ControlTester("myTextBox").Text);
                afterFlag = true;
            }
        }

        public void SuccessListener(bool success)
        {
            successFlag = success;
        }

        [Test]
        public void ScriptPlayAssertFail()
        {
            string script =
                    @"NUnit.Extensions.Forms.TestApplications.TextBoxTestForm, NUnitForms.Test
myTextBox Enter abC
anotherTextBox Enter qrs
myTextBox Assert Text abC
anotherTextBox Assert Text qrst";

            try
            {
                RunScript(script);
                Assert.Fail("Should have thrown exception");
            }
            catch(AssertionException)
            {
                Assert.AreEqual(5, before.Count);
                Assert.AreEqual(5, after.Count);
                Assert.IsFalse(successFlag);
            }
        }
    }
}