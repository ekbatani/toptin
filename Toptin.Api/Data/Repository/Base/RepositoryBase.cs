using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Toptin.Api.Data.Repository.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private DataContext Context { get; set; }
        private bool _disposed = false;

        protected RepositoryBase(DataContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.Context.Set<T>().ToListAsync();
        }

        public async Task<T> FindAsync(int id)
        {
            return await this.Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.Context.Set<T>().Where(expression).ToListAsync();
        }

        public void Add(T param)
        {
            this.Context.Set<T>().Add(param);
        }

        public void Update(T param)
        {
            this.Context.Set<T>().Update(param);
        }

        public void Remove(T param)
        {
            this.Context.Set<T>().Remove(param);
        }

        public async Task SaveAsync()
        {
            await this.Context.SaveChangesAsync();
        }

        public async Task<bool> Any(int id)
        {
            var result = await this.Context.Set<T>().FindAsync(id);
            if (result == null)
                return false;

            return true;
        }

        public  void Clear()
        {
           this.Context.Set<T>().RemoveRange(this.Context.Set<T>());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
