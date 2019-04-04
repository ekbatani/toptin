using Toptin.Api.Data.Interfaces.Base;
using Toptin.Api.Models;

namespace Toptin.Api.Data.Interfaces
{
    public interface ILog : IInterfaceBase<Log>
    {
        void Clear();
    }
}