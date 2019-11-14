namespace ApprovalUtilities.Reflection
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using Utilities;

    public class HandlerListEntry
    {
        private const string HandlerFieldName = "handler";
        private const string KeyFieldName = "key";
        private const string NextFieldName = "next";
        private readonly object listEntry;
        private static Type listEntryType;
        private Delegate handler;
        private object key;
        private HandlerListEntry next;

        public HandlerListEntry(object listEntry)
        {
            if (listEntry != null && listEntry.GetType() == ListEntryType)
            {
                this.listEntry = listEntry;
            }
        }

        public Delegate Handler
        {
            get
            {
                if (handler == null && listEntry != null)
                {
                    handler = GetField<Delegate>(HandlerFieldName);
                }

                return handler;
            }
        }

        public object Key
        {
            get
            {
                if (key == null && listEntry != null)
                {
                    key = GetField<object>(KeyFieldName);
                }

                return key;
            }
        }

        public HandlerListEntry Next
        {
            get
            {
                if (next == null && listEntry != null)
                {
                    var nextValue = GetField<object>(NextFieldName);
                    if (nextValue != null)
                    {
                        next = new HandlerListEntry(nextValue);
                    }
                }

                return next;
            }
        }

        private static Type ListEntryType
        {
            get
            {
                if (listEntryType == null)
                {
                    listEntryType = typeof(EventHandlerList).GetNestedTypes(BindingFlags.NonPublic).Single();
                }

                return listEntryType;
            }
        }

        public override string ToString()
        {
            return this.WritePropertiesToString();
        }

        private T GetField<T>(string name)
        {
            return listEntry.GetInstanceFields(fi => string.Compare(fi.Name, name, false) == 0)
                .Single().GetValue<T>(listEntry);
        }
    }
}