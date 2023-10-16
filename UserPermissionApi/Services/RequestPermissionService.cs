//using Nest;
using Nest;
using UserPermissionApi.Controllers.Schemas;
using UserPermissionApi.Model;

namespace UserPermissionApi.Services
{
    public class RequestPermissionService : IRequestPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestPermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Request(RequestPermissionCommand command)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                var permissionType = _unitOfWork.PermissionTypes.GetByNameAsync(command.TipoPermisoNombre).Result;

                if (permissionType == null)
                {
                    // Si no existe, crea un nuevo tipo de permiso
                    permissionType = new PermissionTypes { Descripcion = command.TipoPermisoNombre };
                    await _unitOfWork.PermissionTypes.AddAsync(permissionType);
                }

                // Realiza operaciones en varios repositorios
                await _unitOfWork.Permissions.AddAsync(new Permissions
                {
                    NombreEmpleado = command.NombreEmpleado,
                    ApellidoEmpleado = command.ApellidoEmpleado,
                    FechaPermiso = command.FechaPermiso,
                    TipoPermiso = permissionType.Id,
                });

                // Confirma la transacción
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();

                return Result.Created;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return Result.Error;
            }
        }
    }
}
