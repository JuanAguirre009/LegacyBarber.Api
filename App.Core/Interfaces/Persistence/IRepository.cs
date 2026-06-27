using System.Linq.Expressions;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    public interface IRepository<TModel, TId> where TModel : class
    {
        TModel? Get(TId id, params Expression<Func<TModel, object>>[] include);
        void Create(TModel element);
        TModel CreateAndSave(TModel element);
        void Update(TModel element);
        void UpdateAndSave(TModel element);
        Task UpdateAndSaveAsync(TModel element);
        void Delete(TModel element);
        void DeleteAndSave(TModel element);
        Task DeleteAndSaveAsync(TModel element);
        IEnumerable<TModel> GetFiltered(Expression<Func<TModel, bool>> filter, params Expression<Func<TModel, object>>[] includes);

        Task<TModel?> GetAsync(TId id, params Expression<Func<TModel, object>>[] include);
        Task CreateAsync(TModel element);
        Task<TModel> CreateAndSaveAsync(TModel element);
        Task<IEnumerable<TModel>> GetFilteredAsync(Expression<Func<TModel, bool>> filter, params Expression<Func<TModel, object>>[] includes);
        IEnumerable<TModel> GetAll();
        void Save();
        Task SaveAsync();
        string PrimaryKeyName { get; }
    }
}
