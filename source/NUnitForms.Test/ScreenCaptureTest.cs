#region Copyright (c) 2003-2006, Bart De Boeck

/********************************************************************************************************************
'
' Copyright (c) 2003-2006, Bart De Boeck
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

namespace NUnit.Extensions.Forms.TestApplications
{
    /// <summary>
    /// Tests the basic functionality of screen captures. Not intended as a real unit tests, used
    /// as a prototype.
    /// </summary>
    [TestFixture]
    public class ScreenCaptureTest : NUnitFormTest
    {
        /// <summary>
        /// Test comparing a screen capture of a ButtonTestForm and a screen capture of a ButtonTestForm instantiated during
        /// testing.
        /// </summary>
        [Test]
		[Ignore("Hangs NUnit")]
		public void TestCapture()
        {
            CompareCapture(new ButtonTestForm(), @"G:\xenopz\projecten\OS\NUnitForms\NUnitForms\source\NUnitForms.Test\NUnitFormsCapture\ButtonTestForm_1.png");
        }
    }
}