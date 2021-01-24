#region

using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Entities.Entities;

#endregion

namespace Example.Dal.Abstract.Repositories
{
    public interface IUserRepository : IContext<User>
    {
        Task<List<OperationClaim>> GetClaims(User user);
    }
}