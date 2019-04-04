using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class StoreRepository : RepositoryBase<Store> , IStore
    {
        public StoreRepository(DataContext context) : base(context)
        {
        }
    }
}