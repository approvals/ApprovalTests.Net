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

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);

			info.AddValue("Approved", Approved);
			info.AddValue("Received", Received);
		}
	}
}