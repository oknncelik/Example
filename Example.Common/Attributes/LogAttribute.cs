using System.Diagnostics;
using Castle.DynamicProxy;
using Example.Common.Intercepters;

namespace Example.Common.Attributes
{
    public class LogAttribute : MethodInterception
    {
        protected override void OnBefore(IInvocation invocation)
        {
            Debug.WriteLine(invocation.Method.Name);
        }
    }
}