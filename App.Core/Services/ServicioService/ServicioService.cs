using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Services.ServicioService
{
    public sealed class ServicioService : IServicioService
    {
        private readonly IServicioRepository servicioRepository;
        private readonly ICategoriaServicioRepository categoriaServicioRepository;
        private readonly IUnitOfWork unitOfWork;

        public ServicioService(
            IServicioRepository servicioRepository,
            ICategoriaServicioRepository categoriaServicioRepository,
            IUnitOfWork unitOfWork)
        {
            this.servicioRepository = servicioRepository;
            this.categoriaServicioRepository = categoriaServicioRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<ServicioModel>> GetAllAsync(long barberiaId, long? categoriaId, CancellationToken cancellationToken = default)
        {
            IReadOnlyList<Servicio> servicios = await servicioRepository.GetFilteredAsync(
                s => s.BarberiaId == barberiaId && (!categoriaId.HasValue || s.CategoriaId == categoriaId),
                cancellationToken);

            return servicios.Select(MapToModel).ToList();
        }

        public async Task<ServicioModel> GetByIdAsync(long barberiaId, long servicioId, CancellationToken cancellationToken = default)
        {
            Servicio? servicio = await servicioRepository.GetByIdAsync(servicioId, cancellationToken);
            if (servicio is null || servicio.BarberiaId != barberiaId)
                throw new KeyNotFoundException("El servicio no existe.");

            return MapToModel(servicio);
        }

        public async Task<ServicioModel> CreateAsync(long barberiaId, CrearServicioModel model, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(model);

            string nombre = model.Nombre.Trim();

            IReadOnlyList<Servicio> existing = await servicioRepository.GetFilteredAsync(
                s => s.BarberiaId == barberiaId && s.Nombre == nombre,
                cancellationToken);

            if (existing.Count > 0)
                throw new InvalidOperationException("Ya existe un servicio con ese nombre en tu barbería.");

            await ValidarCategoriaPerteneceAsync(model.CategoriaId, barberiaId, cancellationToken);

            var servicio = new Servicio
            {
                BarberiaId = barberiaId,
                Nombre = nombre,
                Descripcion = model.Descripcion?.Trim(),
                DuracionMinutos = model.DuracionMinutos,
                Precio = model.Precio,
                CategoriaId = model.CategoriaId,
                Color = model.Color,
                ImagenId = model.ImagenId
            };

            servicioRepository.Add(servicio);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToModel(servicio);
        }

        public async Task<ServicioModel> UpdateAsync(long barberiaId, long servicioId, ActualizarServicioModel model, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(model);

            Servicio? servicio = await servicioRepository.GetByIdAsync(servicioId, cancellationToken);
            if (servicio is null || servicio.BarberiaId != barberiaId)
                throw new KeyNotFoundException("El servicio no existe.");

            string nombre = model.Nombre.Trim();

            IReadOnlyList<Servicio> duplicates = await servicioRepository.GetFilteredAsync(
                s => s.BarberiaId == barberiaId && s.Nombre == nombre && s.Id != servicioId,
                cancellationToken);

            if (duplicates.Count > 0)
                throw new InvalidOperationException("Ya existe otro servicio con ese nombre en tu barbería.");

            await ValidarCategoriaPerteneceAsync(model.CategoriaId, barberiaId, cancellationToken);

            servicio.Nombre = nombre;
            servicio.Descripcion = model.Descripcion?.Trim();
            servicio.DuracionMinutos = model.DuracionMinutos;
            servicio.Precio = model.Precio;
            servicio.CategoriaId = model.CategoriaId;
            servicio.Color = model.Color;
            servicio.ImagenId = model.ImagenId;
            servicio.ActualizadoEn = DateTime.UtcNow;

            servicioRepository.Update(servicio);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToModel(servicio);
        }

        public async Task<ServicioModel> ToggleActivoAsync(long barberiaId, long servicioId, bool activo, CancellationToken cancellationToken = default)
        {
            Servicio? servicio = await servicioRepository.GetByIdAsync(servicioId, cancellationToken);
            if (servicio is null || servicio.BarberiaId != barberiaId)
                throw new KeyNotFoundException("El servicio no existe.");

            servicio.Activo = activo;
            servicio.ActualizadoEn = DateTime.UtcNow;

            servicioRepository.Update(servicio);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToModel(servicio);
        }

        public async Task<bool> DeleteAsync(long barberiaId, long servicioId, CancellationToken cancellationToken = default)
        {
            Servicio? servicio = await servicioRepository.GetByIdAsync(servicioId, cancellationToken);
            if (servicio is null || servicio.BarberiaId != barberiaId)
                return false;

            servicioRepository.Delete(servicio);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        private async Task ValidarCategoriaPerteneceAsync(long? categoriaId, long barberiaId, CancellationToken cancellationToken)
        {
            if (!categoriaId.HasValue)
                return;

            CategoriaServicio? categoria = await categoriaServicioRepository.GetByIdAsync(categoriaId.Value, cancellationToken);
            if (categoria is null || categoria.BarberiaId != barberiaId)
                throw new InvalidOperationException("La categoría seleccionada no pertenece a tu barbería.");
        }

        private static ServicioModel MapToModel(Servicio servicio)
        {
            return new ServicioModel
            {
                Id = servicio.Id,
                Nombre = servicio.Nombre,
                Descripcion = servicio.Descripcion,
                DuracionMinutos = servicio.DuracionMinutos,
                Precio = servicio.Precio,
                Color = servicio.Color,
                Activo = servicio.Activo,
                CategoriaId = servicio.CategoriaId,
                CategoriaNombre = servicio.Categoria?.Nombre,
                ImagenId = servicio.ImagenId,
                CreadoEn = servicio.CreadoEn
            };
        }
    }
}
