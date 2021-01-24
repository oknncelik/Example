#region

using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Example.Common.Ioc;
using Example.Common.Logs.Abstract;
using Example.Common.Results.Abstract;
using Example.Dal.Abstract.Repositories;
using Example.Entities.Entities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

#endregion

namespace Example.Common.Logs.Concreate
{
    public class LogManager : ILogManager
    {
        private readonly ILogRepository _logRepository;

        public LogManager()
        {
            _logRepository = ServiceTool.ServiceProvider.GetService<ILogRepository>();
        }

        public async Task AddLog(IInvocation invoke, TimeSpan interval)
        {
            await Task.Run(() =>
            {
                var request = invoke.Arguments;
                var response = (Task<IResult>) invoke.ReturnValue;
                _logRepository.Add(new Log
                {
                    LogDate = DateTime.Now,
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