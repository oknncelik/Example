#region

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

#endregion

namespace Example.Common.Attributes
{
    public class AuthAttribute : MethodInterception
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly string[] _roles;

        public AuthAttribute(string roles)
        {
            _roles = roles.Split(',');
            _accessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var claims = _accessor.HttpContext.User.ClaimRole();
            if (claims.Any(claim => _roles.Contains(claim)))
            {
                invocation.Proceed();
                return;
            }
            invocation.ReturnValue = AuthorizationDeniedResult();
        }

        private static async Task<IResult> AuthorizationDeniedResult()
        {
            return await Task.Run(() => new WarningResult(403, Messages.AuthorizationDenied));
        }
    }
}