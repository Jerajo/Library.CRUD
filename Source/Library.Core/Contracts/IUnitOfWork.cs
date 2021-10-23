using System;
using System.Threading.Tasks;

namespace Library.Core.Contracts
{
    /// <summary>
    /// Represents a unit of work for saving the changes to the data base.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Get a instance of the current repository associated to this <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <typeparam name="T">Entity for the repository.</typeparam>
        /// <returns></returns>
        IRepository<T> GetRepository<T>() where T : class, IEntity<Guid>;

        /// <summary>
        /// Commit all pending changes to the data base.
        /// </summary>
        void Commit();

        /// <summary>
        /// <see langword="async"/> Commit  all pending changes to the data base.
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
    }
}
