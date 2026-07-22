using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Services.CategoriaServicioService
{
    public sealed class CategoriaServicioService : ICategoriaServicioService
    {
        private readonly ICategoriaServicioRepository categoriaRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoriaServicioService(ICategoriaServicioRepository categoriaRepository, IUnitOfWork unitOfWork)
        {
            this.categoriaRepository = categoriaRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<CategoriaModel>> GetAllAsync(long barberiaId, CancellationToken cancellationToken = default)
        {
            IReadOnlyList<CategoriaServicio> categorias = await categoriaRepository
                .GetAllByBarberiaAsync(barberiaId, cancellationToken);

            return categorias
                .Select(MapToModel)
                .ToList();
        }

        public async Task<CategoriaModel> CreateAsync(long barberiaId, CrearCategoriaModel model, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(model);

            string nombre = model.Nombre.Trim();
            IReadOnlyList<CategoriaServicio> existing = await categoriaRepository.GetFilteredAsync(
                c => c.BarberiaId == barberiaId && c.Nombre == nombre,
                cancellationToken);

            if (existing.Count > 0)
                throw new InvalidOperationException("Ya existe una categoría con ese nombre en tu barbería.");

            var categoria = new CategoriaServicio
            {
                BarberiaId = barberiaId,
                Nombre = nombre,
                Descripcion = model.Descripcion?.Trim(),
                Orden = model.Orden
            };

            categoriaRepository.Add(categoria);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToModel(categoria);
        }

        public async Task<CategoriaModel> UpdateAsync(long barberiaId, long categoriaId, ActualizarCategoriaModel model, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(model);

            CategoriaServicio? categoria = await categoriaRepository.GetByIdAsync(categoriaId, cancellationToken);
            if (categoria is null || categoria.BarberiaId != barberiaId)
                throw new KeyNotFoundException("La categoría no existe.");

            string nombre = model.Nombre.Trim();
            IReadOnlyList<CategoriaServicio> duplicates = await categoriaRepository.GetFilteredAsync(
                c => c.BarberiaId == barberiaId && c.Nombre == nombre && c.Id != categoriaId,
                cancellationToken);

            if (duplicates.Count > 0)
                throw new InvalidOperationException("Ya existe otra categoría con ese nombre en tu barbería.");

            categoria.Nombre = nombre;
            categoria.Descripcion = model.Descripcion?.Trim();
            categoria.Orden = model.Orden;
            categoria.Activa = model.Activa;

            categoriaRepository.Update(categoria);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToModel(categoria);
        }

        public async Task<bool> DeleteAsync(long barberiaId, long categoriaId, CancellationToken cancellationToken = default)
        {
            CategoriaServicio? categoria = await categoriaRepository.GetByIdAsync(categoriaId, cancellationToken);
            if (categoria is null || categoria.BarberiaId != barberiaId)
                return false;

            categoriaRepository.Delete(categoria);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        private static CategoriaModel MapToModel(CategoriaServicio categoria)
        {
            return new CategoriaModel
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Orden = categoria.Orden,
                Activa = categoria.Activa,
                CantidadServicios = categoria.Servicios.Count
            };
        }
    }
}
