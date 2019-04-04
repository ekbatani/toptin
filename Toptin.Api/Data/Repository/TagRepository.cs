using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class TagRepository : RepositoryBase<Tag> , ITag
    {
        public TagRepository(DataContext context) : base(context)
        {
        }
    }
}