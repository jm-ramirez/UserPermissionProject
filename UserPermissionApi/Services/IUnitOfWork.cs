using UserPermissionApi.Repositories;

namespace UserPermissionApi.Services
{
    public interface IUnitOfWork
    {
        IPermissionRepository Permissions { get; }
        IPermissionTypeRepository PermissionTypes { get; }
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
