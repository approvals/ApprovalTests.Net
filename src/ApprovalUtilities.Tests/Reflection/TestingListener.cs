using System;

namespace ApprovalTests.Tests.Events
{
    public static class TestingListener
    {
        public static void AnotherStandardCallback(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void PropertyChangedHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            return;
        }
        public static void StandardCallback(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}