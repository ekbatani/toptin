using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class ColorRepository : RepositoryBase<Color> , IColor
    {
        public ColorRepository(DataContext context) : base(context)
        {
        }
    }
}
