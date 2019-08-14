using ApprovalTests.StackTraceParsers;
using ApprovalUtilities.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ApprovalTests.Namers.UnitTestFrameworks;

namespace ApprovalTests.Namers.StackTraceParsers
{
    public class StackTraceParser : IStackTraceParser
    {
        private static IList<IStackTraceParser> parsers = (IList<IStackTraceParser>) GetParsers();
        private IStackTraceParser parser;

        public string ForTestingFramework => GetParsers().Select(x => x.ForTestingFramework).ToReadableString();

        public bool Parse(StackTrace stackTrace)
        {
            foreach (var parserTemplate in GetParsers())
            {
                var p = (IStackTraceParser) Activator.CreateInstance(parserTemplate.GetType());
                if (p.Parse(stackTrace))
                {
                    parser = p;
                    return true;
                }
            }

            var helpLink = "http://blog.approvaltests.com/2012/01/creating-namers.html";
            throw new Exception(
                $@"
Could Not Detect Test Framework

Either:
1) Optimizer Inlined Test Methods

Solutions:
a) Add [MethodImpl(MethodImplOptions.NoInlining)]
b) Set Build->Optimize Code to False
   & Build->Advanced->DebugInfo to Full

or
2) Approvals is not set up to use your test framework.
It currently supports {ForTestingFramework}

Solution:
To add one use {GetType()}.AddParser() method to add implementation of {typeof(IStackTraceParser)} with support for your testing framework.
To learn how to implement one see {helpLink}")
            {
                HelpLink = helpLink
            };
        }

        public string ApprovalName => parser.ApprovalName;

        public string SourcePath
        {
            get
            {
                var path = parser.SourcePath;
                if (string.IsNullOrEmpty(path))
                {
                    var helpMessage = @"
ApprovalTests is not detecting the proper source path

This is probably because you're missing the following
line in your .csproj file:
	  <DebugType>full</DebugType>
in the
<Project>
  <PropertyGroup>
element.

Solution:
a) Add <DebugType>full</DebugType> to your .csproj file.
b) OR Build->Advanced->DebugInfo to Full";
                    throw new Exception(helpMessage);
                }
                return path;
            }
        }

        private static void LoadIfApplicable(IList<IStackTraceParser> found, AttributeStackTraceParser p)
        {
            if (p.IsApplicable())
            {
                found.Add(p);
            }
        }

        public static void AddParser(IStackTraceParser parser)
        {
            parsers.Add(parser);
        }

        public static IEnumerable<IStackTraceParser> GetParsers()
        {
            if (parsers == null)
            {
                parsers = new List<IStackTraceParser>();
                LoadIfApplicable(parsers, new NUnitStackTraceParser());
                LoadIfApplicable(parsers, new VSStackTraceParser());
                LoadIfApplicable(parsers, new MsTestDataTestMethodStackTraceParser());
                LoadIfApplicable(parsers, new XUnitStackTraceParser());
                LoadIfApplicable(parsers, new XUnit2TheoryStackTraceParser());
                parsers.Add(new MSpecStackTraceParser());
            }

            return parsers;
        }
    }
}