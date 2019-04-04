using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class KalaRepository : RepositoryBase<Kala> , IKala
    {
        public KalaRepository(DataContext context) : base(context)
        {
        }
    }
}