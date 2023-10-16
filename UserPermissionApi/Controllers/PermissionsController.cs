using Microsoft.AspNetCore.Mvc;
using UserPermissionApi.Controllers.Schemas;
using UserPermissionApi.Services;

[ApiController]
[Route("api/permissions")]
public class PermissionsController : ControllerBase
{
    private readonly IRequestPermissionService _requestPermissionService;
    private readonly IModifyPermissionService _modifyPermissionService;
    private readonly IGetPermissionService _getPermissionService;

    public PermissionsController(IRequestPermissionService requestPermissionService, IGetPermissionService getPermissionService, IModifyPermissionService modifyPermissionService)
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
            var response = await _requestPermissionService.Request(command);

            if (response == Nest.Result.Created)
                return Ok("Permiso creado con éxito.");
            else
                return StatusCode(500, new { status = "error", message = "Error al crear nuevo permiso." });
        }
        catch (Exception)
        {
            return StatusCode(500, new { status = "error", message = "Error al crear nuevo permiso." });
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
