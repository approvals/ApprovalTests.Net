using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace ApprovalTests.Asp.Mvc
{
    public static class ReflectionUtility
    {
        public static string GetControllerName<T>()
            where T : IController
        {
            return typeof (T).Name.Replace("Controller", string.Empty);
        }

        public static string GetMethodName<T>(Expression<Func<T, Func<ActionResult>>> expression)
        {
            var unaryExpression = (UnaryExpression) expression.Body;
            var methodCallExpression = (MethodCallExpression) unaryExpression.Operand;
            var constantExpression = (ConstantExpression) methodCallExpression.Object;
            var methodInfo = (MemberInfo) constantExpression.Value;
            return methodInfo.Name;
        }
    }
}