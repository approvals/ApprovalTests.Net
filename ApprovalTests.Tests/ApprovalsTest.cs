using System.Collections.Generic;
using System.IO;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter))]
	public class ApprovalsTest
	{
		private static readonly string[] text = new string[] { "abc", "123", "!@#" };

		[Test]
		public void Text()
		{
			Approvals.Verify("should be approved");
		}

		[Test]
		public void EnumerableWithLabel()
		{
			Approvals.VerifyAll(text, "collection");
		}

		[Test]
		public void TestExistingFile()
		{
			var path = PathUtilities.GetDirectoryForCaller();
			var copy = path + "copyOfa.txt";
			File.Copy(path + "a.txt", copy, true);
			Approvals.VerifyFile(copy);
		}

		[Test]
		public void TestBytes()
		{
			var path = PathUtilities.GetDirectoryForCaller();
			Approvals.VerifyBinaryFile(File.ReadAllBytes(path + "a.png"), "png");
		}


		[Test]
		public void EnumerableWithLabelAndFormatter()
		{
			Approvals.VerifyAll(text, "collection", (t) => "" + t.Length);
		}

		[Test]
		public void EnumerableWithHeaderAndFormatter()
		{
			var word = "Llewellyn";
			Approvals.VerifyAll(word, word.ToCharArray(), (c) => c + " => " + (int)c);
		}
				[Test]
		public void DictionarySimple()
				{
						Approvals.VerifyAll(FireFlyMap());
				}
				[Test]
		public void Dictionary()
				{
						Approvals.VerifyAll("Firefly", FireFlyMap());
				} 
				[Test]
		public void DictionaryCustom()
				{
						Approvals.VerifyAll("Firefly", FireFlyMap(), (k,v)=> "\"{0}\" => {1}".FormatWith(k,v));
				}
	[Test]
		public void DictionaryCustomNoHeader()
				{
						Approvals.VerifyAll(FireFlyMap(), (k,v)=> "\"{0}\" => {1}".FormatWith(k,v));
				}

			private static Dictionary<string, string> FireFlyMap()
			{
					var map = new Dictionary<string, string>()
												{
														{"Caption", "Mal"},
														{"2nd In Command", "Zoey"},
														{"Pilot", "Wash"},
														{"Companion", "Inara"},
														{"Muscle", "Jayne"},
														{"Mechanic", "Kaylee"},
														{"Doctor", "Simon"},
														{"Pastor", "Book"},
														{"Stoaway", "River"}
												};
					return map;
			}

			[Test]
		public void EnumerableWithFormatter()
		{
			Approvals.VerifyAll(text, (t) => "" + t.Length);
		}

		
	}
}