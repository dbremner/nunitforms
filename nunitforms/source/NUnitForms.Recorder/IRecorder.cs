using System;
using System.Reflection;

namespace NUnit.Extensions.Forms.Recorder
{
	/// <summary>
	/// Defines the public interface for all event recorders.
	/// </summary>
	public interface IRecorder
	{
		/// <summary>
		/// Gets the type of object being recorded.
		/// </summary>
		Type RecorderType { get; }

		/// <summary>
		/// Gets the type of the <see cref="ControlTester"/>
		/// being used.
		/// </summary>
		Type TesterType { get; }

		/// <summary>
		/// Gets the <see cref="Listener"/>.
		/// </summary>
		Listener Listener { get; }

		/// <summary>
		/// Returns the event key for the given event name.
		/// </summary>
		FieldInfo EventKey(string name);
	}
}