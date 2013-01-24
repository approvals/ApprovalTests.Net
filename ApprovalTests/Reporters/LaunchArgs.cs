namespace ApprovalTests.Reporters
{
	public struct LaunchArgs
	{
		private string arguments;
		private string program;

		public LaunchArgs(string program, string arguments)
		{
			this.program = program;
			this.arguments = arguments;
		}

		public string Program
		{
			get { return program; }
		}

		public string Arguments
		{
			get { return arguments; }
		}

		public override string ToString()
		{
			return string.Format("\"{0}\" {1}", program, arguments);
		}
	}
}