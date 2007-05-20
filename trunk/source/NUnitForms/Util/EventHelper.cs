using System;
using System.Reflection;

namespace NUnit.Extensions.Forms
{
	///<summary>
	/// Methods for firing events via reflection.
	///</summary>
	public static class EventHelper
	{
		///<summary>
		/// Fires the named event on the given object using the object's "OnEventName" method.
		///</summary>
		/// <remarks>
		/// <para>
		/// By convention, an event named "MyEvent(object sender, MyEventArgs e)" should have a virtual protected
		/// method "OnMyEvent(MyEventArgs e)" that actually calls any attached event handler.
		/// </para>
		/// <para>
		/// This method assumes that the target event has been implemented with this pattern.
		/// </para>
		/// </remarks>
		///<param name="targetObject">The object raising the event.</param>
		///<param name="eventName">The name of the event to raise.</param>
		public static void RaiseEvent(object targetObject, string eventName)
		{
			MethodInfo minfo = targetObject.GetType().GetMethod("On" + eventName,
			                                                    BindingFlags.Instance | BindingFlags.Public |
			                                                    BindingFlags.NonPublic);
			ParameterInfo[] param = minfo.GetParameters();
			Type parameterType = param[0].ParameterType;
			minfo.Invoke(targetObject, new Object[] {Activator.CreateInstance(parameterType)});
		}

		///<summary>
		/// Fires the named event on the given object using the object's "OnEventName" method.
		///</summary>
		/// <remarks>
		/// <para>
		/// By convention, an event named "MyEvent(object sender, MyEventArgs e)" should have a virtual protected
		/// method "OnMyEvent(MyEventArgs e)" that actually calls any attached event handler.
		/// </para>
		/// <para>
		/// This method assumes that the target event has been implemented with this pattern.
		/// </para>
		/// </remarks>
		///<param name="targetObject">The object raising the event.</param>
		///<param name="eventName">The name of the event to raise.</param>
		///<param name="args">A list of arguments to pass to the EventArgs-derived parameter's constructor.</param>
		public static void RaiseEvent(object targetObject, string eventName, object[] args)
		{
			MethodInfo minfo = targetObject.GetType().GetMethod("On" + eventName,
			                                                    BindingFlags.Instance | BindingFlags.Public |
			                                                    BindingFlags.NonPublic);
			ParameterInfo[] param = minfo.GetParameters();
			Type parameterType = param[0].ParameterType;
			minfo.Invoke(targetObject, new object[] {Activator.CreateInstance(parameterType, args)});
		}

		///<summary>
		/// Fires the named event on the given object using the object's "OnEventName" method.
		///</summary>
		/// <remarks>
		/// <para>
		/// By convention, an event named "MyEvent(object sender, MyEventArgs e)" should have a virtual protected
		/// method "OnMyEvent(MyEventArgs e)" that actually calls any attached event handler.
		/// </para>
		/// <para>
		/// This method assumes that the target event has been implemented with this pattern.
		/// </para>
		/// </remarks>
		///<param name="targetObject">The object raising the event.</param>
		///<param name="eventName">The name of the event to raise.</param>
		///<param name="arg">The EventArgs-derived class to pass to this event.</param>
		public static void RaiseEvent(object targetObject, string eventName, EventArgs arg)
		{
			MethodInfo minfo = targetObject.GetType().GetMethod("On" + eventName,
			                                                    BindingFlags.Instance | BindingFlags.Public |
			                                                    BindingFlags.NonPublic);
			minfo.Invoke(targetObject, new object[] {arg});
		}

		///<summary>
		/// Calls the given method on the target object with the specified arguments.
		///</summary>
		///<returns>The return value of the called method, if any.</returns>
		public static object Call(object targetObject, string methodName, object[] args)
		{
			Type[] types = new Type[args.Length];
			for (int i = 0; i < types.Length; i++)
			{
				types[i] = args[i].GetType();
			}
			MethodInfo minfo =
				targetObject.GetType().GetMethod(methodName,
				                                 BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
				                                 null, types, null);
			return minfo.Invoke(targetObject, args);
		}
	}
}