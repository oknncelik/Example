using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Example.Business.Abstract;
using Example.Business.Concreate;
using Example.Common.Intercepters;
using Example.Common.Security.Jwt;
using Example.Common.Security.Jwt.Abstract;
using Example.Dal.Abstract.Repositories;
using Example.Dal.Concreate.Repositories;

namespace Example.Core.DependencyResolvers
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TokenHelper>().As<ITokenHelper>()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions {Selector = new AspectInterceptorSelector()});

            #region Repositories

            builder.RegisterType<UserRepository>().As<IUserRepository>()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions {Selector = new AspectInterceptorSelector()});
            builder.RegisterType<OperationClaimRepository>().As<IOperationClaimRepository>()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions {Selector = new AspectInterceptorSelector()});
            builder.RegisterType<UserOperationClaimRepository>().As<IUserOperationClaimRepository>()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions {Selector = new AspectInterceptorSelector()});
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions {Selector = new AspectInterceptorSelector()});
            builder.RegisterType<ProductRepository>().As<IProductRepository>()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions {Selector = new AspectInterceptorSelector()});

            #endregion

            #region Managers

            builder.RegisterType<AuthManager>().As<IAuthManager>()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions {Selector = new AspectInterceptorSelector()});
            builder.RegisterType<CategoryManager>().As<ICategoryManager>()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions {Selector = new AspectInterceptorSelector()});
            builder.RegisterType<ProductManager>().As<IProductManager>()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions {Selector = new AspectInterceptorSelector()});

            #endregion

            /*
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();    
            */
        }
    }
}