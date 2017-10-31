using System.Collections.Generic;
using Alphaleonis.Win32.Filesystem;
using NUnit.Framework;
using IOPath = System.IO.Path;

namespace ApprovalTests.AlphaFS.Tests
{
	[TestFixture]
    public class PathTests
    {
	    [Test]
	    [TestCaseSource(nameof(CombineTestCases))]
	    public void Combine(string[] parts)
	    {
		    var alphaFS = Path.Combine(parts);
		    var io = IOPath.Combine(parts);

		    Assert.That(alphaFS, Is.EqualTo(io));
	    }

	    [Test]
	    [TestCaseSource(nameof(GetFullPathTestCases))]
	    public void GetFullPath(string path)
	    {
		    var alphaFS = Path.GetFullPath(path);
		    var io = IOPath.GetFullPath(path);

		    Assert.That(alphaFS, Is.EqualTo(io));
	    }

	    [Test]
	    public void GetTempFileName()
	    {
		    var alphaFS = Path.GetTempFileName();
		    var io = IOPath.GetTempFileName();

		    Assert.That(IOPath.GetDirectoryName(alphaFS), Is.EqualTo(Path.GetDirectoryName(io)));
		    Assert.That(IOPath.GetExtension(alphaFS), Is.EqualTo(Path.GetExtension(io)));
	    }

	    [Test]
	    [TestCaseSource(nameof(FilepathTestCases))]
	    public void GetDirectoryName(string filepath)
	    {
		    var alphaFS = Path.GetDirectoryName(filepath);
		    var io = IOPath.GetDirectoryName(filepath);

		    Assert.That(alphaFS, Is.EqualTo(io));
	    }

	    [Test]
	    [TestCaseSource(nameof(FilepathTestCases))]
	    public void GetFileName(string filepath)
	    {
		    var alphaFS = Path.GetFileName(filepath);
		    var io = IOPath.GetFileName(filepath);

		    Assert.That(alphaFS, Is.EqualTo(io));
	    }

	    [Test]
	    public void GetInvalidFileNameChars()
	    {
		    var alphaFS = Path.GetInvalidFileNameChars();
		    var io = IOPath.GetInvalidFileNameChars();

		    Assert.That(alphaFS, Is.EqualTo(io));
	    }

	    [Test]
	    public void DirectorySeparatorChar()
	    {
		    var alphaFS = Path.DirectorySeparatorChar;
		    var io = IOPath.DirectorySeparatorChar;

		    Assert.That(alphaFS, Is.EqualTo(io));
	    }

	    [Test]
	    public void GetTempPath()
	    {
		    var alphaFS = Path.GetTempPath();
		    var io = IOPath.GetTempPath();

		    Assert.That(alphaFS, Is.EqualTo(io));
	    }

	    [Test]
	    [TestCaseSource(nameof(FilepathTestCases))]
	    public void GetExtension(string filepath)
	    {
		    var alphaFS = Path.GetExtension(filepath);
		    var io = IOPath.GetExtension(filepath);

		    Assert.That(alphaFS, Is.EqualTo(io));
	    }

	    [Test]
	    [TestCaseSource(nameof(ExtensionTestCases))]
	    public void ChangeExtension(string filepath, string newExtension)
	    {
		    var alphaFS = Path.ChangeExtension(filepath, newExtension);
		    var io = IOPath.ChangeExtension(filepath, newExtension);

		    Assert.That(alphaFS, Is.EqualTo(io));
	    }

	    [Test]
	    public void PathSeparator()
	    {
		    var alphaFS = Path.PathSeparator;
		    var io = IOPath.PathSeparator;

		    Assert.That(alphaFS, Is.EqualTo(io));
	    }

	    [Test]
	    public void GetLongFullPath()
	    {
		    var expected =
			    "c:\\directory1\\directory2\\directory3\\directory4\\directory5\\directory6\\directory7\\directory8\\directory9\\directory10\\" +
			    "directory11\\directory12\\directory13\\directory14\\directory15\\directory16\\directory17\\directory18\\directory19\\directory20\\" +
			    "directory21\\directory22\\directory23\\directory24\\directory25\\directory26\\directory27\\directory28\\directory29\\file.tmp";
		    var actual = Path.GetFullPath(expected);

		    Assert.That(actual, Is.EqualTo(expected));
	    }

	    private static IEnumerable<TestCaseData> CombineTestCases
	    {
		    get
		    {
			    yield return new TestCaseData((object) new[] {"aa"}).SetName("aa");
			    yield return new TestCaseData((object) new[] {"a", "bb"}).SetName(@"a\bb");
			    yield return new TestCaseData((object) new[] {"aa", "bbb", "c"}).SetName(@"aa\bbb\c");
			    yield return new TestCaseData((object) new[] {"aa", "b", "", "ddd"}).SetName(@"aa\b\ddd");
		    }
	    }

	    private static IEnumerable<TestCaseData> GetFullPathTestCases
	    {
		    get
		    {
			    yield return new TestCaseData(@"aa\b\ccccc\dd\e").SetName(@"aa\b\ccccc\dd\e");
			    yield return new TestCaseData(@"x:\a\bb\c\ddd\").SetName(@"x:\a\bb\c\ddd\");
			    yield return new TestCaseData(@"\a\b\file.ext").SetName(@"\a\b\file.ext");
			    yield return new TestCaseData(@"a\.\b\").SetName(@"a\.\b\");
			    yield return new TestCaseData(@"a\..\b\c").SetName(@"a\..\b\c");
		    }
	    }

	    private static IEnumerable<TestCaseData> FilepathTestCases
	    {
		    get
		    {
			    yield return new TestCaseData(@"aa\b\c\file.tmp").SetName(@"aa\b\c\file.tmp");
			    yield return new TestCaseData(@"x:\a\b\file.n").SetName(@"x:\a\b\file.n");
			    yield return new TestCaseData(@"a\.\b\file.ss").SetName(@"a\.\b\file.ss");
			    yield return new TestCaseData(@"a\..\b\c\file.").SetName(@"a\..\b\c\file.");
			    yield return new TestCaseData(@"a\b\c\d\").SetName(@"a\b\c\d\");
			    yield return new TestCaseData(@"x:\a\b\c").SetName(@"x:\a\b\c");
		    }
	    }

	    private static IEnumerable<TestCaseData> ExtensionTestCases
	    {
		    get
		    {
			    foreach (var filepathTestCase in FilepathTestCases)
			    {
				    var filepath = filepathTestCase.Arguments[0];
				    yield return new TestCaseData(filepath, "").SetName($"{filepath}|empty");
				    yield return new TestCaseData(filepath, ".").SetName($"{filepath}|dot");
				    yield return new TestCaseData(filepath, ".n").SetName($"{filepath}|.n");
				    yield return new TestCaseData(filepath, "ss").SetName($"{filepath}|ss");
			    }
		    }
	    }
	}
}
