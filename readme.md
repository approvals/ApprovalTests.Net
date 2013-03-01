ApprovalTests
====

Capturing Human Intelligence - ApprovalTests is an open source assertion/verification library to aid unit testing.

It is compatiable with most .Net unit testing frameworks (Nunit, MsTest, Xunit, MBUnit)

What can it be used for?
---

Approval Tests can be used for verifing objects that require more than a simple assert. They also come prepackaged with utilities for some common .Net scenarios including


- Dictionaries & Collections
- Long Strings
- Log Files
- Asp.Net
- Asp.Net Mvc
- Winforms
- Wpf
- Enitity Framework
- Rdlc reports


[Video Tutorials](http://www.youtube.com/playlist?list=PL0C32F89E8BBB5368)
---

You can watch a bunch of short videos on getting started and [using ApprovalTests in .Net](http://www.youtube.com/playlist?list=PL0C32F89E8BBB5368) at youtube

Podcasts
---
If you prefer auditory learning, you might enjoy the following podcast 

- [Hanselminutes] (http://www.hanselminutes.com/360/approval-tests-with-llewellyn-falco)
- [Herding Code](http://www.developerfusion.com/media/122649/herding-code-117-llewellyn-falcon-on-approval-tests/)
- [The Watir Podcast](http://watirpodcast.com/podcast-53/)


Available on NuGet
---
[Install-Package ApprovalTests](http://nuget.org/packages/ApprovalTests)


Examples
---
[Sample Code](https://github.com/approvals/ApprovalTests.Net/tree/master/ApprovalDemos/GettingStartedDemos)

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

Will Produce a File 

    SampleTest.TestList.recieved.txt
    [0] = Dan
    [1] = James
    [2] = Jason
    [3] = Katrina
    [4] = Llewellyn

Simply rename this to SampleTest.TestList.**approved**.txt and the test will now pass.


More Info
---

- [Website](http://approvaltests.sourceforge.net/)
- [Blog](http://blog.approvaltests.com/)
- [Getting Started Doc](https://github.com/approvals/ApprovalTests.Net/blob/master/build/Documentation/Approval%20Tests%20-%20Getting%20Started.pdf?raw=true)

	
## LICENSE
[Apache 2.0 License](https://github.com/SignalR/SignalR/blob/master/LICENSE.md)


Questions?
---

twitter: [@LlewellynFalco](https://twitter.com/#!/llewellynfalco) or #ApprovalTests