using MajorKey.Core.Contracts.Repositories;
using MajorKey.Core.Models.Entities;
using MajorKey.Insfrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MajorKey.Insfrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationContext _context;
        public Repository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(long entityId)
        {
            return await _context.Set<TEntity>().FindAsync(entityId);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<bool> ExistAsync(long entityId)
        {
            return _context.Set<TEntity>().AnyAsync(x => x.Id == entityId);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
