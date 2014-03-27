using System;
using ApprovalTests.Core;

namespace ApprovalTests.Writers
{
	public class WriterFactory
	{
		private static Func<string, IApprovalWriter> TextWriterCreator = (s) => new ApprovalTextWriter(s);
		public static void SetTextWriterCreator(Func<String, IApprovalWriter> textWriterCreator)
		{
			TextWriterCreator = textWriterCreator;
		}
		public static IApprovalWriter CreateTextWriter(String data)
		{
			return TextWriterCreator(data);
		}
		private static Func<string,string, IApprovalWriter> TextWriterWithExtensionCreator = (s,e) => new ApprovalTextWriter(s,e);
		public static void SetTextWriterCreator(Func<String, String, IApprovalWriter> textWriterWithExtensionCreator)
		{
			TextWriterWithExtensionCreator = textWriterWithExtensionCreator;
		}
		public static IApprovalWriter CreateTextWriter(string data, string extensionWithoutDot)
		{
			return TextWriterWithExtensionCreator(data, extensionWithoutDot);
		}
	}
}