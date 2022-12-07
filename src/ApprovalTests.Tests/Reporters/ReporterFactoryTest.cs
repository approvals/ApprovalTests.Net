using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters;

[TestFixture]
[UseReporter(typeof(ClassLevelReporter))]
public class ReporterFactoryTest
{
    static IEnumerable<Type> GetSingletonReporterTypes()
    {
        var types = typeof(UseReporterAttribute).Assembly.GetTypes();
        var reporters = types.Where(r => r.GetInterfaces().Contains(typeof(IApprovalFailureReporter)));
        var singletons = reporters.Where(r => r.GetConstructor(Type.EmptyTypes) != null);
        return singletons;
    }

    static void SubMethod()
    {
        Assert.AreEqual(typeof(MethodLevelReporter), Approvals.GetReporter().GetType());
    }

    [Test]
    public void TestClassLevel()
    {
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            Assert.AreEqual(typeof(ClassLevelReporter), Approvals.GetReporter().GetType());
        }
    }

    [Test]
    [UseReporter(typeof(MethodLevelReporter))]
    public void TestMethodOverride()
    {
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            Assert.AreEqual(typeof(MethodLevelReporter), Approvals.GetReporter().GetType());
        }
    }

    [Test]
    [UseReporter(typeof(MethodLevelReporter))]
    public async Task TestAsyncMethodOverride()
    {
        await Task.Delay(1);
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            Assert.AreEqual(typeof(MethodLevelReporter), Approvals.GetReporter().GetType());
        }
    }

    [Test]
    [UseReporter(typeof(MethodLevelReporter))]
    public void TestMethodOverrideWithSubMethod()
    {
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            SubMethod();
        }
    }

    [Test]
    [UseReporter(typeof(MethodLevelReporter), typeof(ClassLevelReporter))]
    public void TestMultipleReporters()
    {
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            Assert.AreEqual(typeof(MultiReporter), Approvals.GetReporter().GetType());
        }
    }

    [Test]
    public void TestSingletonOnAllReporters()
    {
        using (Approvals.SetFrontLoadedReporter(ReportWithoutFrontLoading.INSTANCE))
        {
            var reporters = GetSingletonReporterTypes();
            foreach (var r in reporters) Assert.IsInstanceOf(r, UseReporterAttribute.GetSingleton(r), $"Please add\npublic static readonly {r.FullName} INSTANCE = new {r.FullName}();");
        }
    }
}

public class MethodLevelReporter : IApprovalFailureReporter
{
    public void Report(string approved, string received)
    {
    }
}

public class ClassLevelReporter : IApprovalFailureReporter
{
    public void Report(string approved, string received)
    {
    }
}