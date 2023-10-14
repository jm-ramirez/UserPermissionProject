using Microsoft.AspNetCore.Mvc;
using UserPermissionApi.Services;

[ApiController]
[Route("api/getpermissions")]
public class GetPermissionsController : ControllerBase
{
    private readonly IGetPermissionService _getPermissionService;

    public GetPermissionsController(IGetPermissionService getPermissionService)
    {
        _getPermissionService = getPermissionService;
    }

    [HttpGet]
    public IActionResult GetAllPermissions()
    {
        try
        {
            var permissions = _getPermissionService.GetAllPermissions();

            if (permissions != null)
            {
                return Ok(permissions);
            }
            else
            {
                return NotFound("No se encontraron permisos");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Error al recuperar los permisos: " + ex.Message);
        }
    }

    [HttpGet("{permissionId}")]
    public IActionResult GetPermissionById(int permissionId)
    {
        try
        {
            var permission = _getPermissionService.GetPermissionById(permissionId);

            if (permission != null)
            {
                return Ok(permission);
            }
            else
            {
                return NotFound("Permiso no encontrado");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Error al recuperar el permiso: " + ex.Message);
        }
    }
}
