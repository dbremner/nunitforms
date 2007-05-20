using System.Collections.Generic;

namespace NUnit.Extensions.Forms.Recorder
{
	/// <summary>
	/// The public API for all collapsing processers.
	/// </summary>
	public interface ICollapsingProcessor
	{
		/// <summary>
		/// Process the given actions, possibly collapsing adjacent ones.
		/// </summary>
		/// <param name="actions">The list of actions to process.</param>
		/// <returns>The collapsed list of actions.</returns>
		ICollection<Action> Process(ICollection<Action> actions);

		/// <summary>
		/// Returns true if the given actions can be collapsed.
		/// </summary>
		/// <param name="action1">The earlier event to test.</param>
		/// <param name="action2">The latter event to test.</param>
		/// <returns>True if these events can be collapsed; else false.</returns>
		bool CanCollapse(EventAction action1, EventAction action2);
	}
}