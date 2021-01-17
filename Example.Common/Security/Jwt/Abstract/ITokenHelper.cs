using Example.Common.Security.Jwt.Models;
using Example.Entities.Entities;
using System.Collections.Generic;

namespace Example.Common.Security.Jwt.Abstract
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
