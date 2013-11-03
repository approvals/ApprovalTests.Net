﻿namespace ApprovalTests.StackTraceParsers
{
    public class XUnitTheoryStackTraceParser : XUnitStackTraceParser
    {
        public const string TheoryAttribute = "Xunit.Extensions.TheoryAttribute";

        public override string ForTestingFramework
        {
            get { return "xUnit.extensions"; }
        }

        protected override string GetAttributeType()
        {
            return TheoryAttribute;
        }
    }
}