using System;
using System.Runtime.Serialization;

namespace ApprovalTests.Core.Exceptions
{
	[Serializable]
	public class ApprovalException : Exception
	{
		private readonly string approved;
		private readonly string received;

		public ApprovalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			approved = info.GetString("Approved");
			received = info.GetString("Received");
		}

		public ApprovalException(string received, string approved)
		{
			this.received = received;
			this.approved = approved;
		}

		public string Received
		{
			get { return received; }
		}

		public string Approved
		{
			get { return approved; }
		}

		protected bool Equals(ApprovalException other)
		{
			return string.Equals(approved, other.approved) && string.Equals(received, other.received);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ApprovalException) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((approved != null ? approved.GetHashCode() : 0)*397) ^ (received != null ? received.GetHashCode() : 0);
			}
		}

		public static bool operator ==(ApprovalException left, ApprovalException right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ApprovalException left, ApprovalException right)
		{
			return !Equals(left, right);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);

			info.AddValue("Approved", Approved);
			info.AddValue("Received", Received);
		}
	}
}