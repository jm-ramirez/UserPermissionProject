using UserPermissionApi.Model;

namespace UserPermissionApi.Services
{
    public interface IGetPermissionService
    {
        IEnumerable<Permissions> GetAllPermissions();
        Permissions GetPermissionById(int permissionId);
    }
}
