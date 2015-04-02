using System;
using System.Linq;
using ApprovalTests.WebApi.MicrosoftHttpClient;
using ApprovalUtilities.CallStack;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.ExceptionalExceptions
{
	public class Exceptional : Exception
	{
		public static T Create<T>(string formattableMessage, params object[] messageParameters) where T : Exception
		{
			return Create<T>(null, formattableMessage, messageParameters);
		}

		public static T Create<T>(Exception causedBy, string formattableMessage, params object[] messageParameters)
			where T : Exception
		{
			Func<string, Exception, T> reflectiveConstructor = (m, e) =>
			{
				var type = typeof (T);
				var constructorInfo = type.GetConstructor(new[] {typeof (string), typeof (Exception)});
				var instance = (T) constructorInfo.Invoke(new object[] {m, e});
				return instance;
			};
			return Create<T>(reflectiveConstructor, causedBy, formattableMessage, messageParameters);
		}

		public static T Create<T>(Func<string, Exception, T> constructor, Exception causedBy, string formattableMessage,
			params object[] messageParameters) where T : Exception
		{
			var message = String.Format(formattableMessage, messageParameters);
			var uid = GenerateUniqueId<T>();
			var tldr = GetTlDr(uid);
			var completeMessage = message + tldr;
			var exception = constructor(completeMessage, causedBy);
			return exception;
		}

		private static string GetTlDr(ExceptionalId uid)
		{
			return "";
			//return new ExceptionalTlDr(uid).Load();
		}

		public static ExceptionalId GenerateUniqueId<T>()
		{
			var callingMethod = new Caller().Methods.First(m => m.DeclaringType.Namespace != typeof (Exceptional).Namespace);
	//		callingMethod.GetParameters()[0].Name
			return new ExceptionalId
			{
				Assembly = callingMethod.DeclaringType.Assembly.FullName,
				Class = callingMethod.DeclaringType.FullName,
				Method = callingMethod.ToStandardString(),
				Exception = typeof (T).FullName,
			};
		}
	}

	public class ExceptionalTlDr : RestQuery<string>
	{
		private readonly ExceptionalId uid;

		public ExceptionalTlDr(ExceptionalId uid)
		{
			this.uid = uid;
		}

		public override string GetQuery()
		{
			throw new NotImplementedException();
		}

		public override string GetBaseAddress()
		{
			throw new NotImplementedException();
		}

		public override string Load()
		{
			throw new NotImplementedException();
		}
	}

	public class ExceptionalId
	{
		public string Assembly { get; set; }
		public string Class { get; set; }
		public string Method { get; set; }
		public string Exception { get; set; }

		public override string ToString()
		{
			return this.WritePropertiesToString();
		}
	}
}