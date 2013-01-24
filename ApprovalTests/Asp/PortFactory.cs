using System;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Asp
{
	public class PortFactory
	{
		private static int? aspPort;

		public static int AspPort
		{
			get
			{
				if (aspPort == null)
				{
					throw new MissingFieldException(
						@"{0}.AspPort is uninitialized.
You are using a method that is using {0}.AspPort,
but you have not set a value for this port first"
							.FormatWith(typeof(PortFactory).FullName));
				}
				return (int)aspPort;
			}
			set { aspPort = value; }
		}

		private static int? mvcPort;

		public static int MvcPort
		{
			get
			{
				if (mvcPort == null)
				{
					throw new MissingFieldException(
						@"{0}.MvcPort is uninitialized.
You are using a method that is using {0}.MvcPort,
but you have not set a value for this port first"
							.FormatWith(typeof(PortFactory).FullName));
				}
				return (int)mvcPort;
			}
			set { mvcPort = value; }
		}
	}
}