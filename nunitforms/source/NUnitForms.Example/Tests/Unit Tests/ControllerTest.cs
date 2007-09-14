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

using NMock;
using NUnit.Extensions.Forms.ExampleApplication;
using NUnit.Framework;

namespace NUnit.Forms.ExampleApplication
{
    [TestFixture]
    public class ControllerTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            formManager = new DynamicMock(typeof (IFormManager));
            model = new DynamicMock(typeof (IAppModel));
            controller = new AppController((IAppModel) model.MockInstance, (IFormManager) formManager.MockInstance);
        }

        #endregion

        private int TestValue = 2;

        private string TestValueString = "2";

        private Mock formManager = null;

        private Mock model = null;

        private AppController controller = null;

        [Test]
        public void Count()
        {
            model.Expect("BusinessLogic");
            controller.Count();
            model.Verify();
        }

        [Test]
        public void Data()
        {
            model.ExpectAndReturn("GetData", TestValue);
            Assert.AreEqual(TestValueString, controller.GetData());
            model.Verify();
        }

        [Test]
        public void ShowModal()
        {
            formManager.Expect("ShowMessageBox", "Testing!", "Alert");
            controller.ShowModal();
            formManager.Verify();
        }
    }
}