using alter.treinamento.business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace alter.treinamento.business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : EntityBase
    {
        IUnitOfWork UnitOfWork { get; }
        void Add(TEntity entity);
        Task<TEntity> GetbyId(Guid id);
        Task<List<TEntity>> GetAll();
        void Update(TEntity entity);
        void Remove(Guid id);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
