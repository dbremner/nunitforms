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
using System.Windows.Forms;

using NUnit.Framework;

namespace NUnit.Extensions.Forms.TestApplications
{
    [TestFixture]
    public class ControlCollectionTest
    {
        private ControlCollection collection = null;

        private Control one = null;

        private Control two = null;

        private Control three = null;

        [SetUp]
        public void setup()
        {
            collection = new ControlCollection();
            one = new Button();
            two = new Button();
            three = new Button();
        }

        private void Add(Control item)
        {
            collection.Add(item);
        }

        [Test]
        public void Count()
        {
            Assert.AreEqual(0, collection.Count);
            Add(one);
            Add(two);
            Assert.AreEqual(2, collection.Count);
        }

        [Test]
        public void GetEnumerator()
        {
            Add(one);
            Add(two);
            Add(three);
            Enumerate();
        }

        [Test]
        public void AddCollection()
        {
            ControlCollection collection2 = new ControlCollection();
            collection2.Add(three);
            Add(one);
            Add(two);
            collection.AddRange(collection2);
            Enumerate();
        }

        private void Enumerate()
        {
            IEnumerator e = collection.GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreSame(one, e.Current);
            Assert.IsTrue(e.MoveNext());
            Assert.AreSame(two, e.Current);
            Assert.IsTrue(e.MoveNext());
            Assert.AreSame(three, e.Current);
            Assert.IsTrue(!e.MoveNext());
        }

        [Test]
        public void Indexer()
        {
            Add(one);
            Assert.AreSame(one, collection[0]);
        }
    }
}