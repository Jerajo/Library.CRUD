namespace Library.Core.Contracts
{
    /// <summary>
    /// Base command interface use for dependency injection.
    /// </summary>
    public interface ICommandBase { }

    /// <summary>
    /// Interface for commands.
    /// </summary>
    public interface ICommand<TParam> : ICommandBase
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="param">Represents command parameter.</param>
        void Execute(TParam param);
    }
}
