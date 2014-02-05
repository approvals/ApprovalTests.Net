﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests.StackTraceParsers;
using AssertExLib;
using NUnit.Framework;

namespace ApprovalTests.Tests.StackTraceParsers
{
    [TestFixture]
    public class StackTraceParserTests
    {
        // This test is not perfect, even if the StackTraceParser parser collection is thread-safe it is possible that the test
        // pass. The bug is the result on concurrency between two threads, so this is a timing issue, sometime it happens sometime not.
        [Test]
        public void Parse_UsingStaticInitialize_DontThrowInvalidOperationException()
        {
            // ARRANGE
            var parser = new StackTraceParser();

            // ACT + ASSERT
            try
            {
                Parallel.ForEach(Enumerable.Range(1, 20), (_) =>
                {
                    try
                    {
                        var stackTrace = new StackTrace();
                        parser.Parse(stackTrace);
                    }
                    catch (InvalidOperationException e)
                    {
                        Assert.Fail(
                            "InvalidOperationException when trying to parse stacktrace. " +
                                "This is caused by the parser collection not being thread-safe. " +
                                    "Original exception message : {0} and stacktrace : {1}", 
                                e.Message, 
                                e.StackTrace
                            );
                    }
                    // Because the current stacktrace passed to the parse method doesn't contains any trace of a compliant stacktrace parser
                    // it's normal that we receive an exception here so let's ignore it.
                    catch (Exception e)
                    {
                        if (!e.Message.StartsWith("Approvals is not set up to use your test framework", StringComparison.OrdinalIgnoreCase))
                        {
                            Assert.Fail("Any other exception");
                        }
                    }
                });
            }
            catch (AggregateException e)
            {
                // Throw the first inner exception of the AggretateException, this way NUnit shows a much clearer result.
                throw e.InnerException;
            }
        }
    }
}
