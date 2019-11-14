namespace ApprovalUtilities.Reflection
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public class CallbackDescriptor
    {
        private List<MethodInfo> Methods = new List<MethodInfo>();

        public void AddMethods(IEnumerable<MethodInfo> methods)
        {
            foreach (var m in methods)
            {
                AddMethod(m);
            }
        }

        public CallbackDescriptor(string name)
        {
            EventName = name;
        }

        public string EventName { get; private set; }

        public void AddMethod(MethodInfo method)
        {
            Methods.Add(method);
        }

        public List<MethodInfo> GetMethods()
        {
            return Methods;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{EventName}:");

            for (var i = 0; i < Methods.Count; i++)
            {
                sb.AppendLine($"\t[{i}] {Methods[i]}");
            }

            return sb.ToString();
        }
    }
}