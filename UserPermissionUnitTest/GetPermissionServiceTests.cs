using Moq;
using System.Collections.Generic;
using UserPermissionApi.Model;
using UserPermissionApi.Services;
using Xunit;

namespace UserPermissionUnitTest
{
    [Collection("Unit Tests")]
    public class GetPermissionServiceTests
    {
        private readonly GetPermissionService _getPermissionService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        // Constructor de la clase de pruebas
        public GetPermissionServiceTests()
        {
            //Configuro un mock de IUnitOfWork
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(uow => uow.Permissions.GetAllAsync())
                .ReturnsAsync(new List<Permissions>
                {
                new Permissions { Id = 1, NombreEmpleado = "Empleado1" },
                new Permissions { Id = 2, NombreEmpleado = "Empleado2" },
                });

            _unitOfWorkMock.Setup(uow => uow.Permissions.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int permissionId) =>
                {
                    // Implemento lógica para devolver un permiso simulado según el ID proporcionado.
                    // Esto te permite controlar qué permiso se devuelve en función del ID.
                    return new Permissions { Id = permissionId, NombreEmpleado = "Empleado 1" };
                });

            // Inicializo _getPermissionService con el mock de IUnitOfWork
            _getPermissionService = new GetPermissionService(_unitOfWorkMock.Object);
        }

        // Prueba unitaria para GetAllPermissions
        [Fact]
        public void GetAllPermissions_Should_Return_Permissions()
        {
            // Llamo al método GetAllPermissions de _getPermissionService.
            var permissions = _getPermissionService.GetAllPermissions();

            Assert.NotNull(permissions); // Verifico que la lista de permisos no sea nula.
            Assert.NotEmpty(permissions); // Verifico que la lista de permisos no esté vacía.
        }

        // Prueba unitaria para GetPermissionById
        [Fact]
        public void GetPermissionById_Should_Return_Specific_Permission()
        {
            // Lamo al método GetPermissionById de _getPermissionService con un ID específico.
            var permissionId = 1; // ID específico a probar.
            var permission = _getPermissionService.GetPermissionById(permissionId);

            Assert.NotNull(permission); // Verifica que el permiso no sea nulo.
            Assert.Equal(permissionId, permission.Id); // Verifica que el ID del permiso coincida con el ID especificado.
        }
    }
}
