#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Example.Common.Cachings.Abstract;
using Example.Common.Constants;
using Example.Common.Extensions;
using Example.Common.Intercepters;
using Example.Common.Ioc;
using Example.Common.Results;
using Example.Dal.Abstract.Repositories;
using Example.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IResult = Example.Common.Results.Abstract.IResult;

#endregion

namespace Example.Common.Attributes
{
    public class AuthAttribute : MethodInterception
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ICacheManager _cacheManager;
        private readonly IUserRepository _userRepository;

        public string[] Roles { get; }

        public AuthAttribute(string roles)
        {
            Roles = roles.Split(',');
            _accessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            _userRepository = ServiceTool.ServiceProvider.GetService<IUserRepository>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var user = _accessor.HttpContext.User;
            if (user.Identity is { IsAuthenticated: true })
            {
                var userId = user.GetUserId();
                var cacheKey = $"user-claims-{userId}";

                if (!_cacheManager.IsAdded(cacheKey))
                {
                    var claims = _userRepository.GetClaims(new User { Id = userId }).GetAwaiter().GetResult();
                    var roles = claims.Select(x => x.Name).ToList();
                    _cacheManager.Add(cacheKey, roles, 1); // 1 minute cache for fresh permissions
                }

                var userRoles = _cacheManager.Get<List<string>>(cacheKey);

                if (userRoles.Any(role => Roles.Contains(role)))
                {
                    invocation.Proceed();
                    return;
                }
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