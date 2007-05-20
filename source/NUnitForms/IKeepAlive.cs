namespace NUnit.Extensions.Forms
{
	/// <summary>
	/// This interface allows a Form to signal that it should not be disposed between tests.
	/// </summary>
	public interface IKeepAlive
	{
		/// <summary>
		/// Gets a value which determines if this object should be kept alive between tests.
		/// </summary>
		bool KeepAlive { get; }
	}

	/// <summary>
	/// Static helper class for <see cref="IKeepAlive"/> management.
	/// </summary>
	public static class KeepAlive
	{
		///<summary>
		/// Tests if the given object should be kept alive.
		///</summary>
		///<param name="obj">The object to test.</param>
		///<returns>True if obj should be kept alive.</returns>
		public static bool ShouldKeepAlive(object obj)
		{
			return obj is IKeepAlive && (obj as IKeepAlive).KeepAlive;
		}
	}
}