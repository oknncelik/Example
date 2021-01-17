using Example.Dal.Abstract.Repositories;
using Example.Dal.Context;
using Example.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Dal.Concreate.Repositories
{
    public class OperationClaimRepository : BaseContext<OperationClaim, ExampleContext>, IOperationClaimRepository
    {
    }
}
