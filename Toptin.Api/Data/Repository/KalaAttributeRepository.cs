using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class KalaAttributeRepository : RepositoryBase<KalaAttribute> , IKalaAttribute
    {
        public KalaAttributeRepository(DataContext context) : base(context)
        {
        }
    }
}
