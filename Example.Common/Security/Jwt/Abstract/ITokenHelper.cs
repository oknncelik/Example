#region

using System.Collections.Generic;
using Example.Common.Security.Jwt.Models;
using Example.Entities.Entities;

#endregion

namespace Example.Common.Security.Jwt.Abstract
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}