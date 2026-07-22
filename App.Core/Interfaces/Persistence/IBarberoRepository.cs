using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    public interface IBarberoRepository : IRepository<Barbero, long>
    {
        Task<IReadOnlyList<Barbero>> GetAllByBarberiaAsync(long barberiaId, CancellationToken cancellationToken = default);
    }
}
