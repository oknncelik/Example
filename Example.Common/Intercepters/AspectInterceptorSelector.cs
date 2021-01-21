using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Example.Common.Intercepters
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<BaseInterception>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<BaseInterception>(true);
            classAttributes.AddRange(methodAttributes);
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}