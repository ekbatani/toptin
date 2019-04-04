using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class LogRepository : RepositoryBase<Log> , ILog
    {
        public LogRepository(DataContext context) : base(context)
        {
        }
    }
}
