using System;

namespace ApprovalTests.Reporters
{
	public class NormaliseLineEndingsForTextFilesAttribute : Attribute
	{
		private readonly bool normaliseLineEndings;

		public NormaliseLineEndingsForTextFilesAttribute(bool normaliseLineEndings)
		{
			this.normaliseLineEndings = normaliseLineEndings;
		}

		public bool NormaliseLineEndings
		{
			get
			{
				return normaliseLineEndings;
			}
		}
	}
}