using Nest;
using UserPermissionApi.Controllers.Schemas;

namespace UserPermissionApi.Services
{
    public interface IRequestPermissionService
    {
        Task<Result> Request(RequestPermissionCommand command);
    }
}
