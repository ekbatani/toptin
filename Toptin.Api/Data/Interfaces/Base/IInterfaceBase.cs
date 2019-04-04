using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toptin.Api.Data.Interfaces.Base
{
    public interface IInterfaceBase<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(int id);
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        void Add(T param);
        void Update(T param);
        void Remove(T param);
        Task SaveAsync();
        Task<bool> Any(int id);
    }
}
