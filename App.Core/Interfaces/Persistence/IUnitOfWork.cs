namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Coordinates the persistence of changes across one or more repositories
    /// within a single transaction.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all changes made in this unit of work to the underlying data store.
        /// </summary>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The number of state entries written to the data store.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
