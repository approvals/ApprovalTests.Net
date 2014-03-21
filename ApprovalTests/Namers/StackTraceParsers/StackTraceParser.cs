using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ApprovalTests.StackTraceParsers;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Namers.StackTraceParsers
{
	public class StackTraceParser : IStackTraceParser
	{
		private static IList<IStackTraceParser> parsers = (IList<IStackTraceParser>) GetParsers();
		private IStackTraceParser parser;

		public string ForTestingFramework
		{
			get { return GetParsers().Select(x => x.ForTestingFramework).ToReadableString(); }
		}

		public bool Parse(StackTrace stackTrace)
		{
			foreach (IStackTraceParser p in GetParsers())
			{
				if (p.Parse(stackTrace))
				{
					parser = p;
					return true;
				}
			}

			var helpLink = @"http://blog.approvaltests.com/2012/01/creating-namers.html";
			throw new Exception(
				string.Format("Approvals is not set up to use your test framework.{0}" +
				              "It currently supports {1}{0}" +
				              "To add one use {2}.AddParser() method to add implementation of {3} with support for your testing framework.{0}" +
				              "To learn how to implement one see {4}",
				              Environment.NewLine,
				              ForTestingFramework,
				              GetType(),
				              typeof (IStackTraceParser),
				              helpLink))
				{
					HelpLink = helpLink
				};
		}

		public string ApprovalName
		{
			get { return parser.ApprovalName; }
		}

		public string SourcePath
		{
			get { return parser.SourcePath; }
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
				LoadIfApplicable(parsers, new MbUnitStackTraceParser());
				LoadIfApplicable(parsers, new XUnitStackTraceParser());
				LoadIfApplicable(parsers, new XUnitTheoryStackTraceParser());
				parsers.Add(new MSpecStackTraceParser());
			}
			return parsers;
		}
	}
}