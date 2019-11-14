using System;
using ApprovalUtilities.Persistence;

namespace ApprovalUtilities.Utilities
{
    public class Clock : ILoader<DateTime>
    {
        public virtual DateTime Load()
        {
            return DateTime.Now;
        }
    }

    public class MockClock : Clock
    {
        private readonly DateTime mockTime;
        private int ticks;

        public MockClock() : this(new DateTime(2011, 5, 6, 10, 30, 0, 0))
        {
        }

        public MockClock(DateTime mockTime)
        {
            this.mockTime = mockTime;
        }

        public override DateTime Load()
        {
            ticks += 10;
            return new DateTime(mockTime.Year, mockTime.Month, mockTime.Day, mockTime.Hour, mockTime.Minute, 0, ticks);
        }
    }
}