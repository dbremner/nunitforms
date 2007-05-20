using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms
{
	///<summary>
	/// A strongly-typed <see cref="ControlTester"/> for the given control type.
	///</summary>
	///<typeparam name="TControl">The type of control to test.</typeparam>
	///<typeparam name="TThis">The type of control tester being defined.</typeparam>
	public class ControlTester<TControl, TThis> : ControlTester, IEnumerable<TThis> 
		where TControl : Control
		where TThis : ControlTester<TControl, TThis>, new()
	{
		///<summary>
		/// Constructs a new <see cref="ControlTester"/> from the given tester
		/// for the control as the specified index.
		///</summary>
		public ControlTester(ControlTester tester, int index) : base(tester, index) {}

		/// <summary>
		/// Creates a ControlTester that will test controls with the specified name.
		/// </summary>
		/// <remarks>
		/// If the name is unique, you can operate on the tester directly, otherwise
		/// you should use the indexer or Enumerator properties to access each separate
		/// control.</remarks>
		/// <param name="name">The name of the control to test.</param>
		public ControlTester(string name) : base(name) {}

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
		public ControlTester(string name, Form form) : base(name, form) {}

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
		public ControlTester(string name, string formName) : base(name, formName) {}

		///<summary>
		/// Default constructor for generic support.
		///</summary>
		public ControlTester() {}

		///<summary>
		/// Provides typed access to the underlying <typeparamref name="TControl"/> control.
		///</summary>
		public virtual TControl Properties
		{
			get { return (TControl) Control; }
		}

		///<summary>
		/// Creates a new <typeparamref name="TThis"/> from the
		/// current control tester, for the control at the specified index.
		///</summary>
		new public virtual TThis this[int controlIndex]
		{
			get
			{
				TThis newTester = new TThis();
				newTester.InitFromTester(this, controlIndex);
				return newTester;
			}
		}

		///<summary>
		///Returns an enumerator that iterates through the collection.
		///</summary>
		///
		///<returns>
		///A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
		///</returns>
		///<filterpriority>1</filterpriority>
		new public IEnumerator<TThis> GetEnumerator()
		{
			List<TThis> items = new List<TThis>();
			for(int i=0; i < this.Count; i++)
			{
				items.Add(this[i]);
			}
			return items.GetEnumerator();
		}
	}
}