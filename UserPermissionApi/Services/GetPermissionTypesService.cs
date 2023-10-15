using UserPermissionApi.Model;

namespace UserPermissionApi.Services
{
    public class GetPermissionTypesService : IGetPermissionTypesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionTypesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PermissionTypes> GetAllPermissionTypes()
        {
            try
            {
                return _unitOfWork.PermissionTypes.GetAllAsync().Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
