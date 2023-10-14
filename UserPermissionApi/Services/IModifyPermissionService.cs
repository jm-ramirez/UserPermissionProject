using UserPermissionApi.Controllers.Schemas;

namespace UserPermissionApi.Services
{
    public interface IModifyPermissionService
    {
        Task Update(ModifyPermissionCommand command);
        Task Delete(int permissionId);
    }
}
