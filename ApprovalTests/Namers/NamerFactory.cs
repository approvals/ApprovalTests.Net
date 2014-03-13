using System;
using System.ComponentModel;

namespace ApprovalTests.Namers
{
    public class NamerFactory
    {
        public static ApprovalResults ApprovalResults = new ApprovalResults();

        public static string AdditionalInformation { get; set; }

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

        public static void AsEnvironmentSpecificTest(Func<string> environmentLabeler)
        {
					 throw new Exception("Boo");
            AdditionalInformation = environmentLabeler();
        }

        public static void Clear()
        {
            AdditionalInformation = null;
        }
    }
}