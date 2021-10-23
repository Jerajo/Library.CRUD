using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Library.Core.Contracts;

namespace Library.SqlServer.Services
{
    /// <summary>
    /// Represents the <typeparamref name="TEntity"/>'s repository.
    /// </summary>
    /// <typeparam name="TEntity">The Type of the entity.</typeparam>
    public class DataRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity<Guid>
    {
        private readonly ApplicationDBContext _dBContext;

        /// <summary>
        /// Default constructor for <see cref="DataRepository{TEntity}"/>.
        /// </summary>
        /// <param name="dBContext">The application db context instance.</param>
        public DataRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        /// <inheritdoc/>
        public IUnitOfWork GetTransaction()
        {
            return new UnitOfWork(_dBContext);
        }

        #region Queries

        #region GET

        /// <inheritdoc/>
        public TEntity Get(Func<TEntity, bool> query)
        {
            return _dBContext.Set<TEntity>().FirstOrDefault(query);
        }

        /// <inheritdoc/>
        public Task<TEntity> GetAsync(Func<TEntity, bool> query)
        {
            return Task.FromResult(Get(query));
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> GetDataSet()
        {
            return _dBContext.Set<TEntity>();
        }

        /// <inheritdoc/>
        public List<TEntity> Query(Func<TEntity, bool> query)
        {
            return _dBContext.Set<TEntity>().Where(query).ToList();
        }

        /// <inheritdoc/>
        public Task<List<TEntity>> QueryAsync(Func<TEntity, bool> query)
        {
            return Task.FromResult(Query(query));
        }

        /// <inheritdoc/>
        public List<TResult> Query<TResult>(Func<TEntity, bool> query,
            Func<TEntity, TResult> select)
            where TResult : IEntity<Guid>
        {
            var result = _dBContext.Set<TEntity>()
                .Where(query)
                .Select(select)
                .ToList();

            return result;
        }

        #endregion

        #region OPTIONS

        /// <inheritdoc/>
        public bool Any(Func<TEntity, bool> query = null)
        {
            var result = (query != null) ?
                _dBContext.Set<TEntity>().Any(query) :
                _dBContext.Set<TEntity>().Any();

            return result;
        }

        /// <inheritdoc/>
        public Task<bool> AnyAsync(Func<TEntity, bool> query = null)
        {
            return Task.FromResult(Any(query));
        }

        #endregion

        #endregion

        #region Commands

        #region POST

        /// <inheritdoc/>
        public void Add(TEntity entity)
        {
            _dBContext.Set<TEntity>().Add(entity);
        }

        /// <inheritdoc/>
        public Task AddAsync(TEntity entity)
        {
            return Task.Run(() => Add(entity));
        }

        /// <inheritdoc/>
        public void AddGroup(IEnumerable<TEntity> entities)
        {
            _dBContext.Set<TEntity>().AddRange(entities);
        }

        /// <inheritdoc/>
        public Task AddGroupAsync(IEnumerable<TEntity> entities)
        {
            return Task.Run(() => AddGroup(entities));
        }

        #endregion

        #region DELETE

        /// <inheritdoc/>
        public void Delete(TEntity entity)
        {
            var entityToDelete = this.Get(x => x.Id == entity.Id);

            _dBContext.Set<TEntity>().Remove(entityToDelete);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(TEntity entity)
        {
            return Task.Run(() => Delete(entity));
        }

        /// <inheritdoc/>
        public void DeleteGroup(Func<TEntity, bool> query)
        {
            var entitiesToDelete = Query(query);
            _dBContext.Set<TEntity>().RemoveRange(entitiesToDelete);
        }

        /// <inheritdoc/>
        public Task DeleteGroupAsync(Func<TEntity, bool> query)
        {
            return Task.Run(() => DeleteGroup(query));
        }

        #endregion

        #region PUT/PATCH

        /// <inheritdoc/>
        public void Update(TEntity entity)
        {
            _dBContext.Set<TEntity>().Update(entity);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(TEntity entity)
        {
            return Task.Run(() => Update(entity));
        }

        /// <inheritdoc/>
        public void UpdateGroup(TEntity entity, Func<TEntity, bool> query)
        {
            var entitiesToUpdate = Query(query);
            var updatedEntities = entitiesToUpdate.Select(x => UpdateGenericObject(x, entity));
            _dBContext.Set<TEntity>().UpdateRange(updatedEntities);
        }

        /// <inheritdoc/>
        public Task UpdateGroupAsync(TEntity entity, Func<TEntity, bool> query)
        {
            return Task.Run(() => UpdateGroup(entity, query));
        }

        #endregion

        #endregion

        #region Auxiliary Methods

        private TEntity UpdateGenericObject(TEntity entityToUpdate, TEntity entityChanges)
        {
            var toUpdateProperties = entityToUpdate.GetType().GetProperties();
            var changedPropertyInfos = entityChanges.GetType().GetProperties();

            for (var i = 0; i < toUpdateProperties.Length; i++)
            {
                if (toUpdateProperties[i].Name == nameof(entityChanges.Id))
                    continue;
                toUpdateProperties[i].SetValue(entityToUpdate,
                    changedPropertyInfos[i].GetValue(entityChanges));
            }

            return entityToUpdate;
        }

        #endregion
    }
}
