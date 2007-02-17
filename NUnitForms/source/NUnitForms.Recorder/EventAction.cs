using System;
using System.Text;

namespace NUnit.Extensions.Forms.Recorder
{
	///<summary>
	/// Recordable action for an Event.
	///</summary>
	public class EventAction : Action
	{
		private string methodName;

		private object[] args;

		private string comment;

		public string MethodName
		{
			get { return methodName; }
		}

		public object[] Args
		{
			get { return args; }
		}

		public string Comment
		{
			set { comment = " //" + value; }
		}

		///<summary>
		/// Constructs a new EventAction.
		///</summary>
		public EventAction(string methodName, params object[] args)
		{
			this.methodName = methodName;

			if (args.Length > 0 && args[0] is Array)
			{
				this.args = (object[]) args[0];
			}
			else
			{
				this.args = args;
			}

			comment = string.Empty;
		}

		private string NoSpace(string name)
		{
			return name.Replace(" ", "");
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}({2});{3}", NoSpace(Definition.VarName), methodName, GetArgs(args), comment);
		}

		private static string GetArgs(object[] newArgs)
		{
			StringBuilder sb = new StringBuilder();
			bool first = true;
			foreach (object arg in newArgs)
			{
				if (!first)
				{
					sb.Append(", ");
				}
				first = false;
				sb.Append(FormatArgument(arg));
			}
			return sb.ToString();
		}

		private static string FormatArgument(object arg)
		{
			if (arg is string)
			{
				return string.Format("\"{0}\"", arg);
			}
			return arg.ToString();
		}
	}
}