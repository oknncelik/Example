#region



#endregion

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Example.Common.Extensions;
using Example.Common.Ioc;
using Example.Common.Logs.Abstract;
using Example.Common.Results.Abstract;
using Example.Dal.Abstract.Repositories;
using Example.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Common.Logs.Concreate
{
    public class LogManager : ILogManager
    {
        private readonly ILogRepository _logRepository;
        private readonly IHttpContextAccessor _accessor;

        public LogManager()
        {
            _logRepository = ServiceTool.ServiceProvider.GetService<ILogRepository>();
            _accessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public async Task AddLog(IInvocation invoke, TimeSpan interval)
        {
            await Task.Run(() =>
            {
                var userName = _accessor.HttpContext.User.Claims(ClaimTypes.Name)?.FirstOrDefault();
                var request = invoke.Arguments;
                var response = (Task<IResult>) invoke.ReturnValue;
                if (invoke.Method.ReflectedType is { })
                    _logRepository.Add(new Log
                    {
                        LogDate = DateTime.Now,
                        User = userName ?? "Anonymous User",
                        ResultCode = response.Result.Code,
                        Status = response.Result.ResultType,
                        MethodName = $"{invoke.Method.ReflectedType.FullName}.{invoke.Method.Name}",
                        Request = JsonConvert.SerializeObject(request),
                        Response = JsonConvert.SerializeObject(response.Result),
                        Interval = interval
                    });
            });
        }
    }
}