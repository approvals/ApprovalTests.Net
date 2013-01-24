using System.Collections.Generic;
using System.Linq;

namespace ApprovalUtilities.Persistence
{
    public class MockSaver<T> : ISaver<T>
    {
        private List<T> all = new List<T>();
        public T[] Saved{get { return all.ToArray(); }}
        public T LastSaved{get { return all.Last(); }}
        public T Save(T t)
        {
            all.Add(t);
            return t;
        }
    }
}