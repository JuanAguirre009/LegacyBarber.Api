using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class ConfiguracionRepository : BaseRepository<Configuracion, long>, IConfiguracionRepository
    {
        public ConfiguracionRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
