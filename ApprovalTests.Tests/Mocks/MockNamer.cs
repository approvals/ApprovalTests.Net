using ApprovalTests.Core;

namespace ApprovalTests.Tests.Mocks
{
    internal class MockNamer : IApprovalNamer
    {
        public string SourcePath { get; set; }
        public string Name { get; set; }
    }
}