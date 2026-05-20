#region

using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Example.Common.Constants;
using Example.Common.Extensions;
using Example.Common.Intercepters;
using Example.Common.Ioc;
using Example.Common.Results;
using Example.Common.Results.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IResult = Example.Common.Results.Abstract.IResult;

#endregion

namespace Example.Common.Attributes
{
    public class AuthAttribute : MethodInterception
    {
        private readonly IHttpContextAccessor _accessor;
        public string[] Roles { get; }

        public AuthAttribute(string roles)
        {
            Roles = roles.Split(',');
            _accessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var claims = _accessor.HttpContext.User.ClaimRole();
            if (claims.Any(claim => Roles.Contains(claim)))
            {
                invocation.Proceed();
                return;
            }

            var result = new WarningResult(403, Messages.AuthorizationDenied);
            
            // Metot asenkron ise (Task veya Task<T> dönüyorsa) dönüş değerini Task.FromResult ile sarmalamalıyız.
            if (typeof(Task).IsAssignableFrom(invocation.Method.ReturnType))
            {
                // Task<IResult> tipine uygun bir Task oluşturuyoruz.
                invocation.ReturnValue = Task.FromResult<IResult>(result);
            }
            else
            {
                invocation.ReturnValue = result;
            }
        }
    }
}