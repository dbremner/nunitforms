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

namespace NUnit.Extensions.Forms
{
	/// <summary>
	/// A ControlTester for testing CheckBoxes.
	/// </summary>
	/// <remarks>
	/// Has convenience methods for Check and Uncheck as well as the Checked Property.
	/// <para>
	/// Fully supported by the recorder application
	/// </para>
	/// </remarks>
	public class CheckBoxTester : ControlTester<CheckBox, CheckBoxTester>
	{
		///<summary>
		/// Default constructor for Generic support.
		///</summary>
		public CheckBoxTester() {}

		/// <summary>
		/// Creates a ControlTester from the control name and the form instance.
		/// </summary>
		/// <remarks>
		/// It is best to use the overloaded Constructor that requires just the name 
		/// parameter if possible.
		/// </remarks>
		/// <param name="name">The Control name.</param>
		/// <param name="form">The Form instance.</param>
		public CheckBoxTester(string name, Form form) : base(name, form) {}

		/// <summary>
		/// Creates a ControlTester from the control name and the form name.
		/// </summary>
		/// <remarks>
		/// It is best to use the overloaded Constructor that requires just the name 
		/// parameter if possible.
		/// </remarks>
		/// <param name="name">The Control name.</param>
		/// <param name="formName">The Form name..</param>
		public CheckBoxTester(string name, string formName) : base(name, formName) {}

		/// <summary>
		/// Creates a ControlTester from the control name.
		/// </summary>
		/// <remarks>
		/// This is the best constructor.</remarks>
		/// <param name="name">The Control name.</param>
		public CheckBoxTester(string name) : base(name) {}

		/// <summary>
		/// Creates a ControlTester from a ControlTester and an index where the
		/// original tester's name is not unique.
		/// </summary>
		/// <remarks>
		/// It is best to use the overloaded Constructor that requires just the name 
		/// parameter if possible.
		/// </remarks>
		/// <param name="tester">The ControlTester.</param>
		/// <param name="index">The index to test.</param>
		public CheckBoxTester(ControlTester tester, int index) : base(tester, index) {}

		/// <summary>
		/// Sets the shouldCheck property to the specified value.
		/// </summary>
		/// <remarks>
		/// Pass a boolean to indicate checked (true) or unchecked (false)
		/// </remarks>
		/// <param name="shouldCheck">Should the box be checked or not checked.</param>
		public void Check(bool shouldCheck)
		{
			EditChecked(shouldCheck);
		}

		/// <summary>
		/// Sets the Checked property of the CheckBox to true
		/// </summary>
		/// <remarks>
		/// Equivalent to Check(true)
		/// </remarks>
		public void Check()
		{
			EditChecked(true);
		}

		/// <summary>
		/// Sets the Checked property of the CheckBox to false
		/// </summary>
		/// <remarks>
		/// Equivalent to Check(false)
		/// </remarks>
		public void UnCheck()
		{
			EditChecked(false);
		}

		private void EditChecked(bool shouldCheck)
		{
			Properties.Checked = shouldCheck;
			EndCurrentEdit("Checked");
		}

		/// <summary>
		/// Retrieves the Checked property of the CheckBox
		/// </summary>
		/// <remarks>
		/// Just returns the checked property of the underlying checkbox.
		/// </remarks>
		/// <value>
		/// A boolean to indicate whether the CheckBox is checked.
		/// </value>
		public bool Checked
		{
			get { return Properties.Checked; }
		}
	}
}