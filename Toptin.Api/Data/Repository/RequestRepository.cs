using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class RequestRepository : RepositoryBase<Request> , IRequest
    {
        public RequestRepository(DataContext context) : base(context)
        {
        }
    }
}