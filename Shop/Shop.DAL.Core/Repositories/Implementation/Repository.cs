using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Core.Repositories.Interfaces;

namespace Shop.DAL.Core.Repositories.Implementation
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity: class
    {
        protected readonly ShopContext _context;
        protected readonly DbSet<TEntity> _dbSet;


        public Repository(ShopContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }


        public async Task<TEntity> GetByID(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var entityList = await _dbSet.AsNoTracking().ToListAsync();
            return entityList;
        }

        public IQueryable<TEntity> Get()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task Add(TEntity obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public async Task AddRange(IEnumerable<TEntity> objs)
        {
            await _dbSet.AddRangeAsync(objs);
        }

        public async Task Remove(Guid id)
        {
            var obj = await _dbSet.FindAsync(id);
            Remove(obj);
        }

        public void Remove(TEntity obj)
        {
            _dbSet.Remove(obj);
        }

        public void RemoveRange(IEnumerable<TEntity> objs)
        {
            _dbSet.RemoveRange(objs);
        }

        public void Update(TEntity obj)
        {
            _dbSet.Update(obj);
        }

        public void UpdateRange(IEnumerable<TEntity> objs)
        {
            _dbSet.UpdateRange(objs);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}