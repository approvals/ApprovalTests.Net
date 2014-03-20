using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ApprovalTests.TheoryTests
{
	public class SerializableTheory
	{
		public static void Verify(object original, Action<Object,Object> assertEqual)
		{
			var stream = new MemoryStream();
			var formatter = new BinaryFormatter();
			formatter.Serialize(stream, original);
			stream.Seek(0, 0);
			var result = formatter.Deserialize(stream);
			assertEqual(result, original);
		} 
	}
}