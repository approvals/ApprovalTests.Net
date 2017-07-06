using System.Linq;
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

	    public static string FormatXmlWithOrderedAttributes(string xml)
	    {
	        string text;
	        var xElement = XElement.Parse(xml);
	        SortAttributes(xElement);

	        text = xElement.ToString();
	        return text;
	    }

	    public static void SortAttributes(XElement xElement)
	    {
	        var orderedNodes = xElement.Attributes().OrderBy(e => e.ToString()).ToArray();
	        xElement.RemoveAttributes();
	        
            foreach (var attribute in orderedNodes)
	        {
	            xElement.SetAttributeValue(attribute.Name, attribute.Value);
	        }

	        foreach (var node in xElement.Nodes().Where(n => n is XElement))
	        {
	            SortAttributes((XElement)node);
	        }
	    }
	}
}