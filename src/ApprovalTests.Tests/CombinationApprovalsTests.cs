using System;
using System.Globalization;
using System.Linq;
using ApprovalTests.Combinations;
using NUnit.Framework;

namespace ApprovalTests.Tests;

public class CombinationApprovalsTests
{
    [Test]
    [SetCulture("es-ES")]
    public void ArgsShouldBeReportedInInvariantCulture()
    {
        var dateTime = new DateTime(2000, 5, 22, 13, 43, 21);
        var result = CombinationApprovals.GetApprovalString(_ => "test", Enumerable.Repeat(dateTime, 1));
        var invariantDate = dateTime.ToString(CultureInfo.InvariantCulture);
        StringAssert.Contains(invariantDate, result);
    }
}