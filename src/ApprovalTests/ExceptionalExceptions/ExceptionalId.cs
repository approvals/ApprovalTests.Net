namespace ApprovalTests.ExceptionalExceptions;

public class ExceptionalId
{
    public string Assembly { get; set; }
    public string Class { get; set; }
    public string Method { get; set; }
    public string Exception { get; set; }

    public override string ToString()
    {
        return this.WritePropertiesToString();
    }
}