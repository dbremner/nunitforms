using System;

namespace NUnit.Extensions.Forms
{
	/// <summary>
	/// Defines a base class for exceptions generated when testing forms.
	/// </summary>
	public class FormsTestAssertionException : Exception
	{
		///<summary>
		/// Constructs a new <see cref="FormsTestAssertionException"/> with the given message.
		///</summary>
		public FormsTestAssertionException(string message) : base(message) {}

		///<summary>
		/// Constructs a new <see cref="FormsTestAssertionException"/> with the given message
		/// and inner exception.
		///</summary>
		public FormsTestAssertionException(string message, Exception ex)
			: base(message, ex) {}
	}
}