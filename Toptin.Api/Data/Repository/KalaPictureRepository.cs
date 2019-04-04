using Toptin.Api.Data.Interfaces;
using Toptin.Api.Models;
using Toptin.Api.Data.Repository.Base;

namespace Toptin.Api.Data.Repository
{
    public class KalaPictureRepository : RepositoryBase<KalaPicture> , IKalaPicture
    {
        public KalaPictureRepository(DataContext context) : base(context)
        {
        }
    }
}