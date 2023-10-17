using UserPermissionApi.Model;

namespace UserPermissionApi.Services
{
    public interface IGetPermissionTypesService
    {
        IEnumerable<PermissionTypes> GetAllPermissionTypes();
    }
}
