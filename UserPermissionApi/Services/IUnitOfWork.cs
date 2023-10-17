using UserPermissionApi.Model;
using UserPermissionApi.Repositories;

namespace UserPermissionApi.Services
{
    public interface IUnitOfWork
    {
        IPermissionRepository<Permissions> Permissions { get; }
        IPermissionTypeRepository<PermissionTypes> PermissionTypes { get; }
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
