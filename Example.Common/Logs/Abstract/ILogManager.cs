#region

using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;

#endregion

namespace Example.Common.Logs.Abstract
{
    public interface ILogManager
    {
        Task AddLog(IInvocation invoke, TimeSpan interval);
    }
}