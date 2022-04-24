namespace ApprovalUtilities.Reflection;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

public static class HandlerListHelper
{
    private const string HeadFieldName = "head";

    public static IEnumerable<HandlerListEntry> AsEnumerable(this EventHandlerList list)
    {
        var current = list.GetHead();
        while (current != null)
        {
            yield return current;
            current = current.Next;
        }
    }

    public static HandlerListEntry GetHead(this EventHandlerList list)
    {
        Func<FieldInfo, bool> selector = fi => string.Compare(fi.Name, HeadFieldName, false) == 0;
        var headInfo = list.GetInstanceFields(selector).Single();
        return new HandlerListEntry(headInfo.GetValue(list));
    }
}