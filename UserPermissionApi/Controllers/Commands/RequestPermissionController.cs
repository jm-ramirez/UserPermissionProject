using Microsoft.AspNetCore.Mvc;
using UserPermissionApi.Controllers.Schemas;
using UserPermissionApi.Services;

[ApiController]
[Route("api/permissions/request")]
public class RequestPermissionController : ControllerBase
{
    private readonly IRequestPermissionService _requestPermissionService;

    public RequestPermissionController(IRequestPermissionService requestPermissionService)
    {
        _requestPermissionService = requestPermissionService;
    }

    [HttpPost]
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
}