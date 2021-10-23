namespace Library.Core.Contracts
{
    /// <summary>
    /// Represents the command factory interface.
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Gets a instance of a command using the command type.
        /// </summary>
        /// <typeparam name="TCommand">The command type from <see cref="ICommandBase"/></typeparam>
        /// <returns>Returns <typeparamref name="TCommand"/>.</returns>
        TCommand MakeCommand<TCommand>() where TCommand : ICommandBase;
    }
}
