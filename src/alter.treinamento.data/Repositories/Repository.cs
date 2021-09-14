using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Models;
using alter.treinamento.data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace alter.treinamento.data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        protected readonly AlterDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(AlterDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public IUnitOfWork UnitOfWork => Db;
        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetbyId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Remove(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
        }
        public void Dispose() => Db?.Dispose();
    }
}
