using System;

namespace ApprovalTests.Namers
{
    public class NamerFactory
    {
        public static string AdditionalInformation { get; set; }

        public static ApprovalResults ApprovalResults = new ApprovalResults();

        [Obsolete("Use ApprovalResults.UniqueForMachineName instead.")]
        public static void AsMachineSpecificTest()
        {
            ApprovalResults.UniqueForMachineName();
        }

        public static void AsMachineSpecificTest(Func<string> environmentLabeler)
        {
            AdditionalInformation = environmentLabeler();
        }

        public static void Clear()
        {
            AdditionalInformation = null;
        }
    }
}