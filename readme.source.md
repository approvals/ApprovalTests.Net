# ApprovalTests

[![Build](https://travis-ci.com/approvals/ApprovalTests.Net.svg?branch=master)](https://travis-ci.com/approvals/ApprovalTests.Net) [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0) [![NuGet Status](http://img.shields.io/nuget/v/ApprovalTests.svg?style=flat)](https://www.nuget.org/packages/ApprovalTests/)

Capturing Human Intelligence - ApprovalTests is an open source assertion/verification library to aid unit testing.

It is compatible with most .Net unit testing frameworks (Nunit, MsTest, xUnit, MBUnit)

toc


## What can it be used for?

Approval Tests can be used for verifying objects that require more than a simple assert. They also come prepackaged with utilities for some common .Net scenarios including:

 * Dictionaries & Collections
 * Long Strings
 * Log Files
 * [Asp.Net](https://github.com/approvals/Approvals.Net.Asp)
 * [Asp.Net Mvc](https://github.com/approvals/Approvals.Net.Asp)
 * [Winforms](https://github.com/approvals/ApprovalTests.Net.WinForms)
 * [Wpf](https://github.com/approvals/ApprovalTests.Net.Wpf)
 * [Entity Framework](https://github.com/approvals/ApprovalTests.Net.EntityFramework)
 * [Rdlc reports](https://github.com/approvals/ApprovalTests.Net.Rdlc)


## [Video Tutorials](http://www.youtube.com/playlist?list=PL0C32F89E8BBB5368)

You can watch a bunch of short videos on getting started and [using ApprovalTests in .Net](http://www.youtube.com/playlist?list=PL0C32F89E8BBB5368) at YouTube


## Podcasts

If you prefer auditory learning, you might enjoy the following podcast 

 * [This Agile Life](http://www.thisagilelife.com/46/)
 * [Hanselminutes](http://www.hanselminutes.com/360/approval-tests-with-llewellyn-falco)
 * [Herding Code](http://www.developerfusion.com/media/122649/herding-code-117-llewellyn-falcon-on-approval-tests/)
 * [The Watir Podcast](http://watirpodcast.com/podcast-53/)


## Docs

 * [ApprovalTests](/ApprovalTests/docs/README.md)
 * [ApprovalUtilities](/ApprovalUtilities/docs/README.md)


## Available on NuGet

[Install-Package ApprovalTests](http://nuget.org/packages/ApprovalTests)


## Examples

```c#
[UseReporter(typeof(DiffReporter))]
[TestFixture]
public class SampleTest
{
	[Test]
	public void TestList()
	{
		var names = new[] {"Llewellyn", "James", "Dan", "Jason", "Katrina"};
		Array.Sort(names);
		Approvals.VerifyAll(names, "");
	}
}
```

Will Produce a File

    SampleTest.TestList.received.txt
    [0] = Dan
    [1] = James
    [2] = Jason
    [3] = Katrina
    [4] = Llewellyn

Simply rename this to SampleTest.TestList.**approved**.txt and the test will now pass.


## Approved File Artifacts

The `*.approved.*` files must be checked into source your source control. ApprovalTests now ignores line endings by default (so you can remove `*.approved.* binary` from your .gitattributes file if you added previously).

If you would like to verify line endings, simply add `[assembly: IgnoreLineEndingsAttribute(false)]` to your AssemblyInfo.cs

Do not add `*.received.*` files to your source control (they are transitory, and some SCMs like TFS will lock them or mark them read-only, which will break every dependent test).


## More Info

 * [Website](http://approvaltests.com/)
 * [Blog](https://approvaltests.blogspot.com/)


## Questions?

ask on twitter: [@LlewellynFalco](https://twitter.com/#!/llewellynfalco) or #ApprovalTests
