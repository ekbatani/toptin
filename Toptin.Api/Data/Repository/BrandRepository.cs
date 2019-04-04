using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class BrandRepository : RepositoryBase<Brand> , IBrand
    {
        public BrandRepository(DataContext context) : base(context)
        {
        }
    }
}
