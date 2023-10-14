using UserPermissionApi.Model;

namespace UserPermissionApi.Services
{
    public class GetPermissionService : IGetPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Permissions> GetAllPermissions()
        {
            try
            {
                return _unitOfWork.Permissions.GetAllAsync().Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Permissions GetPermissionById(int permissionId)
        {
            try
            {
                return _unitOfWork.Permissions.GetByIdAsync(permissionId).Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
