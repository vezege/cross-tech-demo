using CrossTech.Application.Interfaces.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossTech.Persistence.Gateways
{
    /// <summary>
    /// Базовый класс, позволяющий только чтение данных из хранилища
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BasicReadOnlyGateway<TEntity> : IBasicReadOnlyGateway<TEntity> where TEntity : class
    {
        protected readonly IEnumerable<TEntity> _entities;

        public BasicReadOnlyGateway()
        {
            _entities = InitializeCollection();
        }

        protected abstract IEnumerable<TEntity> InitializeCollection();

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _entities.Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities;
        }
    }
}
