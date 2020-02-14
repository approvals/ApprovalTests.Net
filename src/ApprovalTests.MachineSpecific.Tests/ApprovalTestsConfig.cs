// begin-snippet: config_file
using ApprovalTests.Reporters;

[assembly: UseReporter(typeof(DiffReporter))]
// end-snippet