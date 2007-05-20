using System;

namespace NUnit.Extensions.Forms
{
	///<summary>
	/// Additional methods for working with strings.
	///</summary>
	public static class Strings
	{
		/// <summary>
		/// Removes all spaces from a string.
		/// </summary>
		/// <param name="name">
		/// Remove all spaces from this string.
		/// </param>
		/// <returns>
		/// <list type="bullet">
		/// <item><paramref name="name"/> without spaces.</item>
		/// <item>if <paramref name="name"/> is not effective, returns an empty string</item>
		/// </list>
		/// </returns>
		public static string SafeRemoveSpaces(string name)
		{
			return (name ?? "").Replace(" ", "");
		}
	}
}
