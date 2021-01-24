#region

using Example.Dal.Abstract.Repositories;
using Example.Dal.Context;
using Example.Entities.Entities;

#endregion

namespace Example.Dal.Concreate.Repositories
{
    public class LogRepository : BaseContext<Log, ExampleContext>, ILogRepository
    {
    }
}