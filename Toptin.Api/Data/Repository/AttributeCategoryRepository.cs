using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class AttributeCategoryRepository : RepositoryBase<AttributeCategory> , IAttributeCategory
    {
        public AttributeCategoryRepository(DataContext context) : base(context)
        {
        }
    }
}
