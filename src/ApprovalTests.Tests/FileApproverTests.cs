﻿[TestFixture]
public class FileApproverTests
{
    [Test]
    public void TestFailureDueToMissingApproval() =>
        AssertApprover("a.txt", "non_existing_file.txt", false);

    [Test]
    public void TestFailureDueToMismatch() =>
        AssertApprover("a.txt", "b.txt", false);

    [Test]
    public void TestSuccess() =>
        AssertApprover("a.txt", "a.txt", true);

    [Test]
    public void LineEndingsAreIgnored()
    {
        var basePath = PathUtilities.GetDirectoryForCaller();
        var approvedFile = basePath + "UnixLineEndings.txt";
        var receivedFile = basePath + "WindowsLineEndings.txt";
        File.WriteAllText(approvedFile, "Foo\nBar");
        File.WriteAllText(receivedFile, "Foo\r\nBar");
        var fileApprover = new FileApprover(null, null, true).Approve(approvedFile, receivedFile);
        ClassicAssert.IsNull(fileApprover);
    }

    [Test]
    public void LineEndingAreNotIgnored()
    {
        var basePath = PathUtilities.GetDirectoryForCaller();
        var approvedFile = basePath + "UnixLineEndings.txt";
        var receivedFile = basePath + "WindowsLineEndings.txt";
        File.WriteAllText(approvedFile, "Foo\nBar");
        File.WriteAllText(receivedFile, "Foo\r\nBar");
        var fileApprover = new FileApprover(null, null).Approve(approvedFile, receivedFile);
        ClassicAssert.IsInstanceOf<ApprovalMismatchException>(fileApprover);
    }

    static void AssertApprover(string receivedFile, string approvedFile, bool expected)
    {
        var basePath = PathUtilities.GetDirectoryForCaller();
        var fileApprover = new FileApprover(null, null).Approve(basePath + approvedFile, basePath + receivedFile);
        ClassicAssert.AreEqual(expected, fileApprover == null);
    }
}