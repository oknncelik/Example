using Example.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Dal.Abstract.Repositories
{
    public interface IUserRepository : IContext<User>
    {
        Task<List<OperationClaim>> GetClaims(User user);
    }
}
