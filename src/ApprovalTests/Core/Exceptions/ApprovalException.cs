using System.Runtime.Serialization;

namespace ApprovalTests.Core.Exceptions;

[Serializable]
public class ApprovalException : Exception
{
    public ApprovalException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Approved = info.GetString("Approved");
        Received = info.GetString("Received");
    }

    public ApprovalException(string received, string approved)
    {
        Received = received;
        Approved = approved;
    }

    public string Received { get; }

    public string Approved { get; }

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

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue("Approved", Approved);
        info.AddValue("Received", Received);
    }
}