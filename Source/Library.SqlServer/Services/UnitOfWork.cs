using System;
using System.Threading.Tasks;
using Library.Core.Contracts;

namespace Library.SqlServer.Services
{
    /// <summary>
    /// Represents the application unit of work service.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dBContext;

        /// <summary>
        /// Default constructor for <see cref="UnitOfWork"/>.
        /// </summary>
        /// <param name="dBContext">The application data base context instance.</param>
        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        #region Interface Methods

        /// <inheritdoc/>
        public void Commit()
        {
            _dBContext.SaveChanges();
        }

        /// <inheritdoc/>
        public Task CommitAsync()
        {
            return _dBContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public IRepository<T> GetRepository<T>() where T : class, IEntity<Guid>
        {
            return new DataRepository<T>(_dBContext);
        }

        #endregion
    }
}
