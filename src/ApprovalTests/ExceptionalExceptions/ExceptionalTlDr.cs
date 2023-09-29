using ApprovalTests.WebApi.MicrosoftHttpClient;

namespace ApprovalTests.ExceptionalExceptions;

public class ExceptionalTlDr(ExceptionalId uid) :
    RestQuery<string>
{
    readonly ExceptionalId uid = uid;

    public override string GetQuery() => throw new NotImplementedException();

    public override string GetBaseAddress() => throw new NotImplementedException();

    public override string Load() => throw new NotImplementedException();
}