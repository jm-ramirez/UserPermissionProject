using Microsoft.AspNetCore.Mvc;
using UserPermissionApi.Controllers.Schemas;
using UserPermissionApi.Services;

[ApiController]
[Route("api/permissions")]
public class UserPermissionController : ControllerBase
{
    private readonly IRequestPermissionService _requestPermissionService;
    private readonly IModifyPermissionService _modifyPermissionService;
    private readonly IGetPermissionService _getPermissionService;

    public UserPermissionController(IRequestPermissionService requestPermissionService, IGetPermissionService getPermissionService, IModifyPermissionService modifyPermissionService)
    {
        _requestPermissionService = requestPermissionService;
        _modifyPermissionService = modifyPermissionService; 
        _getPermissionService = getPermissionService;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand command)
    {
        try
        {
            await _requestPermissionService.Request(command);

            return Ok("Permiso creado con éxito.");
        }
        catch (Exception)
        {
            return Ok("Error al crear nuevo permiso.");
        }
    }

    [HttpPut("modify")]
    public async Task<IActionResult> UpdatePermission(ModifyPermissionCommand command)
    {
        try
        {
            await _modifyPermissionService.Update(command);

            return Ok("Permiso actualizado exitosamente");
        }
        catch (Exception ex)
        {
            return BadRequest("Error al actualizar el permiso: " + ex.Message);
        }
    }

    [HttpGet("get")]
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

    [HttpGet("get/{permissionId}")]
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
