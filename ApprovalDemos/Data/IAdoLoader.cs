using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalUtilities.Persistence;
using NUnit.Framework;

namespace ApprovalDemos.Data
{
	public interface IAdoLoader<T> : ILoader<T>
	{
		string Query { get; }
		string ConnectionString { get; }
	}
}