using Example.Dal.Abstract.Repositories;
using Example.Dal.Context;
using Example.Entities.Entities;

namespace Example.Dal.Concreate.Repositories
{
    public class UserOperationClaimRepository : BaseContext<UserOperationClaim, ExampleContext>,IUserOperationClaimRepository
    {
    }
}
