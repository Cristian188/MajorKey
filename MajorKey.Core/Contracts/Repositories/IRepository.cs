using MajorKey.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MajorKey.Core.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(long entityId);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<int> CountAsync();

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<bool> ExistAsync(long entityId);
    }
}
