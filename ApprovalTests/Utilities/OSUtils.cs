using System;
using System.IO;
using System.Linq;
using System.Management;

namespace ApprovalUtilities
{
	internal  enum ApprovalsPlatform
	{
		Windows,
		Linux,
		Mac
	}

	internal  class OSUtils
	{
		public static ApprovalsPlatform GetPlatformId()
		{
			var platformID = Environment.OSVersion.Platform;
			if (platformID == PlatformID.MacOSX || platformID == PlatformID.Unix) {
				if (Directory.Exists("/Applications") &&
				    Directory.Exists("/Users") &&
				    Directory.Exists("/Volumes") &&
				    Directory.Exists("/System")) {
					return ApprovalsPlatform.Mac;
				} else {
					return ApprovalsPlatform.Linux;
				}
			}
			return ApprovalsPlatform.Windows;
		}

		public static string GetFullOsNameFromWmi()
		{
			var platformId = ApprovalUtilities.OSUtils.GetPlatformId();
			if (platformId == ApprovalUtilities.ApprovalsPlatform.Windows) {
				var caption =
					(from x in
						new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
						select x.GetPropertyValue("Caption")).FirstOrDefault ();

				var name = caption == null ? Environment.OSVersion.ToString() : caption.ToString();
				return name;
			} else {
				return platformId.ToString();
			}
		}
	}
}

