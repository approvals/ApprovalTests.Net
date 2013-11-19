using System;
using System.IO;
using System.Linq;
using System.Management;

namespace ApprovalTests.Namers
{
	public class ApprovalResults
	{
		public static void UniqueForDotNetVersion()
		{
			NamerFactory.AsEnvironmentSpecificTest(GetDotNetVersion);
		}

		public static string GetDotNetVersion()
		{
			return "Net_v" + Environment.Version;
		}

		public static void UniqueForMachineName()
		{
			NamerFactory.AsEnvironmentSpecificTest(GetMachineName);
		}

		public static string GetMachineName()
		{
			return "ForMachine." + Environment.MachineName;
		}

		public static string GetOsName()
		{
			var name = TransformEasyOsName(GetFullOsNameFromWmi());
			return name.Trim().Replace(' ', '_');
		}

		public static string GetFullOsName()
		{
			var name = GetFullOsNameFromWmi();
			return name.Trim().Replace(' ', '_');
		}

		private static string GetFullOsNameFromWmi()
		{
			var caption =
				(from x in
					 new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
				 select x.GetPropertyValue("Caption")).FirstOrDefault();

			var name = caption == null ? Environment.OSVersion.ToString() : caption.ToString();
			return name;
		}

		public static string TransformEasyOsName(string captionName)
		{
			string[] known = {"XP", "2000", "Vista", "7", "8", "Server 2003", "Server 2008", "Server 2012"};
			var matched = known.FirstOrDefault(s => captionName.StartsWith("Microsoft Windows " + s));
			if (matched != null)
			{
				return "Windows " + matched;
			}
			return captionName;
		}

		public static void UniqueForOs()
		{
			NamerFactory.AsEnvironmentSpecificTest(GetOsName);
		}

		public static string GetUserName()
		{
			return "ForUser." + Environment.UserName;
		}

		public static void UniqueForUserName()
		{
			NamerFactory.AsEnvironmentSpecificTest(GetUserName);
		}

		public static void ForScenario(string data)
		{
			NamerFactory.AdditionalInformation = "ForScenario." + Scrub(data);
		}

		public static string Scrub(string data)
		{
			var invalid = Path.GetInvalidFileNameChars().ToArray();
			var chars = data.Select(c => invalid.Contains(c) ? '_' : c).ToArray();
			return new string(chars);
		}
	}
}