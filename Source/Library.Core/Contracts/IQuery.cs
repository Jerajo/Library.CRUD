namespace Library.Core.Contracts
{
    /// <summary>
    /// Represents the query interface.
    /// </summary>
    /// <typeparam name="TQuery">Query options.</typeparam>
    /// <typeparam name="TResult">Query result.</typeparam>
    public interface IQuery<TQuery, TResult> : IQueryBase
    {
        /// <summary>
        /// Execute the query.
        /// </summary>
        /// <param name="query">Query that will be perform in the data base.</param>
        /// <returns>Returns <typeparamref name="TResult"/>.</returns>
        TResult Execute(TQuery query);
    }

    /// <summary>
    /// The base query interface used for dependency injection.
    /// </summary>
    public interface IQueryBase { }
}
