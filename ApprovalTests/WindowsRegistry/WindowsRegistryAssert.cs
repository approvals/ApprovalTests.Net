using System;
using ApprovalUtilities.Utilities;
using Microsoft.Win32;

namespace ApprovalTests.WindowsRegistry
{
	public static class WindowsRegistryAssert
	{
		public static void HasDword(RegistryKey registryKey, string keyName, string valueName, Int32 expectedValue,
		                            string failureMessage)
		{
			var key = registryKey.OpenSubKey(keyName);

			int actualValue = key == null ? 0 : (int) key.GetValue(valueName, 0);

			if (actualValue != expectedValue)
			{
				string message = "{0}\r\nMust set DWORD {1}\\{2} : {3} = {4}.".FormatWith(failureMessage, registryKey.Name, keyName, valueName,
				                                                                     expectedValue);
				throw new Exception(message);
			}
		}

		public static void HasDword(string keyName, string valueName, Int32 expectedValue, string failureMessage)
		{
			HasDword(Registry.CurrentUser, keyName, valueName, expectedValue, failureMessage);
		}
	}
}