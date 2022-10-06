namespace ApprovalUtilities.Reflection;

using System.ComponentModel;

public class HandlerListEntry
{
    const string HandlerFieldName = "handler";
    const string KeyFieldName = "key";
    const string NextFieldName = "next";
    readonly object listEntry;
    static Type listEntryType;
    Delegate handler;
    object key;
    HandlerListEntry next;

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
                    next = new(nextValue);
                }
            }

            return next;
        }
    }

    static Type ListEntryType
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

    T GetField<T>(string name)
    {
        return listEntry.GetInstanceFields(fi => string.Compare(fi.Name, name, false) == 0)
            .Single().GetValue<T>(listEntry);
    }
}