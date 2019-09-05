using System;
using System.ComponentModel;
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

        [Obsolete("Use ApprovalResults.UniqueForMachineName instead.")]
        public static void AsMachineSpecificTest()
        {
            ApprovalResults.UniqueForMachineName();
        }

        [Obsolete("Use AsEnvironmentSpecificTest instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
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

            return new EnviromentSpecificCleanUp();
        }

        public static void Clear()
        {
            AdditionalInformation = null;
        }
    }
}