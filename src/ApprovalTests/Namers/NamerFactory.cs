using System;
using System.Threading;

namespace ApprovalTests.Namers
{
    public static class NamerFactory
    {
        static AsyncLocal<string> additionalInformation = new AsyncLocal<string>();

        public static string AdditionalInformation
        {
            get => additionalInformation.Value;
            set => additionalInformation.Value = value;
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = "ApprovalResults.UniqueForMachineName")]
        public static void AsMachineSpecificTest()
        {
            ApprovalResults.UniqueForMachineName();
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = nameof(AsEnvironmentSpecificTest))]
        public static void AsMachineSpecificTest(Func<string> environmentLabeler)
        {
            AsEnvironmentSpecificTest(environmentLabeler);
        }

        public static IDisposable AsEnvironmentSpecificTest(string label)
        {
            if (AdditionalInformation == null)
            {
                AdditionalInformation = label;
            }
            else
            {
                AdditionalInformation += "." + label;
            }

            return new EnvironmentSpecificCleanUp();
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = "AsEnvironmentSpecificTest(string)")]
        public static IDisposable AsEnvironmentSpecificTest(Func<string> environmentLabeler)
        {
            return AsEnvironmentSpecificTest(environmentLabeler());
        }

        public static void Clear()
        {
            AdditionalInformation = null;
        }
    }
}