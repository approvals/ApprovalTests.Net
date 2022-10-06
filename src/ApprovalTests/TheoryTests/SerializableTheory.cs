namespace ApprovalTests.TheoryTests;

public static class SerializableTheory
{
    public static void Verify(object original, Action<object, object> assertEqual)
    {
        var stream = new MemoryStream();
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, original);
        stream.Seek(0, 0);
        var result = formatter.Deserialize(stream);
        assertEqual(result, original);
    }
}