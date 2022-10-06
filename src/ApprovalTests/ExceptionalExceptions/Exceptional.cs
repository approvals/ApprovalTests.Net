namespace ApprovalTests.ExceptionalExceptions;

public class Exceptional : Exception
{
    public static T Create<T>(string formattableMessage, params object[] messageParameters)
        where T : Exception
    {
        return Create<T>(null, formattableMessage, messageParameters);
    }

    public static T Create<T>(Exception causedBy, string formattableMessage, params object[] messageParameters)
        where T : Exception
    {
        Func<string, Exception, T> reflectiveConstructor = (m, e) =>
        {
            var type = typeof(T);
            var constructorInfo = type.GetConstructor(new[] {typeof(string), typeof(Exception)});
            var instance = (T) constructorInfo.Invoke(new object[] {m, e});
            return instance;
        };
        return Create(reflectiveConstructor, causedBy, formattableMessage, messageParameters);
    }

    public static T Create<T>(Func<string, Exception, T> constructor, Exception causedBy, string formattableMessage,
        params object[] messageParameters)
        where T : Exception
    {
        var message = string.Format(formattableMessage, messageParameters);
        var uid = GenerateUniqueId<T>();
        var tldr = GetTlDr(uid);
        var completeMessage = message + tldr;
        var exception = constructor(completeMessage, causedBy);
        return exception;
    }

    // ReSharper disable once UnusedParameter.Local
    static string GetTlDr(ExceptionalId uid)
    {
        return "";
    }

    public static ExceptionalId GenerateUniqueId<T>()
    {
        var callingMethod = new Caller().Methods.First(m => m.DeclaringType.Namespace != typeof(Exceptional).Namespace);
        return new()
        {
            Assembly = callingMethod.DeclaringType.Assembly.FullName,
            Class = callingMethod.DeclaringType.FullName,
            Method = callingMethod.ToStandardString(),
            Exception = typeof(T).FullName,
        };
    }
}