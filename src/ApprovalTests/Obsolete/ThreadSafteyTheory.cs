using System;

namespace ApprovalTests.TheoryTests
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        ReplacementTypeOrMember = nameof(ThreadSafetyTheory))]
    public class ThreadSafteyTheory
    {
        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = "ThreadSafetyTheory.VerifyNoRaceConditions")]
        public static void VerifyNoRaceConditions<T>(int times, Func<T> caseGenerator, Func<T, string> caseString, Func<T, object> possibleRaceConditonFunction, Func<T, object> knownGoodFunction)
        {
            ThreadSafetyTheory.VerifyNoRaceConditions(times, caseGenerator, caseString, possibleRaceConditonFunction, knownGoodFunction);
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = "ThreadSafetyTheory.CompareArrays")]
        public static void CompareArrays<T>(T[] n1, string label1, T[] n2, string label2)
        {
            ThreadSafetyTheory.CompareArrays(n1, label1, n2, label2);
        }
    }
}