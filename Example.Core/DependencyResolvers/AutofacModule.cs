using Autofac;
using Example.Business.Abstract;
using Example.Business.Concreate;
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
            
            builder.RegisterType<TokenHelper>().As<ITokenHelper>();

            #region Repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<OperationClaimRepository>().As<IOperationClaimRepository>();
            builder.RegisterType<UserOperationClaimRepository>().As<IUserOperationClaimRepository>();
            #endregion

            #region Managers
            builder.RegisterType<AuthManager>().As<IAuthManager>();
            #endregion        
        }
    }
}
