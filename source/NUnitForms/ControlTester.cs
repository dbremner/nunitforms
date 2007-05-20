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

using System;
using System.Collections;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms
{
	/// <summary>
	/// <para>
	/// A ControlTester for any type of control.  It is the base class for all
	/// ControlTesters in the API.  It can also serve as a generic tester for all 
	/// Controls with no specifically implemented support.
	/// </para>
	/// <para>
	/// This ControlTester looks for Forms and Controls based on their name. So, it is sufficient to initialize
	/// a Form or Control which has a known name and passing this name to this ControlTester during intialization. In
	/// the following code
	/// <code>
	/// new LabelTestForm().Show();
	/// ControlTester label = new ControlTester("myLabel");
	/// </code>
	/// the initialization of <c>LabelTestForm</c> sets its name as <c>myLabel</c>. <c>ControlTester</c> looks for 
	/// initialized Forms and Controls based on their names. Passing <c>myLabel</c> during construction to <c>ControlTester</c>
	/// allows <c>ControlTester</c> to look for Forms and Controls with the name <c>myLabel</c>. This happens in <c>GetControlFinder()</c>.
	/// </para>
	/// <para>
	/// The following names are used by build in NUnitForm types :
	/// <list type="bullet">
	/// <li>LabelTestForm : myLabel</li>
	/// <li>AppForm : statusBar1 - speedBar - lblSpeed - gutter</li>
	/// </list>
	/// </para>
	/// <para>
	/// This <c>ControlTester</c> encapsulates the <c>Control</c> under test. For instance, a click event is simulated 
	/// by calling the <c>OnClick</c> method on the encapsulated <c>Control</c>.
	/// </para>
	/// </summary>
	/// <remarks>
	/// If you want to make your own ControlTester for a custom or unsupported
	/// control, you should implement a version of each of the four constructors.
	/// I plan to separate out (and generate) this code once we get partial class
	/// support in c#.
	/// You should also implement a Property named Properties that returns the 
	/// underlying control.
	/// You should hide the indexer (new) and implement one that returns the
	/// appropriate type.
	/// The ButtonTester class is a good place to look for an example (or cut and
	/// paste starting point) if you are making your own tester.</remarks>
	public class ControlTester : ObjectTester, IEnumerable
	{
		private Form form;
		private string formName;
		private int index = -1;

		/// <summary>
		/// The name of the underlying control.
		/// </summary>
		protected string name;


		/// <summary>
		/// Creates a ControlTester that will test controls with the specified name
		/// on a form with the specified name.
		/// </summary>
		/// <remarks>
		/// If the name is unique, you can operate on the tester directly, otherwise
		/// you should use the indexer or Enumerator properties to access each separate
		/// control.</remarks>
		/// <param name="name">The name of the control to test.</param>
		/// <param name="formName">The name of the form to test.</param>
		public ControlTester(string name, string formName)
		{
			this.formName = formName;
			this.name = name;
		}

		/// <summary>
		/// Creates a ControlTester that will test controls with the specified name
		/// on the specified form.
		/// </summary>
		/// <remarks>
		/// If the name is unique, you can operate on the tester directly, otherwise
		/// you should use the indexer or Enumerator properties to access each separate
		/// control.</remarks>
		/// <param name="name">The name of the control to test.</param>
		/// <param name="form">The form to test.</param>
		public ControlTester(string name, Form form)
		{
			this.form = form;
			this.name = name;
		}

		/// <summary>
		/// Creates a ControlTester that will test controls with the specified name.
		/// </summary>
		/// <remarks>
		/// If the name is unique, you can operate on the tester directly, otherwise
		/// you should use the indexer or Enumerator properties to access each separate
		/// control.</remarks>
		/// <param name="name">The name of the control to test.</param>
		public ControlTester(string name)
		{
			this.name = name;
		}

		/// <summary>
		/// Allows you to find a ControlTester by index where the name is not unique.
		/// </summary>
		/// <remarks>
		/// When a control is not uniquely identified by its name property, you can
		/// access it according to an index.  This should only be used when you have
		/// dynamic controls and it is inconvenient to set the Name property uniquely.
		///
		/// This was added to support the ability to find controls where their name is
		/// not unique.  If all of your controls are uniquely named (I recommend this) then
		/// you will not need this.
		/// </remarks>
		/// <value>The ControlTester at the specified index.</value>
		/// <param name="controlIndex">The index of the ControlTester.</param>
		public ControlTester this[int controlIndex]
		{
			get { return new ControlTester(this, controlIndex); }
		}

		/// <summary>
		/// Returns the number of controls associated with this tester.
		/// </summary>
		public int Count
		{
			get { return GetControlFinder().Count; }
		}

		/// <summary>
		/// Returns uniquely qualified ControlTesters for each of the controls
		/// associated with this tester as an IEnumerator.  This allows use of a 
		/// foreach loop.
		/// </summary>
		/// <returns>IEnumerator of ControlTesters (typed correctly)</returns>
		public virtual IEnumerator GetEnumerator()
		{
			ArrayList list = new ArrayList();
			int count = Count;
			Type type = GetType();
			for (int i = 0; i < count; i++)
			{
				list.Add(Activator.CreateInstance(type, new object[] {this, i}));
			}
			return list.GetEnumerator();
		}

		/// <summary>
		/// Convenience method "Clicks" on the control being tested if it is visible.
		/// </summary>
		/// <exception>
		/// ControlNotVisibleException is thrown if the Control is not Visible.
		/// </exception>
		public override void Click()
		{
			if (!Control.Visible)
				throw new ControlNotVisibleException(name);

			FireEvent("Click");
		}

		/// <summary>
		/// Convenience method "DoubleClicks" on the control being tested if it is visible.
		/// </summary>
		/// <exception>
		/// ControlNotVisibleException is thrown if the Control is not Visible.
		/// </exception>
		public virtual void DoubleClick()
		{
			if (!Control.Visible)
				throw new ControlNotVisibleException(name);

			FireEvent("DoubleClick");
		}

		/// <summary>
		/// Default handler for entering text into a text control.
		/// Must be exposed publically by testers that want to use it.
		/// </summary>
		protected virtual void EnterText(string text)
		{
			FireEvent("Enter");
			Control.Text = text;
			FireEvent("Leave");

			EndCurrentEdit("Text");
		}

		/// <summary>
		/// Should call this method after editing something in order to trigger any
		/// databinding done with the Databindings collection.  (ie text box to a data
		/// set)
		/// </summary>
		public void EndCurrentEdit(string propertyName)
		{
			if (Control.DataBindings[propertyName] != null)
			{
				Control.DataBindings[propertyName].BindingManagerBase.EndCurrentEdit();
			}
		}


		/// <summary>
		/// Convenience method retrieves the Text property of the tested control.
		/// </summary>
		public virtual string Text
		{
			get { return Control.Text; }
		}

		///<summary>
		/// Constructs a new <see cref="ControlTester"/> from the given tester
		/// for the control as the specified index.
		///</summary>
		public ControlTester(ControlTester tester, int index)
		{
			InitFromTester(tester, index);
		}

		///<summary>
		/// Default constructor for generic support.
		///</summary>
		protected ControlTester(){}

		/// <summary>
		/// Initializes a <see cref="ControlTester"/> from an existing
		/// ControlTester, using the specified control index.
		/// </summary>
		protected void InitFromTester(ControlTester tester, int controlIndex) 
		{
			if (controlIndex < 0)
				throw new ArgumentOutOfRangeException("controlIndex", controlIndex, "Should not have index < 0");

			this.index = controlIndex;
			this.form = tester.form;
			this.formName = tester.formName;
			this.name = tester.name;
		}

		#region Mouse

		/// <summary>
		///   Returns a <see cref="NUnit.Extensions.Forms.MouseController"/> that
		///   can be used with the <see cref="Control">control under test</see>.
		///   
		///   It would be better to use the MouseController on the base test class so
		///   that you don't have to worry about disposing it after your test.  I think
		///   this may be marked obsolete soon.
		/// </summary>
		/// <returns>
		///   A <see cref="NUnit.Extensions.Forms.MouseController"/>.
		/// </returns>
		/// <remarks>
		///   <b>MouseController</b> returns a new instance of a <see cref="NUnit.Extensions.Forms.MouseController"/>
		///   that can be used with the <see cref="Control">control under test</see>.
		///   All <see cref="NUnit.Extensions.Forms.MouseController.Position">mouse positions</see> are relative
		///   the control.
		///   <para>
		///   The returned <b>MouseController</b> must be <see cref="NUnit.Extensions.Forms.MouseController.Dispose">disposed</see>
		///   to restore the mouse settings prior to the testing; which can be accomplished with the <c>using</c>
		///   statement.
		///   </para>
		/// </remarks>
		/// <example>
		/// <code>
		/// TextBoxTester textBox = new TextBoxTester("myTextBox");
		/// using (MouseController mouse = textBox.MouseController())
		/// {
		///   mouse.Position = new PointF(1,1);
		///   mouse.Drag(30,1);
		/// }
		/// </code>
		/// </example>
		[Obsolete()]
		public MouseController MouseController()
		{
			return new MouseController(this);
		}

		#endregion

		/// <summary>
		/// The underlying <see cref="Control"/> for this tester.
		/// </summary>
		protected internal Control Control
		{
			get { return GetControlFinder().Find(index); }
		}

		/// <summary>
		/// The Control being tested.
		/// </summary>
		protected override object theObject
		{
			get { return Control; }
		}

		private ControlFinder GetControlFinder()
		{
			if (form != null)
			{
				//may have dynamically added controls.  I am not saving this.
				return new ControlFinder(name, form);
			}
			else if (formName != null)
			{
				return new ControlFinder(name, new FormFinder().Find(formName));
			}
			else
			{
				return new ControlFinder(name);
			}
		}

		/// <summary>
		/// Calls EndCurrentEdit on this control's data binding for the given property.
		/// </summary>
		/// <param name="propertyName"></param>
		protected override void DoAfterSetProperty(string propertyName)
		{
			EndCurrentEdit(propertyName);
		}
	}
}