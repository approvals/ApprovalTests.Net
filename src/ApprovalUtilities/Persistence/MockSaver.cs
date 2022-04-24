using System.Collections.Generic;
using System.Linq;

namespace ApprovalUtilities.Persistence;

public class MockSaver<T> : ISaver<T>
{
    List<T> all = new();

    public T[] Saved => all.ToArray();

    public T LastSaved => all.Last();

    public T Save(T objectToBeSaved)
    {
        all.Add(objectToBeSaved);
        return objectToBeSaved;
    }
}