﻿namespace ApprovalTests.Set;

public static class SetApprovals
{
    static IEnumerable<T> GetSorted<T>(IEnumerable<T> enumerable) where T:IComparable<T> =>
        enumerable.OrderBy(e => e);

    public static void VerifySet<T>(IEnumerable<T> enumerable, Func<T, string> formatter) where T : IComparable<T> =>
        Approvals.VerifyAll(GetSorted(enumerable), formatter);

    public static void VerifySet<T>(IEnumerable<T> enumerable, string label) where T : IComparable<T> =>
        Approvals.VerifyAll(GetSorted(enumerable), label);

    public static void VerifySet<T>(IEnumerable<T> enumerable, string label, Func<T, string> formatter) where T : IComparable<T> =>
        Approvals.VerifyAll(GetSorted(enumerable), label, formatter);

    public static void VerifySet<T>(string header, IEnumerable<T> enumerable, string label) where T : IComparable<T> =>
        Approvals.VerifyAll(header, GetSorted(enumerable), label);

    public static void VerifySet<T>(string header, IEnumerable<T> enumerable, Func<T, string> formatter) where T : IComparable<T> =>
        Approvals.VerifyAll(header, GetSorted(enumerable), formatter);

    public static void VerifyFileAsSet(string filename, Func<string, string> scrubber)
    {
        var lines = File.ReadAllLines(filename);
        var scrubbed = lines.Select(scrubber);
        VerifySet(scrubbed, s => s);
    }

    public static void VerifyFileAsSet(string filename) =>
        VerifyFileAsSet(filename, Scrubber.ScrubberUtils.NO_SCRUBBER);
}