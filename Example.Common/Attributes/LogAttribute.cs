#region

using System;
using Castle.DynamicProxy;
using Example.Common.Intercepters;
using Example.Common.Ioc;
using Example.Common.Logs.Abstract;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Example.Common.Attributes
{
    public class LogAttribute : MethodInterception
    {
        private readonly ILogManager _logManager;

        public LogAttribute()
        {
            _logManager = ServiceTool.ServiceProvider.GetService<ILogManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var start = DateTime.Now;
            invocation.Proceed();
            var end = DateTime.Now;
            _logManager.AddLog(invocation, end - start);
        }
    }
}