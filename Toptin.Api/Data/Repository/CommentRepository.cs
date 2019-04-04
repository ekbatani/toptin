using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class CommentRepository : RepositoryBase<Comment> , IComment
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }
    }
}