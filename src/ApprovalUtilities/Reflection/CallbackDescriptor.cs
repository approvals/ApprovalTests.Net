namespace ApprovalUtilities.Reflection;

using System.Collections.Generic;
using System.Reflection;
using System.Text;

public class CallbackDescriptor
{
    List<MethodInfo> Methods = new();

    public void AddMethods(IEnumerable<MethodInfo> methods)
    {
        foreach (var m in methods)
        {
            AddMethod(m);
        }
    }

    public CallbackDescriptor(string name) =>
        EventName = name;

    public string EventName { get; private set; }

    public void AddMethod(MethodInfo method) =>
        Methods.Add(method);

    public List<MethodInfo> GetMethods() => Methods;

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"{EventName}:");

        for (var i = 0; i < Methods.Count; i++)
        {
            builder.AppendLine($"\t[{i}] {Methods[i]}");
        }

        return builder.ToString();
    }
}