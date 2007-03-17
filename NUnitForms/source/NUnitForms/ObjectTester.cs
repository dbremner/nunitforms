using System;
using System.Reflection;

namespace NUnit.Extensions.Forms
{
	///<summary>
	/// Abstract base class for control and component testers that
	/// handles getting and setting properties and fields via reflection.
	///</summary>
	public abstract class ObjectTester
	{
		/// <summary>
		/// Derived testers must overtide this method to provide the object being tested.
		/// </summary>
		protected abstract object theObject { get; }

		/// <summary>
		/// Convenience accessor / mutator for any nonsupported property on a control
		/// to test.
		/// </summary>
		/// <example>
		/// ControlTester t = new ControlTester("t");
		/// t["Text"] = "a";
		/// AssertEqual("a", t["Text"]);
		/// </example>
		/// 
		public object this[string propertyName]
		{
			get
			{
				PropertyInfo prop = GetPropertyInfo(propertyName);
				if (prop != null)
				{
					return prop.GetValue(theObject, null);
				}
				else
				{
					FieldInfo field = GetFieldInfo(propertyName);
					if (field != null)
						return field.GetValue(theObject);
				}

				return null;
			}
			set
			{
				PropertyInfo prop = GetPropertyInfo(propertyName);
				if (prop != null)
				{
					prop.SetValue(theObject, value, null);
					DoAfterSetProperty(propertyName);
				}
				else
				{
					FieldInfo field = GetFieldInfo(propertyName);
					if (field != null)
						field.SetValue(theObject, value);
				}

			}
		}

		/// <summary>
		/// Called after this[string] is used to set a property value.
		/// Typically calls "EndCurrentEdit" on the object's data binding binding for that property.
		/// </summary>
		protected virtual void DoAfterSetProperty(string propertyName) {}

		private FieldInfo GetFieldInfo(string fieldName)
		{
			return
				theObject.GetType().GetField(fieldName,
				                             BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		private PropertyInfo GetPropertyInfo(string propertyName)
		{
			return
				theObject.GetType().GetProperty(propertyName,
				                                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>
		/// Simulates firing of an event by the control being tested.
		/// </summary>
		/// <param name="eventName">The name of the event to fire.</param>
		/// <param name="args">The optional arguments required to construct the EventArgs for the specified event.</param>
		public void FireEvent(string eventName, params object[] args)
		{
			EventHelper.RaiseEvent(theObject, eventName, args);
		}

		/// <summary>
		/// Simulates firing of an event by the control being tested.
		/// </summary>
		/// <param name="eventName">The name of the event to fire.</param>
		/// <param name="arg">The EventArgs object to pass as a parameter on the event.</param>
		public void FireEvent(string eventName, EventArgs arg)
		{
			EventHelper.RaiseEvent(theObject, eventName, arg);
		}

		/// <summary>
		/// Simulates firing of an event by the control being tested.
		/// </summary>
		/// <param name="eventName">The name of the event to fire.</param>
		public void FireEvent(string eventName)
		{
			EventHelper.RaiseEvent(theObject, eventName);
		}

		///<summary>
		/// Raises this object's Click event.
		///</summary>
		public virtual void Click() {}
		
		/// <summary>
		/// Convenience method invoker for any nonsupported method on a control to test
		/// </summary>
		/// <param name="methodName">the name of the method to invoke</param>
		/// <param name="args">the arguments to pass into the method</param>
		public object Invoke(string methodName, params object[] args)
		{
			return EventHelper.Call(theObject, methodName, args);
		}
	}
}