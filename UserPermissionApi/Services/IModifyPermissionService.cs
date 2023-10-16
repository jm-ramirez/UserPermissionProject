using Nest;
using UserPermissionApi.Controllers.Schemas;

namespace UserPermissionApi.Services
{
    public interface IModifyPermissionService
    {
        Task<Result> Update(ModifyPermissionCommand command);
        Task Delete(int permissionId);
    }
}
