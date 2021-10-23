namespace Library.Core.Contracts
{
    /// <summary>
    /// Represents the query factory interface.
    /// </summary>
    public interface IQueryFactory
    {
        /// <summary>
        /// Gets a instance of a query using the query type.
        /// </summary>
        /// <typeparam name="TQuery">The query type from <see cref="IQueryBase"/></typeparam>
        /// <returns>Returns <typeparamref name="TQuery"/>.</returns>
        TQuery MakeQuery<TQuery>() where TQuery : IQueryBase;
    }
}
