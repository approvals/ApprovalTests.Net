using System;

namespace ApprovalTests.Reporters
{
	public class IgnoreLineEndingsAttribute : Attribute
	{
		private readonly bool _ignoreLineEndings;

		public IgnoreLineEndingsAttribute(bool ignoreLineEndings)
		{
			_ignoreLineEndings = ignoreLineEndings;
		}

		public bool IgnoreLineEndings
		{
			get
			{
				return _ignoreLineEndings;
			}
		}
	}
}