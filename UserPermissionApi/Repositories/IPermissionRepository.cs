using UserPermissionApi.Model;

namespace UserPermissionApi.Repositories
{
    public interface IPermissionRepository<T> where T : class
    {
        Task<Permissions> GetByIdAsync(int id);
        Task<IEnumerable<Permissions>> GetAllAsync();
        Task AddAsync(Permissions permission);
        Task UpdateAsync(Permissions permission);
        Task DeleteAsync(int id);
    }
}
