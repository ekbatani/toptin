using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class RequestDetailRepository : RepositoryBase<RequestDetail> , IRequestDetail
    {
        public RequestDetailRepository(DataContext context) : base(context)
        {
        }
    }
}