using System;
using ApprovalTests.Core;

namespace ApprovalTests.Namers
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
    public class UseNamerAttribute : Attribute
    {
        public Type ForReporterOfType { get; set; }
        public static Func<IApprovalFailureReporter> CurrentReporterRetrievalFunc = () => Approvals.GetReporter();
        public static Action<Func<IApprovalNamer>> RegisterNamerCreatorAction = Approvals.RegisterDefaultNamerCreation;

        public UseNamerAttribute(Type namerType)
        {
            RegisterNamerCreatorAction(() => CreateNamer(namerType, MatchTypeAgainstCurrentReporter));
        }

        public bool MatchTypeAgainstCurrentReporter()
        {
            if (ForReporterOfType != null)
            {
                var currentReporterType = CurrentReporterRetrievalFunc().GetType();
                return currentReporterType == ForReporterOfType;
            }

            return false;
        }

        public static IApprovalNamer CreateNamer(Type reporter, Func<bool> useForCurrentReporter)
        {
            try
            {
                if (useForCurrentReporter != null && useForCurrentReporter())
                {
                    return (IApprovalNamer)Activator.CreateInstance(reporter);
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception) { }
            // ReSharper restore EmptyGeneralCatchClause

            return new UnitTestFrameworkNamer();
        }
    }
}
