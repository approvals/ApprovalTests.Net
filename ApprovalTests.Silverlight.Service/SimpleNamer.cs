
using ApprovalTests.Core;

namespace ApprovalTests.Silverlight.Service
{
	public class SimpleNamer : IApprovalNamer
	{
		private readonly string path;
		private readonly string name;

		public SimpleNamer(string path, string name)
		{
			this.path = path;
			this.name = name;
		}

		public string SourcePath
		{
			get { return path; }
		}

		public string Name
		{
			get { return name; }
		}
	}
}