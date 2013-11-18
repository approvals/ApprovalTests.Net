using System.Reflection;
using ApprovalTests.Reporters;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle("Approval Tests Tests")]
[assembly: AssemblyDescription("Tests for the approval testing library")]
[assembly: AssemblyProduct("Approval Tests Tests")]

[assembly: UseReporter(typeof(DiffReporter))]

