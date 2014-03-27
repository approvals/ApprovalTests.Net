using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using ApprovalTests.Writers;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Email
{
	public class EmailApprovals
	{
		public static void Verify(MailMessage email)
		{
			VerifyScrubbed(email, ScrubBoundries);
		}

		public static void VerifyScrubbed(MailMessage email, params Func<string, string>[] scrubbers)
		{
			var emailText = CreateEmail(email);
			foreach (var scrubber in scrubbers)
			{
				emailText = scrubber.Invoke(emailText);
			}
			Approvals.Verify(WriterFactory.CreateTextWriter(emailText, "eml"));
		}

		public static string CreateEmail(MailMessage email)
		{
			var tempdir = Path.GetTempFileName();
			File.Delete(tempdir);
			Directory.CreateDirectory(tempdir);
			var client = new SmtpClient("doesntmatter")
			             	{
			             		DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
			             		PickupDirectoryLocation = tempdir
			             	};
			client.Send(email);
			var emailText = ReadFileWhereLines(GetLatestFile(client.PickupDirectoryLocation), l => !l.StartsWith("Date"));
			return emailText;
		}

		private static string ScrubBoundries(string emailText)
		{
			var boundies = FindBoundies(emailText);
			emailText = ScrubBoundies(emailText, boundies);
			return emailText;
		}

		private static string ScrubBoundies(string emailText, string[] boundies)
		{
			int count = 0;
			string guid = "--boundary_{0}_00000000-0000-0000-0000-00000000000{0}";
			foreach (var b in boundies)
			{
				emailText = emailText.Replace(b, guid.FormatWith(count++));
			}
			return emailText;
		}

		public static string[] FindBoundies(string emailText)
		{
			int startPoint = 0;
			var boundies = new HashSet<string>();
			while ((startPoint = emailText.IndexOf("boundary=--", startPoint)) != -1)
			{
				var preamble = "boundary=--boundary_0_".Length;
				var guid = "7ddc4a25-b0f6-44d4-bcb0-03f577170c19".Length;
				boundies.Add(emailText.Substring(preamble + startPoint, guid));
				startPoint++;
			}
			return boundies.ToArray();
		}

		public static string ReadFileWhereLines(string latestFile, Func<string, bool> predicate)
		{
			string[] latestFileLines = File.ReadAllLines(latestFile).Where(predicate).ToArray();
			string newText = string.Join(Environment.NewLine, latestFileLines);
			return newText;
		}

		public static string GetLatestFile(string dir)
		{
			return new DirectoryInfo(dir)
				.GetFiles("*.eml")
				.OrderBy(f => f.CreationTime)
				.Last()
				.FullName;
		}
	}
}