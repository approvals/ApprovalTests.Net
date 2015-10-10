using System;
using ApprovalUtilities.CallStack;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.SimpleLogger.Writers;
using ApprovalUtilities.Utilities;
using System.Linq;

namespace ApprovalUtilities.SimpleLogger
{
	public class LoggerInstance
	{
		public IAppendable Writer = new MultiWriter(new ConsoleWriter(),new DebugerWriter());
		private int indent = 0;
		public int TabSize = 4;
		private bool showMarkerIn = true;
		private bool showVariables = true;
		private bool showEvents = true;
		private bool showSql = true;
		private bool showTimestamp = true;
		private bool showTimeDifference = true;
		private ILoader<DateTime> clock = new Clock();
		private DateTime lastTime = DateTime.Now;

		public StringBuilderLogger LogToStringBuilder()
		{
			Show(markerIn: true, variables: true, events: true, sql: true, timestamp: false, timeDifference: false);
			var log = new StringBuilderLogger();
			Writer = log;
			return log;
		}

		public void MarkerIn()
		{
			if (!showMarkerIn)
			{
				return;
			}
			var method = GetCallingMethod();
			Write("=> " + method);
			indent += TabSize;
		}

		public void MarkerOut()
		{
			if (!showMarkerIn)
			{
				return;
			}

			var method = GetCallingMethod();
			indent -= TabSize;
			Write("<= " + method);
		}

		public string GetCallingMethod()
		{
			var outsideCallingMethod = new Caller().Methods.First(m => m.DeclaringType.Namespace != this.GetType().Namespace);
			return outsideCallingMethod.ToStandardString();
		}

		private void Write(string text)
		{
			var time = showTimestamp ? clock.Load() + " " : "";
			var difference = "";
			if (showTimeDifference)
			{
				var t = clock.Load();
				var diff = t - lastTime;
				lastTime = t;
				difference = string.Format("~{0:000000}ms ", diff.TotalMilliseconds);
			}

			var message = text.Replace(Environment.NewLine, Environment.NewLine + "\t");
			Writer.AppendLine(time + difference + GetIndentation() + message);
		}

		private string GetIndentation()
		{
			return "".PadLeft(indent, ' ');
			//				.Substring(0, indent);
		}

		public string Event(string message, params object[] items)
		{
			if (showEvents)
			{
				Write(string.Format("Event: " + message, items));
			}
			return message;
		}

		public string Message(string message)
		{
			if (showEvents)
			{
				Write("Message: " + message);
			}
			return message;
		}

		public void Variable(string name, object value)
		{
			if (showVariables)
			{
				Write("Variable: " + name + " = '" + value + "'");
			}
		}

		public string Sql(string sql)
		{
			if (showSql)
			{
				Write("Sql: " + sql);
			}
			return sql;
		}

		public string Miscellaneous(string label, string message)
		{
			Write("{0}: {1}".FormatWith(label, message));
			return message;
		}

		public void Warning(Exception except, params string[] additional)
		{
			Writer.AppendLine(except.FormatError(additional));
		}

		public string Warning(string format, params object[] data)
		{
			PrintWarning(string.Format(format, data));
			return format.FormatWith(data);
		}

		private void PrintWarning(params string[] lines)
		{
			Writer.AppendLine(ExceptionUtilities.FormatAsError(lines));
		}

		public void Show(bool markerIn = true, bool variables = true, bool events = true, bool sql = true,
						 bool timestamp = true, bool timeDifference = true)
		{
			showMarkerIn = markerIn;
			showVariables = variables;
			showEvents = events;
			showSql = sql;
			showTimestamp = timestamp;
			showTimeDifference = timeDifference;
		}

		public void UseTimer(ILoader<DateTime> timeLoader)
		{
			lastTime = timeLoader.Load();
			clock = timeLoader;
		}
	}
}