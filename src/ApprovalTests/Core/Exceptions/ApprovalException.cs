namespace ApprovalTests.Core.Exceptions;

public class ApprovalException(string received, string approved) : Exception
{
    public string Received { get; } = received;

    public string Approved { get; } = approved;

    protected bool Equals(ApprovalException other) => string.Equals(Approved, other.Approved) && string.Equals(Received, other.Received);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ApprovalException) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((Approved != null ? Approved.GetHashCode() : 0) * 397) ^ (Received != null ? Received.GetHashCode() : 0);
        }
    }

    public static bool operator ==(ApprovalException left, ApprovalException right) => Equals(left, right);

    public static bool operator !=(ApprovalException left, ApprovalException right) => !Equals(left, right);
}