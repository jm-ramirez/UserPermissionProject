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
    private readonly IKafkaProducer _kafkaProducer; 

    public PermissionsController(
        IRequestPermissionService requestPermissionService,
        IGetPermissionService getPermissionService,
        IModifyPermissionService modifyPermissionService,
        IKafkaProducer kafkaProducer)
    {
        _requestPermissionService = requestPermissionService;
        _modifyPermissionService = modifyPermissionService; 
        _getPermissionService = getPermissionService;
        _kafkaProducer = kafkaProducer;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand command)
    {
        try
        {
            var response = await _requestPermissionService.Request(command);

            if (response == Nest.Result.Created)
            {
                // Envía un mensaje a Kafka para registrar la operación "request"
                var kafkaMessage = new KafkaMessageDto
                {
                    Id = Guid.NewGuid(), // Genera un ID único
                    Operation = "request"
                };
                await _kafkaProducer.ProduceAsync(kafkaMessage);

                return Ok("Permiso creado con éxito.");
            }
            else
            {
                return StatusCode(500, new { status = "error", message = "Error al crear nuevo permiso." });
            }
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
            var response = await _modifyPermissionService.Update(command);

            if (response == Nest.Result.Created) { 
                var kafkaMessage = new KafkaMessageDto
                {
                    Id = Guid.NewGuid(), 
                    Operation = "update"
                };
                await _kafkaProducer.ProduceAsync(kafkaMessage);

                return Ok("Permiso actualizado exitosamente");            
            }
            else
                return StatusCode(500, new { status = "error", message = "Error al actualizar permiso." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { status = "error", message = "Error al actualizar permiso." });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPermissions()
    {
        try
        {
            var permissions = _getPermissionService.GetAllPermissions();

            if (permissions != null)
            {
                var kafkaMessage = new KafkaMessageDto
                {
                    Id = Guid.NewGuid(),
                    Operation = "get"
                };
                await _kafkaProducer.ProduceAsync(kafkaMessage);

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
    public async Task<IActionResult> GetPermissionById(int permissionId)
    {
        try
        {
            var permission = _getPermissionService.GetPermissionById(permissionId);

            if (permission != null)
            {
                var kafkaMessage = new KafkaMessageDto
                {
                    Id = Guid.NewGuid(),
                    Operation = "getById"
                };
                await _kafkaProducer.ProduceAsync(kafkaMessage);

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
