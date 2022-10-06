namespace ApprovalUtilities.Reflection;

using System.ComponentModel;

public static class ReflectionUtilities
{
    const BindingFlags NonPublicInstance = BindingFlags.NonPublic | BindingFlags.Instance;

    public static MethodBase GetRealMethod(MethodBase method)
    {
        var declaringType = method.DeclaringType;
        if (typeof(IAsyncStateMachine).IsAssignableFrom(declaringType))
        {
            var realType = declaringType.DeclaringType;
            foreach (var methodInfo in realType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var stateMachineAttribute = methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>();
                if (stateMachineAttribute == null)
                {
                    continue;
                }
                if (stateMachineAttribute.StateMachineType == declaringType)
                {
                    return methodInfo;
                }
            }
        }

        return method;
    }
    public static IEnumerable<CallbackDescriptor> GetEventHandlerListEvents(this object value)
    {
        Func<PropertyInfo, bool> selector = pi => string.Compare(pi.Name, "Events", false) == 0;
        var listInfo = value.NonPublicInstanceProperties(selector).SingleOrDefault();
        if (listInfo == null)
        {
            return Enumerable.Empty<CallbackDescriptor>();
        }

        var events = from fi in value.NonPublicStaticFields(true)
            let list = listInfo.GetValue<EventHandlerList>(value, null)
            from handlerEntry in list.AsEnumerable()
            where handlerEntry.Key == fi.GetValue(value)
                  && handlerEntry.Handler != null
            select new
            {
                fi.Name,
                handlerEntry.Handler
            };
        return events.Select(e =>
        {
            var callbackDescriptor = new CallbackDescriptor(e.Name);
            callbackDescriptor.AddMethods(e.Handler.GetInvocationList().Select(del => del.Method));
            return callbackDescriptor;
        }).ToArray();
    }

    public static FieldInfo GetFieldForChild(object parent, object child)
    {
        return GetInstanceFields(parent, f => f.GetValue(parent) == child).FirstOrDefault();
    }

    public static IEnumerable<CallbackDescriptor> GetPocoEventsForTypes(object value, params Type[] types)
    {
        return value.GetInstanceFields()
            .Where(fi => types.Any(t => t.IsAssignableFrom(fi.FieldType)) && fi.GetValue<Delegate>(value) != null)
            .Select(e =>
            {
                var callbackDescriptor = new CallbackDescriptor(e.Name);
                var eventDelegate = e.GetValue<Delegate>(value);
                callbackDescriptor.AddMethods(eventDelegate.GetInvocationList().Select(del => del.Method));
                return callbackDescriptor;
            }).ToArray();
    }

    public static IEnumerable<CallbackDescriptor> GetPocoEvents(this object value)
    {
        var c = GetType(value).GetEvents().Select(ei => ei.EventHandlerType).Distinct().ToArray();
        return GetPocoEventsForTypes(value, c);
    }

    public static T GetValue<T>(this FieldInfo info, object value)
    {
        return (T) info.GetValue(value);
    }

    public static T GetValue<T>(this PropertyInfo info, object value, object[] index)
    {
        return (T) info.GetValue(value, index);
    }

    public static IEnumerable<FieldInfo> GetInstanceFields(this object value, Func<FieldInfo, bool> selector)
    {
        return GetAllFields(GetType(value)).Where(selector);
    }

    public static IEnumerable<FieldInfo> GetInstanceFields(this object value)
    {
        return value.GetInstanceFields(_ => true);
    }

    public static IEnumerable<FieldInfo> GetAllFields(Type forType)
    {
        if (forType == null)
        {
            return new FieldInfo[0];
        }

        var fields = forType.GetFields(NonPublicInstance | BindingFlags.Public);
        return fields.Concat(GetAllFields(forType.BaseType));
    }

    public static IEnumerable<PropertyInfo> NonPublicInstanceProperties(this object value, Func<PropertyInfo, bool> selector)
    {
        return GetType(value).GetProperties(NonPublicInstance).Where(selector);
    }

    static Type GetType(object value)
    {
        return value == null ? typeof(void) : value.GetType();
    }

    public static IEnumerable<PropertyInfo> NonPublicInstanceProperties(this object value)
    {
        return value.NonPublicInstanceProperties(_ => true);
    }

    public static IEnumerable<FieldInfo> NonPublicStaticFields(this object value, bool includeInherited)
    {
        var typeInfo = value.GetType();
        while (typeInfo != null)
        {
            var fieldInfos = typeInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var fieldInfo in fieldInfos)
            {
                yield return fieldInfo;
            }

            typeInfo = includeInherited ? typeInfo.BaseType : null;
        }
    }

    public static T GetValueForProperty<T>(object instance, string propertyName)
    {
        var propertyInfo = instance.NonPublicInstanceProperties().First(p => p.Name == propertyName);
        return propertyInfo.GetValue<T>(instance, null);
    }
}