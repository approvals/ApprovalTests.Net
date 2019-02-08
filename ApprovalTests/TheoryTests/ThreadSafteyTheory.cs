using System;
using System.Collections.Concurrent;
using System.Linq;
using ApprovalTests.Utilities;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.TheoryTests
{
    public class ThreadSafteyTheory
    {
        public static void VerifyNoRaceConditions<T>(int times, Func<T> caseGenerator, Func<T, string> caseString, Func<T, object> possibleRaceConditonFunction, Func<T, object> knownGoodFunction)
        {
            var n1 = new ConcurrentBag<string>();
            var n2 = new ConcurrentBag<string>();
            var count = Enumerable.Range(0, times).AsParallel().WithDegreeOfParallelism(16).Select(i =>
            {
                var inputs = caseGenerator();
                var text = caseString(inputs);
                n1.Add(text + possibleRaceConditonFunction(inputs));
                n2.Add(text + knownGoodFunction(inputs));
                return 1;
            }).Sum();

            CompareArrays(n1.ToArray(), "Race Condition Function", n2.ToArray(), "Thread Safe Function");
        }

        public static void CompareArrays<T>(T[] n1, string label1, T[] n2, string label2)
        {
            Array.Sort(n1);
            Array.Sort(n2);
            var failed = n1.Length != n2.Length;
            for (var i = 0; i < n1.Length && !failed; i++)
            {
                if (!n1[i].Equals(n2[i]))
                {
                    failed = true;
                }
            }

            if (failed)
            {
                var method2 = ToText(n2, label2);
                var method1 = ToText(n1, label1);
                method2.DiffWith(method1);
                throw new Exception("Race condition detected");
            }
        }

        private static string ToText<T>(T[] n2, string label2)
        {
            return label2 + "\n" + n2.JoinStringsWith(t => "" + t, "\n");
        }
    }
}