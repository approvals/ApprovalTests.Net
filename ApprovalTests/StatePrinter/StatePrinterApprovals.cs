using System;
using StatePrinter.Configurations;

namespace ApprovalTests.StatePrinter
{
	public static class StatePrinterApprovals
	{
		private static Func<Configuration> defaultConfiguration = ConfigurationHelper.GetStandardConfiguration;

		public static void Verify(object source, string rootName = "Root")
		{
			Verify(source, GetDefaultConfiguration(), rootName);
		}


		public static void Verify(object source, Configuration configuration, string rootName = "Root")
		{
			var printer = new global::StatePrinter.StatePrinter(configuration);
			string printedResult = printer.PrintObject(source, rootName);
			Approvals.Verify(printedResult);
		}

		public static Configuration GetDefaultConfiguration()
		{
			return defaultConfiguration();
		}

		public static void RegisterDefaultConfiguration(Func<Configuration> configurationProducer)
		{
			defaultConfiguration = configurationProducer;
		}

	}
}