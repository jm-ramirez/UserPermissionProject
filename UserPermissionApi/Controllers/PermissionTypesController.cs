using Microsoft.AspNetCore.Mvc;
using UserPermissionApi.Services;

[ApiController]
[Route("api/permissiontypes")]
public class PermissionTypesController : ControllerBase
{
    private readonly IGetPermissionTypesService _getPermissionTypesService;

    public PermissionTypesController(IGetPermissionTypesService getPermissionTypesService)
    {
        _getPermissionTypesService = getPermissionTypesService;
    }

    [HttpGet]
    public IActionResult GetAllPermissionTypes()
    {
        try
        {
            var permissionTypes = _getPermissionTypesService.GetAllPermissionTypes();

            if (permissionTypes != null)
            {
                return Ok(permissionTypes);
            }
            else
            {
                return NotFound("No se encontraron tipos de permisos");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Error al recuperar los tipos de permisos: " + ex.Message);
        }
    }
}
