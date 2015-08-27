using System;
using System.IO;
using System.Linq;
using System.Management;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Namers
{
	public class ApprovalResults
	{
		public static IDisposable UniqueForDotNetVersion()
		{
			return NamerFactory.AsEnvironmentSpecificTest(GetDotNetVersion);
		}

		public static string GetDotNetVersion()
		{
			return "Net_v" + Environment.Version;
		}

		public static IDisposable UniqueForMachineName()
		{
			return NamerFactory.AsEnvironmentSpecificTest(GetMachineName);
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
			var platformID = Environment.OSVersion.Platform;
			if (platformID == PlatformID.MacOSX || platformID == PlatformID.Unix) {
				if (Directory.Exists("/Applications") &&
					Directory.Exists("/Users") &&
					Directory.Exists("/Volumes") &&
					Directory.Exists("/System"))
				{
					return "Mac";
				}
				else
				{
					return "Linux";
				}
			} else {
				var caption =
					(from x in
					     new ManagementObjectSearcher ("SELECT Caption FROM Win32_OperatingSystem").Get ().OfType<ManagementObject> ()
					 select x.GetPropertyValue ("Caption")).FirstOrDefault ();

				var name = caption == null ? Environment.OSVersion.ToString () : caption.ToString ();
				return name;
			}
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

		public static IDisposable UniqueForOs()
		{
			return NamerFactory.AsEnvironmentSpecificTest(GetOsName);
		}

		public static string GetUserName()
		{
			return "ForUser." + Environment.UserName;
		}

		public static IDisposable UniqueForUserName()
		{
			return NamerFactory.AsEnvironmentSpecificTest(GetUserName);
		}

		public static IDisposable ForScenario(string data)
		{
			var name = "ForScenario." + Scrub(data);
			return NamerFactory.AsEnvironmentSpecificTest(() => name);
		}

		public static IDisposable ForScenario(params object[] dataPoints)
		{
			var name = dataPoints.JoinStringsWith(o => "" + o, ".");
			return ForScenario(name);
		}

		public static string Scrub(string data)
		{
			var invalid = Path.GetInvalidFileNameChars().ToArray();
			var chars = data.Select(c => invalid.Contains(c) ? '_' : c).ToArray();
			return new string(chars);
		}
	}
}