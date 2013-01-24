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

- [Herding Code](http://www.developerfusion.com/media/122649/herding-code-117-llewellyn-falcon-on-approval-tests/)
- [The Watir Podcast](http://watirpodcast.com/podcast-53/)


Available on NuGet
---
[Install-Package ApprovalTests](http://nuget.org/packages/ApprovalTests)


Examples
---


    using ApprovalTests.Reporters;
    using CompositionTests;

    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class IntegrationTest
    {
        [TestMethod]
        public void VerifyComposition()
        {
            var catalog = new TypeCatalog(typeof(Ford));
            MefComposition.VerifyCompositionInfo(catalog);
        }
    }

More Info
---

	
## LICENSE
[Apache 2.0 License](https://github.com/SignalR/SignalR/blob/master/LICENSE.md)


Questions?
---

twitter: [@LlewellynFalco](https://twitter.com/#!/llewellynfalco) or #ApprovalTests