using UserPermissionApi.Controllers.Schemas;

namespace UserPermissionApi.Services
{
    public interface IRequestPermissionService
    {
        Task Request(RequestPermissionCommand command);
    }
}
