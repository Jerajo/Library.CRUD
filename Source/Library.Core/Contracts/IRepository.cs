using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Library.Core.Contracts
{
    /// <summary>
    /// Represents the <typeparamref name="TEntity"/>'s repository.
    /// </summary>
    /// <typeparam name="TEntity">The Type of the entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class, IEntity<Guid>
    {
        /// <summary>
        /// Get a instance of the associated unit of work with this repository.
        /// </summary>
        /// <returns>Returns <see cref="IUnitOfWork"/>.</returns>
        IUnitOfWork GetTransaction();

        #region GET

        /// <summary>
        /// Get the selected entity's registry.
        /// </summary>
        /// <param name="query">Query to search the entity on the data base.</param>
        /// <returns>Returns <typeparamref name="TEntity"/>.</returns>
        TEntity Get(Func<TEntity, bool> query);

        /// <summary>
        /// <see langword="async"/> Get the selected entity's registry.
        /// </summary>
        /// <param name="query">Query to search the entity on the data base.</param>
        /// <returns>Returns <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> GetAsync(Func<TEntity, bool> query);

        /// <summary>
        /// Get a query able model that allows to build the query
        /// before retrieving the data from the data base.
        /// </summary>
        /// <returns>Returns <see cref="IQueryable{TEntity}"/>.</returns>
        IQueryable<TEntity> GetDataSet();

        /// <summary>
        /// Get the selected entity's registries.
        /// </summary>
        /// <param name="query">Query to search the entity on the data base.</param>
        /// <returns>Returns <see cref="List{TEntity}"/>.</returns>
        List<TEntity> Query(Func<TEntity, bool> query);

        /// <summary>
        /// <see langword="async"/> Get the selected entity's registries.
        /// </summary>
        /// <param name="query">Query to search the entity on the data base.</param>
        /// <returns>Returns <see cref="List{TEntity}"/>.</returns>
        Task<List<TEntity>> QueryAsync(Func<TEntity, bool> query);

        /// <summary>
        /// Get the selected entity's registries.
        /// </summary>
        /// <typeparam name="TResult">The Type to cast the registries on.</typeparam>
        /// <param name="query">Query to search the entity on the data base.</param>
        /// <param name="select">Cast operation executed on each registry.</param>
        /// <returns>Returns <see cref="List{TResult}"/>.</returns>
        List<TResult> Query<TResult>(Func<TEntity, bool> query,
            Func<TEntity, TResult> select)
            where TResult : IEntity<Guid>;

        /// <summary>
        /// Indicates if the registry is in the data base or not.
        /// </summary>
        /// <param name="query">Query to search the entity on the data base.</param>
        /// <returns>Returns <see langword="true"/> or <see langword="false"/>.</returns>
        bool Any(Func<TEntity, bool> query = null);

        /// <summary>
        /// <see langword="async"/> Indicates if the registry is in the data base or not.
        /// </summary>
        /// <param name="query">Query to search the entity on the data base.</param>
        /// <returns>Returns <see langword="true"/> or <see langword="false"/>.</returns>
        Task<bool> AnyAsync(Func<TEntity, bool> query = null);

        #endregion

        #region POST

        /// <summary>
        /// Add the given <paramref name="entity"/> to the data base.
        /// </summary>
        /// <param name="entity">The <paramref name="entity"/> to be inserted into the data base.</param>
        void Add(TEntity entity);

        /// <summary>
        /// <see langword="async"/> Add the given <paramref name="entity"/> to the data base.
        /// </summary>
        /// <param name="entity">The <paramref name="entity"/> to be inserted into the data base.</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Add a group of given <paramref name="entities"/> to the data base.
        /// </summary>
        /// <param name="entities">The <paramref name="entities"/> to be inserted into the data base.</param>
        void AddGroup(IEnumerable<TEntity> entities);

        /// <summary>
        /// <see langword="async"/> Add a group of given <paramref name="entities"/> to the data base.
        /// </summary>
        /// <param name="entities">The <paramref name="entities"/> to be inserted into the data base.</param>
        Task AddGroupAsync(IEnumerable<TEntity> entities);

        #endregion

        #region PUT/PATCH

        /// <summary>
        /// Update the given <paramref name="entity"/> from the data base.
        /// </summary>
        /// <param name="entity">The <paramref name="entity"/> to be updated from the data base.</param>
        void Update(TEntity entity);

        /// <summary>
        /// <see langword="async"/> Update the given <paramref name="entity"/> from the data base.
        /// </summary>
        /// <param name="entity">The <paramref name="entity"/> to be updated from the data base.</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Update a group of entities from the data base.
        /// </summary>
        /// <param name="entity">The data to update the entities from the data base.</param>
        /// <param name="query">Query to select the registries on the data base.</param>
        void UpdateGroup(TEntity entity, Func<TEntity, bool> query);

        /// <summary>
        /// <see langword="async"/> Update a group of entities from the data base.
        /// </summary>
        /// <param name="entity">The data to update the entities from the data base.</param>
        /// <param name="query">Query to select the registries on the data base.</param>
        Task UpdateGroupAsync(TEntity entity, Func<TEntity, bool> query);

        #endregion

        #region DELETE

        /// <summary>
        /// Delete the given <paramref name="entity"/> from the data base.
        /// </summary>
        /// <param name="entity">The <paramref name="entity"/> to be deleted from the data base.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// <see langword="async"/> Delete the given <paramref name="entity"/> from the data base.
        /// </summary>
        /// <param name="entity">The <paramref name="entity"/> to be deleted from the data base.</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete the selected registries on the data base.
        /// </summary>
        /// <param name="query">Query to select the registries that will be deleted on the data base.</param>
        void DeleteGroup(Func<TEntity, bool> query);

        /// <summary>
        /// <see langword="async"/> Delete the selected registries on the data base.
        /// </summary>
        /// <param name="query">Query to select the registries that will be deleted on the data base.</param>
        Task DeleteGroupAsync(Func<TEntity, bool> query);

        #endregion
    }
}
