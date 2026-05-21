#region

using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Example.Business.Concreate;
using Example.Common.Intercepters;
using Example.Common.Security.Jwt;
using Example.Common.Security.Jwt.Abstract;
using Example.Dal.Concreate.Repositories;
using Module = Autofac.Module;

#endregion

namespace Example.Core.DependencyResolvers
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TokenHelper>().As<ITokenHelper>().InstancePerLifetimeScope();

            var businessAssembly = Assembly.GetAssembly(typeof(AuthManager));
            var dataAccessAssembly = Assembly.GetAssembly(typeof(UserRepository));

            builder.RegisterAssemblyTypes(businessAssembly, dataAccessAssembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions
                {
                    Selector = new AspectInterceptorSelector()
                })
                .InstancePerLifetimeScope();
        }
    }
}