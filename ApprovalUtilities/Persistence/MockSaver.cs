using System.Collections.Generic;
using System.Linq;

namespace ApprovalUtilities.Persistence
{
    public class MockSaver<T> : ISaver<T>
    {
        private List<T> all = new List<T>();

        public T[] Saved => all.ToArray();

        public T LastSaved => all.Last();

        public T Save(T objectToBeSaved)
        {
            all.Add(objectToBeSaved);
            return objectToBeSaved;
        }
    }
}