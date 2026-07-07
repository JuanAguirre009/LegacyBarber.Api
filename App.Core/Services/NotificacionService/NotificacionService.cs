using LegacyBarber.App.Core.Interfaces.Persistence;

namespace LegacyBarber.App.Core.Services.NotificacionService
{
    /// <summary>
    /// Default implementation of <see cref="INotificacionService"/>.
    /// </summary>
    public sealed class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository notificacionRepository;
        private readonly IUnitOfWork unitOfWork;

        public NotificacionService(INotificacionRepository notificacionRepository, IUnitOfWork unitOfWork)
        {
            this.notificacionRepository = notificacionRepository;
            this.unitOfWork = unitOfWork;
        }
    }
}
