using System;
using System.Collections.Generic;

namespace CrossTech.Application.Interfaces.Gateways
{
    public interface IGateway<TEntity> where TEntity : class
    {
    }

    public interface IBasicGateway<TEntity> : IGateway<TEntity>
        where TEntity : class
    {
        IList<TEntity> GetAll();
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        void Add(TEntity entity);

        void Remove(TEntity entity);
        void Remove(Func<TEntity, bool> predicate);
    }

    public interface IBasicReadOnlyGateway<TEntity> : IGateway<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
    }
}
