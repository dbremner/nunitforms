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
// Author: Anders Lillrank

#endregion

using System.Windows.Forms;

namespace NUnit.Extensions.Forms
{
	///<summary>
	/// A tester for <see cref="ToolStripButton"/> items.
	///</summary>
	public class ToolStripButtonTester : ToolStripItemTester
	{
		#region Constructors

		///<summary>
		/// Constructs a new ToolStripButtonTester for the ToolStripButton with the given name
		/// on the given Form.
		///</summary>
		///<param name="name">The name of the ToolStripButton to find.</param>
		///<param name="form">The form to search.</param>
		public ToolStripButtonTester(string name, Form form) : base(name, form) {}

		///<summary>
		/// Constructs a new ToolStripButtonTester for the ToolStripButton with the given name
		/// on the Form with the given formName.
		///</summary>
		///<param name="name">The name of the ToolStripButton to find.</param>
		///<param name="formName">The name of the form to search.</param>
		public ToolStripButtonTester(string name, string formName) : base(name, formName) { }

		///<summary>
		/// Constructs a new ToolStripButtonTester for the ToolStripButton with the given name.
		///</summary>
		///<param name="name">The name of the ToolStripButton to find.</param>
		public ToolStripButtonTester(string name): base(name) {}

		#endregion

		/// <summary>
		/// Provides access to all of the Properties of the button.
		/// </summary>
		/// <remarks>
		/// Allows typed access to all of the properties of the underlying control.
		/// </remarks>
		/// <value>The underlying control.</value>
		public ToolStripButton Properties
		{
			get { return (ToolStripButton) Component; }
		}
	}
}