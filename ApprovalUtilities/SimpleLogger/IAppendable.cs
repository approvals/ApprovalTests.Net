using System;

namespace ApprovalUtilities.SimpleLogger
{
	public interface IAppendable
	{
		void AppendLine(String text);
	}
}