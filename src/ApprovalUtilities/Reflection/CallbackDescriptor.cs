namespace ApprovalUtilities.Reflection;

using System.Collections.Generic;
using System.Reflection;
using System.Text;

public class CallbackDescriptor
{
    List<MethodInfo> methods = new();

    public void AddMethods(IEnumerable<MethodInfo> methods) =>
        this.methods.AddRange(methods);

    public CallbackDescriptor(string name) =>
        EventName = name;

    public string EventName { get; }

    public void AddMethod(MethodInfo method) =>
        methods.Add(method);

    public List<MethodInfo> GetMethods() => methods;

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"{EventName}:");

        for (var i = 0; i < methods.Count; i++)
        {
            builder.AppendLine($"\t[{i}] {methods[i]}");
        }

        return builder.ToString();
    }
}