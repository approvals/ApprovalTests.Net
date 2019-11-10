using System;
using System.Threading;

namespace ApprovalTests.Namers
{
    public class NamerFactory
    {
        public static ApprovalResults ApprovalResults = new ApprovalResults();

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

        public static IDisposable AsEnvironmentSpecificTest(Func<string> environmentLabeler)
        {
            if (AdditionalInformation == null)
            {
                AdditionalInformation = environmentLabeler();
            }
            else
            {
                AdditionalInformation += "." + environmentLabeler();
            }

            return new EnvironmentSpecificCleanUp();
        }

        public static void Clear()
        {
            AdditionalInformation = null;
        }
    }
}