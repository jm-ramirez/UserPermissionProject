using Microsoft.AspNetCore.Mvc;
using UserPermissionApi.Controllers.Schemas;
using UserPermissionApi.Services;

[ApiController]
[Route("api/modifypermissions")]
public class ModifyPermissionController : ControllerBase
{
    private readonly IModifyPermissionService _modifyPermissionService;

    public ModifyPermissionController(IModifyPermissionService modifyPermissionService)
    {
        _modifyPermissionService = modifyPermissionService;
    }

    [HttpPut]
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

    [HttpDelete("{permissionId}")]
    public async Task<IActionResult> DeletePermission(int permissionId)
    {
        try
        {
            await _modifyPermissionService.Delete(permissionId);

            return Ok("Permiso eliminado exitosamente");
        }
        catch (Exception ex)
        {
            return BadRequest("Error al eliminar el permiso: " + ex.Message);
        }
    }
}