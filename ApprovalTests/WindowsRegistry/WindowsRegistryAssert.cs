using System;
using Microsoft.Win32;

namespace ApprovalTests.WindowsRegistry
{
    public static class WindowsRegistryAssert
    {
        public static void HasDword(string keyName, string valueName, int expectedValue, string failureMessage)
        {
            HasDword(Registry.CurrentUser, keyName, valueName, expectedValue, failureMessage);
        }

        private static void HasDword(RegistryKey registryKey, string keyName, string valueName, int expectedValue, string failureMessage)
        {
            var actualValue = ReadIntKeyValue(registryKey, keyName, valueName);

            if (actualValue != expectedValue)
            {
                var message = $"{failureMessage}\nMust set DWORD {registryKey.Name}\\{keyName} : {valueName} = {expectedValue}.";
                throw new Exception(message);
            }
        }

        private static int ReadIntKeyValue(RegistryKey registryKey, string keyName, string valueName)
        {
            using var key = registryKey.OpenSubKey(keyName);
            return key == null ? 0 : (int) key.GetValue(valueName, 0);
        }
    }
}