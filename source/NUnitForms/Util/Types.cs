using System;
using System.Collections.Generic;
using System.Reflection;

namespace NUnit.Extensions.Forms
{
	/// <summary>
	/// Additional methods for working with Types and other Reflection objects.
	/// </summary>
	public static class Types
	{
		///<summary>
		/// Returns true if the given type has any declared public properties.
		///</summary>
		public static bool HasPublicProperties(Type type)
		{
			PropertyInfo info =
				type.GetProperty("Properties",
				                 BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			return info != null;
		}

		///<summary>
		/// Retrives a list of names of events that the given type publishes.
		///</summary>
		///<param name="type"></param>
		///<returns></returns>
		public static ICollection<string> GetEventNames(Type type)
		{
			List<string> list = new List<string>();
			foreach(EventInfo info in type.GetEvents())
			{
				list.Add(info.Name);
			}
			return list;
		}
	}
}
