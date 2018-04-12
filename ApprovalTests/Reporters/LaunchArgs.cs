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

        public string Program => program;

        public string Arguments => arguments;

        public override string ToString()
        {
            return $"\"{program}\" {arguments}";
        }
    }
}