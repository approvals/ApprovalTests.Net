using System.Xml.Linq;

namespace ApprovalUtilities.Xml
{
	public class XmlUtils
	{
		public static string FormatXml(string xml, bool safe)
		{
			try
			{
				return XElement.Parse(xml).ToString();
			}
			catch
			{
				if (safe)
				{
					return xml;
				}
				throw;
			}
		}
	}
}