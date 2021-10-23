using System;

namespace Library.Core.Contracts
{
    /// <summary>
    /// Represents the repository factory interface.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets a instance of a repository using the repository type.
        /// </summary>
        /// <typeparam name="TEntity">The entity form the data base.</typeparam>
        /// <returns>Returns <see cref="IRepository{TEntity}"/>.</returns>
        IRepository<TEntity> MakeRepository<TEntity>() where TEntity : class, IEntity<Guid>;
    }
}
