﻿using ApprovalUtilities.Persistence;

namespace ApprovalTests;

public class ExecutableLambda : IExecutableQuery
{
    readonly string text;
    readonly Func<string, string> executeOnFailure;

    public ExecutableLambda(string text, Action<string> executeOnFailure)
        : this(text, s =>
        {
            executeOnFailure(s);
            return null;
        })
    {

    }

    public ExecutableLambda(string text, Func<string, string> executeOnFailure)
    {
        this.text = text;
        this.executeOnFailure = executeOnFailure;
    }

    public string GetQuery() => text;

    public string ExecuteQuery(string query)
    {
        if (!string.IsNullOrEmpty(query))
        {
            return executeOnFailure(query);
        }

        return null;
    }
}