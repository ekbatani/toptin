using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class CategoryRepository : RepositoryBase<Category> , ICategory
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }
    }
}