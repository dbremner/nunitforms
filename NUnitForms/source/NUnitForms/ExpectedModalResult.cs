using System;
using System.Collections.ObjectModel;

namespace NUnit.Extensions.Forms
{
	/// <summary>
	/// This class stores expected and unexpected dialog information.
	/// </summary>
	public class ExpectedModalResult
	{
		/// <summary>
		/// The expected key for this window.
		/// </summary>
		public string ExpectedName;

		/// <summary>
		/// The actual WinForms name of this window, if applicable.
		/// </summary>
		public string ActualName;

		/// <summary>
		/// The actual caption of this window.
		/// </summary>
		public string ActualCaption;

		/// <summary>
		/// True if this window was expected.
		/// </summary>
		public bool WasExpected;

		/// <summary>
		/// True if this window was shown.
		/// </summary>
		public bool WasShown;

		/// <summary>
		/// True if this window was expected and shown, or unexpected and not shown.
		/// </summary>
		public bool Passed
		{
			get { return WasExpected == WasShown; }
		}
	}

	/// <summary>
	/// A collection of <see cref="ExpectedModalResult"/>s.
	/// </summary>
	public class ExpectedModalResultCollection : Collection<ExpectedModalResult>
	{
		/// <summary>
		/// Returns a list of all results in this list that did not pass.
		/// </summary>
		public ExpectedModalResultCollection GetFailures()
		{
			ExpectedModalResultCollection list = new ExpectedModalResultCollection();
			foreach(ExpectedModalResult result in this)
			{
				if (!result.Passed)
					list.Add(result);
			}
			return list;
		}
	}
}
