using System;
using ApprovalTests.WebApi.MicrosoftHttpClient;

namespace ApprovalTests.ExceptionalExceptions;

public class ExceptionalTlDr : RestQuery<string>
{
    readonly ExceptionalId uid;

    public ExceptionalTlDr(ExceptionalId uid)
    {
        this.uid = uid;
    }

    public override string GetQuery()
    {
        throw new NotImplementedException();
    }

    public override string GetBaseAddress()
    {
        throw new NotImplementedException();
    }

    public override string Load()
    {
        throw new NotImplementedException();
    }
}