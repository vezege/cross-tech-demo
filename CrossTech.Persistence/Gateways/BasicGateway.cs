using CrossTech.Application.Interfaces.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossTech.Persistence.Gateways
{
    /// <summary>
    /// Базовый класс, отвечающий за хренение и манипулирование данными
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BasicGateway<TEntity> : IBasicGateway<TEntity> where TEntity : class
    {
        /// <summary>
        /// Хранящиеся в данный момент сущности
        /// </summary>
        protected readonly IList<TEntity> _entities;

        public BasicGateway()
        {
            _entities = InitializeCollection();
        }

        /// <summary>
        /// Метод заполнения коллекции сущностей
        /// </summary>
        protected virtual IList<TEntity> InitializeCollection()
        {
            return new List<TEntity>();
        }

        /// <summary>
        /// Добавление новой сущности в список
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _entities.Where(predicate);
        }

        public IList<TEntity> GetAll()
        {
            return _entities;
        }

        public void Remove(TEntity entity)
        {
            if (entity != null)
            {
                _entities.Remove(entity);
            }
            else
            {
                throw new Exception("Невозможно удалить запись, т.к. её не существует");
            }
        }

        public void Remove(Func<TEntity, bool> predicate)
        {
            TEntity entity = _entities.Where(predicate).First();
            Remove(entity);
        }
    }
}
