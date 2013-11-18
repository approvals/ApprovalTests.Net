using System.Reflection;
using ApprovalTests.Reporters;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ApprovalTests.Xunit")]
[assembly: AssemblyProduct("ApprovalTests.Xunit")]
[assembly: AssemblyCopyright("Copyright ©  2013")]



[assembly: UseReporter(typeof(DiffReporter))]
