namespace ApprovalTests.Reporters
{
    using System;

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
				return this.normaliseLineEndings;
			}
		}
	}
}