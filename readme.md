ApprovalTests
====
[![Backers on Open Collective](https://opencollective.com/approvaltestsnet/backers/badge.svg)](#backers)

 [![Sponsors on Open Collective](https://opencollective.com/approvaltestsnet/sponsors/badge.svg)](#sponsors) 

Capturing Human Intelligence - ApprovalTests is an open source assertion/verification library to aid unit testing.

It is compatible with most .Net unit testing frameworks (Nunit, MsTest, Xunit, MBUnit)

What can it be used for?
---

Approval Tests can be used for verifying objects that require more than a simple assert. They also come prepackaged with utilities for some common .Net scenarios including


- Dictionaries & Collections
- Long Strings
- Log Files
- Asp.Net
- Asp.Net Mvc
- Winforms
- Wpf
- Entity Framework
- Rdlc reports

[Video Tutorials](http://www.youtube.com/playlist?list=PL0C32F89E8BBB5368)
---

You can watch a bunch of short videos on getting started and [using ApprovalTests in .Net](http://www.youtube.com/playlist?list=PL0C32F89E8BBB5368) at YouTube

Podcasts
---
If you prefer auditory learning, you might enjoy the following podcast 

- [This Agile Life](http://www.thisagilelife.com/46/)
- [Hanselminutes](http://www.hanselminutes.com/360/approval-tests-with-llewellyn-falco)
- [Herding Code](http://www.developerfusion.com/media/122649/herding-code-117-llewellyn-falcon-on-approval-tests/)
- [The Watir Podcast](http://watirpodcast.com/podcast-53/)


Available on NuGet
---
[Install-Package ApprovalTests](http://nuget.org/packages/ApprovalTests)

Nightly (CI) Builds available at myget.org: 

[![ApprovalTests Nightly Build Status](https://www.myget.org/BuildSource/Badge/approvaltests?identifier=c56b6e36-ea68-4965-8cd8-e7033c66e38e "Build Status")](https://www.myget.org/gallery/approvaltests)

[ApprovalTests on the MyGet Gallery](https://www.myget.org/gallery/approvaltests)
Note: Select "Include Prerelease" instead of "Stable Only" (`-IncludePrerelease` in powershell)

Examples
---
[Sample Code](https://github.com/approvals/ApprovalTests.Net/tree/master/ApprovalDemos/GettingStartedDemos)

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

Approved File Artifacts
---

The `*.approved.*` files must be checked into source your source control. ApprovalTests now ignores line endings by default (so you can remove `*.approved.* binary` from your .gitattributes file if you added previously).

If you would like to verify line endings, simply add `[assembly: IgnoreLineEndingsAttribute(false)]` to your AssemblyInfo.cs

Do not add `*.received.*` files to your source control (they are transitory, and some SCMs like TFS will lock them or mark them read-only, which will break every dependent test).



More Info
---

- [Website](http://approvaltests.sourceforge.net/)
- [Blog](http://blog.approvaltests.com/)
- [Getting Started Doc](https://github.com/approvals/ApprovalTests.Net/blob/master/build/Documentation/Approval%20Tests%20-%20Getting%20Started.pdf?raw=true)

## Credits

### Contributors

This project exists thanks to all the people who contribute. [[Contribute](CONTRIBUTING.md)].

<a href="graphs/contributors"><img src="https://opencollective.com/approvaltestsnet/contributors.svg?width=890&button=false" /></a>

### Backers

Thank you to all our backers! 🙏 [[Become a backer](https://opencollective.com/approvaltestsnet#backer)]

<a href="https://opencollective.com/approvaltestsnet#backers" target="_blank"><img src="https://opencollective.com/approvaltestsnet/backers.svg?width=890"></a>

### Sponsors

Support this project by becoming a sponsor. Your logo will show up here with a link to your website. [[Become a sponsor](https://opencollective.com/approvaltestsnet#sponsor)]

<a href="https://opencollective.com/approvaltestsnet/sponsor/0/website" target="_blank"><img src="https://opencollective.com/approvaltestsnet/sponsor/0/avatar.svg"></a>


	
## LICENSE
[Apache 2.0 License](https://github.com/SignalR/SignalR/blob/master/LICENSE.md)


Questions?
---

ask on twitter: [@LlewellynFalco](https://twitter.com/#!/llewellynfalco) or #ApprovalTests
