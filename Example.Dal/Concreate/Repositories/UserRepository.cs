#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Dal.Abstract.Repositories;
using Example.Dal.Context;
using Example.Entities.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Example.Dal.Concreate.Repositories
{
    public class UserRepository : BaseContext<User, ExampleContext>, IUserRepository
    {
        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            await using var context = new ExampleContext();
            var result = context.UserOperationClaims
                .Where(x => x.UserId == user.Id)
                .Join(context.OperationClaims, u => u.OperationClaimId, o => o.Id, (u, o) => o);
            return await result.ToListAsync();
        }
    }
}