using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class QuestionRepository : RepositoryBase<Question> , IQuestion
    {
        public QuestionRepository(DataContext context) : base(context)
        {
        }
    }
}