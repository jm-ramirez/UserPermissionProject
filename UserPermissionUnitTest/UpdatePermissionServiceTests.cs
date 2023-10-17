using Xunit;
using UserPermissionApi.Model;
using UserPermissionApi.Repositories;
using UserPermissionApi.Services;
using Moq;
using Nest;
using UserPermissionApi.Controllers.Schemas;
using System.Threading.Tasks;

namespace UserPermissionUnitTest
{
    public class UpdatePermissionServiceTests
    {
        [Fact]
        public async Task UpdatePermission_Should_Update_PermissionStatus()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockPermissionTypesRepository = new Mock<IPermissionTypeRepository<PermissionTypes>>();
            var mockPermissionsRepository = new Mock<IPermissionRepository<Permissions>>();

            // Simula el comportamiento de los métodos GetByNameAsync, UpdateAsync y SaveChangesAsync en IUnitOfWork
            mockUnitOfWork.Setup(uow => uow.PermissionTypes).Returns(mockPermissionTypesRepository.Object);
            mockUnitOfWork.Setup(uow => uow.Permissions).Returns(mockPermissionsRepository.Object);

            var modifyPermissionService = new ModifyPermissionService(mockUnitOfWork.Object);
            var requestCommand = new ModifyPermissionCommand
            {
                NombreEmpleado = "Juan",
                ApellidoEmpleado = "Ramirez",
                FechaPermiso = System.DateTime.Now,
                TipoPermisoNombre = "Lider Técnico",
            };

            mockPermissionTypesRepository.Setup(repo => repo.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string typeName) =>
                {
                    // Simula el comportamiento de GetByNameAsync
                    if (typeName == requestCommand.TipoPermisoNombre)
                    {
                        return null; // El tipo de permiso no existe
                    }
                    return new PermissionTypes { Id = 1, Descripcion = typeName };
                });

            // Act
            var result = await modifyPermissionService.Update(requestCommand);

            // Assert
            Assert.True(result == Result.Created);
            // Verifica que se haya creado el permiso con el tipo de permiso correcto en mockPermissionsRepository
            mockPermissionsRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Permissions>()), Times.Once);
            // Realiza otras comprobaciones según la lógica de tu servicio
            return;
        }
    }
}